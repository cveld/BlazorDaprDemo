// using Microsoft.AspNetCore.SignalR.Client;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.SignalR.Management;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BlazorInfrastructure.ConnectedAppServiceInstances
{
    public class SignalRServer
    {
        private readonly Shared shared;
        private readonly ILogger<SignalRServer> logger;
        private IServiceHubContext _hubContext;
        private IServiceManager serviceManager;
        Task initialization;

        public SignalRServer(Shared shared, ILogger<SignalRServer> logger)
        {
            this.shared = shared;
            this.logger = logger;
            initialization = Initialize();
        }

        private async Task Initialize()
        {
            serviceManager = new ServiceManagerBuilder().WithOptions(option =>
            {
                option.ConnectionString = shared.connectionstring;
                // we choose the REST API. we could also go for websockets transport
                option.ServiceTransportType = ServiceTransportType.Transient;
            }).Build();

            _hubContext = await serviceManager.CreateHubContextAsync(Shared.hubname, new LoggerFactory());
        }

        private void EnsureInitiatized()
        {
            if (initialization.IsCompleted)
            {
                return;
            }
            initialization.Wait();
        }

        public async Task SendMessageAsync(string message, MessageTypeEnum messageTypeEnum)
        {
            EnsureInitiatized();
            switch (messageTypeEnum) {
                case MessageTypeEnum.CrossCircuit:
                    await _hubContext.Clients.All.SendAsync(Shared.crosscircuitmethodname, message);
                    break;
                case MessageTypeEnum.String:
                    await _hubContext.Clients.All.SendAsync(Shared.stringmethodname, message);
                    break;
                case MessageTypeEnum.Request:
                    await _hubContext.Clients.All.SendAsync(Shared.requestmethodname, message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(messageTypeEnum));
            }
        }

        public async Task SendActionAsync<T>(IRequest<T> request, MessageTypeEnum messageTypeEnum)
        {                        
            using (StringWriter textWriter = new StringWriter())
            {
                shared.jsonSerializer.Value.Serialize(textWriter, request);
                var s = textWriter.ToString();
                await SendMessageAsync(s, messageTypeEnum);
            }
        }
        public async Task SendObjectAsync(object request, MessageTypeEnum messageTypeEnum)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                shared.jsonSerializer.Value.Serialize(textWriter, request);
                var s = textWriter.ToString();
                await SendMessageAsync(s, messageTypeEnum);
            }
        }
    }
}
