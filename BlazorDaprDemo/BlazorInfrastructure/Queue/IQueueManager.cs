using MediatR;
using System;
using System.Threading.Tasks;

namespace BlazorInfrastructure.Queue
{
    public interface IQueueManager
    {
        Task AddMessageAsync(string input);
        Task SendActionAsync<T>(IRequest<T> request);

        event EventHandler<MessageReceivedArgs> MessageReceived;
    }
}