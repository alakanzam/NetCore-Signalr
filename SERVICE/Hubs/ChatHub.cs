using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalrCore.Hubs
{
    public class ChatHub : Hub
    {
        /// <summary>
        /// Fired when a client connects to the hub.
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}