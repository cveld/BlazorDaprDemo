using BlazorInfrastructure.ConnectedAppServiceInstances;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorInfrastructure.Queue
{
    public class SignalRQueueManager : IQueueManager
    {
        private readonly SignalRServer server;
        private readonly SignalRClient client;

        public SignalRQueueManager(SignalRServer server, SignalRClient client)
        {
            this.server = server;
            this.client = client;

            client.MessageReceived += Client_MessageReceived;
        }

        private void Client_MessageReceived(object sender, MessageReceivedArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        public event EventHandler<MessageReceivedArgs> MessageReceived;

        public async Task AddMessageAsync(string input)
        {
            await server.SendMessageAsync(input, MessageTypeEnum.String);
        }

        public async Task SendActionAsync<T>(IRequest<T> request)
        {
            await server.SendActionAsync(request, MessageTypeEnum.Request);
        }
    }
}
