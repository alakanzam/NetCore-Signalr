using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using SignalrCore.Attributes;
using SignalrCore.Authentications.Requirements;
using SignalrCore.Interfaces;
using SignalrCore.Interfaces.Repositories;

namespace SignalrCore.Authentications.Handlers
{
    public class SolidAccountRequirementHandler : AuthorizationHandler<SolidAccountRequirement>
    {
        #region Constructor

        /// <summary>
        ///     Initiate requirement handler with injectors.
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userRepository"></param>
        public SolidAccountRequirementHandler(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Handle requirement asychronously.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            SolidAccountRequirement requirement)
        {
            // Convert authorization filter context into authorization filter context.
            var authorizationFilterContext = (AuthorizationFilterContext)context.Resource;

            //var httpContext = authorizationFilterContext.HttpContext;
            var httpContext = _httpContextAccessor.HttpContext;

            // Find claim identity attached to principal.
            var claimIdentity = (ClaimsIdentity)httpContext.User.Identity;

            // Find id from claims list.
            var email = claimIdentity.Claims.Where(x => x.Type.Equals(ClaimValueTypes.Email))
                .Select(x => x.Value)
                .FirstOrDefault();


            // Id is invalid
            if (string.IsNullOrEmpty(email) || !int.TryParse(email, out int iId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            // Check whether email exists or not.
            var bIsEmailAvailable = _userRepository.Get()
                .Any(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));

            if (!bIsEmailAvailable)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Context accessor.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IUserRepository _userRepository;

        #endregion
    }
}