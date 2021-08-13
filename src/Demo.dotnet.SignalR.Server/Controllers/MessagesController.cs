using Demo.AWS.SignalR.Server.DTOs;
using Demo.AWS.SignalR.Server.Hubs;
using Demo.AWS.SignalR.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Demo.AWS.SignalR.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;
        private readonly IMetadataService _metadataService;

        public MessagesController(
            IHubContext<ChatHub, IChatHub> hubContext,
            IMetadataService metadataService
        )
        {
            this._hubContext = hubContext;
            this._metadataService = metadataService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(MessageRequest request)
        {
            try
            {
                var metadata = this._metadataService.GetMetadata();

                await this._hubContext
                    .Clients.All
                        .OnMessage(new Message(
                            request.Text,
                            metadata.AWSInstanceId,
                            metadata.MachineName
                        ));

                return this.Ok(metadata);
            }
            catch(Exception exception)
            {
                return this.Ok(exception.ToString());
            }
        }
    }
}