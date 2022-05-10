using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorInfrastructure.CrossCircuitCommunication
{
    public class DummyCrossCircuitCommunication : ICrossCircuitCommunication
    {
        public DummyCrossCircuitCommunication(ILogger<DummyCrossCircuitCommunication> logger)
        {
            this.logger = logger;
        }
        ConcurrentDictionary<string, ConcurrentDictionary<int, HashSet<Action<MessagePayload>>>> Subscriptions = new ConcurrentDictionary<string, ConcurrentDictionary<int, HashSet<Action<MessagePayload>>>>();
        private readonly ILogger logger;

        public Task Dispatch(string key, int index, object message)
        {
            logger.LogInformation($"Dummy dispatch operation: {key}, {index}, {message}");
            return Task.CompletedTask;
        }

        public HashSet<Action<MessagePayload>> GetCallbacksHashSet(string key, int index)
        {
            var keydict = Subscriptions.GetOrAdd(key, (_) => new ConcurrentDictionary<int, HashSet<Action<MessagePayload>>>());
            var indexdict = keydict.GetOrAdd(index, (_) => new HashSet<Action<MessagePayload>>());
            return indexdict;
        }
    }
}
