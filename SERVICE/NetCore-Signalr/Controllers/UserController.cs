using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using SignalrCore.Interfaces;
using SignalrCore.Interfaces.Repositories;
using SignalrCore.Models;
using SignalrCore.Services;
using SignalrCore.ViewModels;

namespace SignalrCore.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        #region Properties

        private readonly IUserRepository _userRepository;

        private readonly JwtOption _jwtOption;

        private readonly ITimeService _timeService;

        #endregion

        #region Methods

        /// <summary>
        /// Sign user into system.
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel info)
        {
            if (info == null)
            {
                info = new LoginViewModel();
                TryValidateModel(info);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userRepository
                .Get().FirstOrDefault(x => x.Email.Equals(info.Email, StringComparison.InvariantCultureIgnoreCase));

            if (user == null)
                return NotFound();

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimValueTypes.Email, user.Email));
            claims.Add(new Claim(ClaimValueTypes.KeyInfo, user.Id.ToString()));

            // Find current time on the system.
            var systemTime = DateTime.Now;
            var jwtExpiration = systemTime.AddSeconds(_jwtOption.LifeTime);
            
            // Write a security token.
            var jwtSecurityToken = new JwtSecurityToken(_jwtOption.Issuer, _jwtOption.Audience, claims,
                null, jwtExpiration, _jwtOption.SigningCredentials);

            // Initiate token handler which is for generating token code.
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            // Initialize jwt response.
            var jwt = new JwtResponse();
            jwt.Code = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            jwt.LifeTime = _jwtOption.LifeTime;
            jwt.Expiration = _timeService.DateTimeUtcToUnix(jwtExpiration);

            return Ok(jwt);
        }

        #endregion

        #region Constructor

        public UserController(IUserRepository userRepository, 
            ITimeService timeService,
            IOptions<JwtOption> jwtOptions)
        {
            _userRepository = userRepository;
            _timeService = timeService;
            _jwtOption = jwtOptions.Value;
        }

        #endregion
    }
}