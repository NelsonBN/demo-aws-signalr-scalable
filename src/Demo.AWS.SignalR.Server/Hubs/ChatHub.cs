using Demo.AWS.SignalR.Server.DTOs;
using Demo.AWS.SignalR.Server.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Demo.AWS.SignalR.Server.Hubs
{

    public class ChatHub : Hub<IChatHub>
    {
        private readonly IMetadataService _metadataService;

        public ChatHub(IMetadataService metadataService)
        {
            this._metadataService = metadataService;
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"AWSInstanceId: '{this._metadataService.GetAWSInstanceId()}', MachineNameClient: '{this._metadataService.GetMachineName()}', ConnectionId: '{this.Context.ConnectionId}' connected");

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {

            if(string.IsNullOrWhiteSpace(exception?.Message))
            {
                Console.WriteLine($"AWSInstanceId: '{this._metadataService.GetAWSInstanceId()}', MachineNameClient: '{this._metadataService.GetMachineName()}', ConnectionId: '{this.Context.ConnectionId}' disconnected");
            }
            else
            {
                Console.WriteLine($"AWSInstanceId: '{this._metadataService.GetAWSInstanceId()}', MachineNameClient: '{this._metadataService.GetMachineName()}', ConnectionId: '{this.Context.ConnectionId}' disconnected > {exception.Message}");
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task OnMessage(string clientMessage)
        {
            string awsInstanceId = this._metadataService.GetAWSInstanceId();
            string machineName = this._metadataService.GetMachineName();

            Console.WriteLine($"AWSInstanceId: '{awsInstanceId}', MachineNameClient: '{machineName}', ConnectionId: '{this.Context.ConnectionId}' message: {clientMessage}");

            await this.Clients
                .Client(this.Context.ConnectionId)
                    .OnMessage(new Message(
                        clientMessage,
                        awsInstanceId,
                        machineName
                    ));
        }
    }
}