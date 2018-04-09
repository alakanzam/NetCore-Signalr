using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalrCore.Hubs;
using SignalrCore.ViewModels;

namespace SignalrCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        #region Properties

        private readonly IHubContext<ChatHub> _chatHubContext;

        #endregion

        #region Constructor

        public MessageController(IHubContext<ChatHub> chatHubContext)
        {
            _chatHubContext = chatHubContext;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Send message to specific clients.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] SendMessageViewModel info)
        {
            if (info == null)
            {
                info = new SendMessageViewModel();
                TryValidateModel(info);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            IClientProxy clientProxy;

            if (info.ClientIds == null || info.ClientIds.Count < 1)
                clientProxy = _chatHubContext.Clients.All;
            else
                clientProxy = _chatHubContext.Clients.Clients(info.ClientIds.ToList());

            await clientProxy.SendAsync(info.EventName, info.Item);
            return Ok();
        }

        #endregion
    }
}