using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorInfrastructure.CrossCircuitCommunication
{
    public interface ICrossCircuitCommunication
    {
        public HashSet<Action<MessagePayload>> GetCallbacksHashSet(string key, int index);
        public Task Dispatch(string key, int index, object message);
    }
}
