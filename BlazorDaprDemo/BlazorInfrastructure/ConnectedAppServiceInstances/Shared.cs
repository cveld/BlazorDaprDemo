using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorInfrastructure.ConnectedAppServiceInstances
{
    public class Shared
    {
        public const string hubname = "connectedinstances";
        public const string stringmethodname = "connectedinstancemessage_string";
        public const string crosscircuitmethodname = "connectedinstancemessage_crosscircuit";
        public const string requestmethodname = "connectedinstancemessage_request";
        private readonly IConfiguration configuration;
        public string connectionstring;

        public Shared(IConfiguration configuration)
        {
            this.configuration = configuration;
            var section = configuration.GetSection("Azure:SignalR:ConnectedInstancesConnectionString");
            connectionstring = section.Value;
        }

        public Lazy<JsonSerializer> jsonSerializer = new Lazy<JsonSerializer>(() =>
        {
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.TypeNameHandling = TypeNameHandling.Objects;
            return jsonSerializer;
        });
    }
}
