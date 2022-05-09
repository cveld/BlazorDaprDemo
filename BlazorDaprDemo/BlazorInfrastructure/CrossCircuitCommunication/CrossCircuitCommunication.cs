using BlazorInfrastructure.ConnectedAppServiceInstances;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorInfrastructure.CrossCircuitCommunication
{
    public class CrossCircuitCommunication
    {
        public CrossCircuitCommunication(SignalRClient signalRClient, SignalRServer signalRServer)
        {
            this.signalRClient = signalRClient;
            this.signalRServer = signalRServer;

            signalRClient.MessageReceived += SignalRClient_MessageReceived;
        }

        protected Lazy<JsonSerializer> jsonSerializer = new Lazy<JsonSerializer>(() =>
        {
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.TypeNameHandling = TypeNameHandling.Objects;
            return jsonSerializer;
        });

        private void SignalRClient_MessageReceived(object sender, Queue.MessageReceivedArgs e)
        {
            using (var reader = new JsonTextReader(new StringReader((string)e.Message)))
            {
                object obj = jsonSerializer.Value.Deserialize(reader);
                MessagePayload messagePayload = (MessagePayload)obj;
                var callbacks = GetCallbacksHashSet(messagePayload.Key, messagePayload.Index);
                foreach (var callback in callbacks)
                {
                    callback.Invoke(messagePayload);
                }
            }
        }

        ConcurrentDictionary<string, ConcurrentDictionary<int, HashSet<Action<MessagePayload>>>> Subscriptions = new ConcurrentDictionary<string, ConcurrentDictionary<int, HashSet<Action<MessagePayload>>>>();
        private readonly SignalRClient signalRClient;
        private readonly SignalRServer signalRServer;

        public HashSet<Action<MessagePayload>> GetCallbacksHashSet(string key, int index)
        {
            var keydict = Subscriptions.GetOrAdd(key, (_) => new ConcurrentDictionary<int, HashSet<Action<MessagePayload>>>());
            var indexdict = keydict.GetOrAdd(index, (_) => new HashSet<Action<MessagePayload>>());
            return indexdict;
        }

        async public Task Dispatch(string key, int index, object message)
        {
            await signalRServer.SendObjectAsync(new MessagePayload
            {
                Key = key,
                Index = index,
                Message = message
            }, MessageTypeEnum.CrossCircuit);
        }
    }
}
