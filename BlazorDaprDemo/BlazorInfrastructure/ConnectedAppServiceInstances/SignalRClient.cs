using Microsoft.Azure.SignalR.Management;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Serialization;
using MediatR;
using System.IO;
using Newtonsoft.Json;
using BlazorState;
using BlazorInfrastructure.Queue;

namespace BlazorInfrastructure.ConnectedAppServiceInstances
{
    /// <summary>
    /// Singleton class that's responsible for receiving messages from the connected app service instances
    /// This class currently contains three iterations of the same:
    /// 1) Single property broadcasting
    /// 2) BlazorState action broardcasting
    /// 3) Arbitrary entity broadcasting
    /// </summary>
    public class SignalRClient
    {
        private readonly IServiceManager _serviceManager;
        private readonly Shared shared;
        private readonly ILogger<SignalRClient> logger;
        private readonly IServiceProvider serviceProvider;        
        private readonly string user;
        private HubConnection hubConnection;

        public SignalRClient(Shared shared, ILogger<SignalRClient> logger, IServiceProvider serviceProvider)
        {
            _serviceManager = new ServiceManagerBuilder()
                .WithOptions(o => o.ConnectionString = shared.connectionstring)
                .Build();
            this.shared = shared;
            this.logger = logger;
            this.serviceProvider = serviceProvider;            
            user = Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID");
            if (string.IsNullOrEmpty(user))
            {
                // We assume you are running this code within the context of an app service
                // if we are running outside App Service the environment variable is not set and we need to assign a random value:
                user = Guid.NewGuid().ToString();
            }
            string siteName = Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME");
        }
        public HubConnection CreateConnectionDirect()
        {
            var url = _serviceManager.GetClientEndpoint(Shared.hubname);

            var accessToken = _serviceManager.GenerateClientAccessToken(Shared.hubname, user);

            var connection = new HubConnectionBuilder()
                    .WithUrl(url, option =>
                    {
                        option.AccessTokenProvider = () =>
                        {

                            return Task.FromResult(accessToken);
                        };
                    })
                    // .WithUrl("http://localhost:53353/ChatHub")
                    .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            return connection;
        }

        public async Task SetupClientAsync()
        {
            hubConnection = CreateConnectionDirect();
            await StartConnection();
        }

        public event EventHandler<MessageReceivedArgs> MessageReceived;        

        private async Task StartConnection()
        {
            //connection.On<string, string>(Target, (user, message) =>
            //{
            //    var newMessage = $"{user}: {message}";
            //    MessagesList.Add(newMessage);
            //});

            // Single property broadcasting:
            hubConnection.On(Shared.stringmethodname, (string message) =>
            {
                MessageReceived?.Invoke(this, new MessageReceivedArgs { Message = message });
            });

            // Arbitrary entity broadcasting (applied in Vacation Planner demo):
            hubConnection.On(Shared.crosscircuitmethodname, (string message) =>
            {
                MessageReceived?.Invoke(this, new MessageReceivedArgs { Message = message });
            });

            // BlazorState action broadcasting -
            // This scenario does not function properly as the transiently scoped BlazorState subscriptions within the component
            // are not available from within the backplane scope below. The result being that the component's view is not updated
            hubConnection.On(Shared.requestmethodname, async (string message) =>
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedProvider = scope.ServiceProvider;
                    var mediator = scopedProvider.GetRequiredService<IMediator>();

                    using (var reader = new JsonTextReader(new StringReader(message)))
                    {

                        object obj = shared.jsonSerializer.Value.Deserialize(reader);                        
                        
                        // The following commented out lines are converted below to processing a generic T:
                        // IncrementCountAction action = obj as IncrementCountAction;
                        // await mediator.Send(action);

                        var sendmethod = typeof(IMediator).GetMethod("Send");
                        
                        // Find the interface IRequest<T>:
                        var IRequestInterface = obj.GetType().GetInterfaces().First(i => i.Name == "IRequest`1");

                        // Create Send<T> based on the same T from IRequestInterface:
                        var genericmethod = sendmethod.MakeGenericMethod(IRequestInterface.GetGenericArguments()); 
                        
                        var result = genericmethod.Invoke(mediator, new object[] { obj, null });
                        Task t = (Task)result;                        
                        await t;
                    }
                }
            });


            try
            {
                await hubConnection.StartAsync();                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected exception when starting SignalR connection");
            }
        }
    }
}
