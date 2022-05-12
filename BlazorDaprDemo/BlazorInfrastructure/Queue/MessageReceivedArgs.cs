namespace BlazorInfrastructure.Queue
{
    public class MessageReceivedArgs
    {
        /// <summary>
        /// The message that is being received
        /// </summary>
        public object Message { get; set; }
    }
}