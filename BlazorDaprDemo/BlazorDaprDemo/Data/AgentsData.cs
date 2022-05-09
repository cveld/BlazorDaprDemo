using BlazorServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerSide.Data
{
    public class AgentsData
    {
        public static List<AgentModel> GetAgents()
        {
            return new List<AgentModel>
            {
                new AgentModel { Longitude = -74.0060, Latitude = 40.7126, Name = "George Smiley", Mission = "Place listening device in City Hall" },
                new AgentModel { Longitude = -73.9989, Latitude = 40.7077, Name = "Nancy Drew", Mission = "Control bridge access" },
                new AgentModel { Longitude = -74.0102, Latitude = 40.7068, Name = "Natalya Ivanova", Mission = "Trigger global banking crisis" },
                new AgentModel { Longitude = -74.0165, Latitude = 40.7034, Name = "Hans Kloss", Mission = "Infiltrate castle" },
                new AgentModel { Longitude = -74.0063, Latitude = 40.7033, Name = "Agent K", Mission = "Prepare SCUBA™ equipment" },
                new AgentModel { Longitude = -74.0152, Latitude = 40.7104, Name = "Jason Bourne", Mission = "Look out for suspicious characters" },
                new AgentModel { Longitude = -74.0107, Latitude = 40.7038, Name = "Johnny Fedora", Mission = "Awaiting orders" },
                new AgentModel { Longitude = -74.0073, Latitude = 40.7131, Name = "Emily Pollifax", Mission = "Block enemy agents from reaching Broadway" },
                new AgentModel { Longitude = -74.0079, Latitude = 40.7149, Name = "Work experience boy", Mission = "Check prices of Dell XPS laptops" },
                new AgentModel { Longitude = -74.0069, Latitude = 40.7134, Name = "Tim Donohue", Mission = "Get part in Broadway show" },
                new AgentModel { Longitude = -74.0058, Latitude = 40.7028, Name = "David Shirazi", Mission = "Intercept enemy shipment" },
                new AgentModel { Longitude = -74.0014, Latitude = 40.7054, Name = "Max Payne", Mission = "Prepare shipment" },
                new AgentModel { Longitude = -74.0140, Latitude = 40.7027, Name = "Evelyn Salt", Mission = "General surveillance rota" },
            };
        }
    }
}

