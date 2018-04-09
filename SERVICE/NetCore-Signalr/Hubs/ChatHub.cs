using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SignalrCore.Constants;
using SignalrCore.Interfaces;
using SignalrCore.Models;

namespace SignalrCore.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatHub(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        {
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Fired when a client connects to the hub.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = PolicyConstant.DefaultSignalRPolicyName)]
        public override Task OnConnectedAsync()
        {
            var connectionCacheService = _serviceProvider.GetService<IConnectionCacheService>();
            var logger = _serviceProvider.GetService<ILogger<ChatHub>>();

            var user = connectionCacheService.FindKey(Context.ConnectionId);

            if (user != null)
            {
                logger.LogInformation($"{Context.ConnectionId} exists.");
                return base.OnConnectedAsync();
            }

            var httpUser = _httpContextAccessor.HttpContext.User;
            var email = httpUser.FindFirst(ClaimValueTypes.Email).Value;
            
            var u = new User(email);
            connectionCacheService.Add(Context.ConnectionId, u);

            logger.LogInformation($"{Context.ConnectionId} has been imported to cache.");
            return base.OnConnectedAsync();
        }
    }
}