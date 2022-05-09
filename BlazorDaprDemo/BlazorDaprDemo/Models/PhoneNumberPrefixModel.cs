using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerSide.Models
{
    public class PhoneNumberPrefixModel
    {
        public const string PREFIXNETHERLANDS = "+31";

        public string ID { get; set; }
        public string Prefix { get; set; }
        public string Display { get; set; }        
        public static List<PhoneNumberPrefixModel> PhoneNumberPrefixList()
        {
            return new List<PhoneNumberPrefixModel> {
                new PhoneNumberPrefixModel {
                    ID = PREFIXNETHERLANDS,
                    Prefix = PREFIXNETHERLANDS,
                    Display = $"Netherlands ({PREFIXNETHERLANDS})"
                },
                new PhoneNumberPrefixModel {
                    ID = "Other",
                    Prefix = "",
                    Display = "Other"
                }
            };
        }

    }
}
