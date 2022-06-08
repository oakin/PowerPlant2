using Newtonsoft.Json;
using System.Collections.Generic;

namespace PowerPlant.DataAccess
{
    public class Payload
    {
        [JsonProperty("load")]
        public double Load { get; set; }
        [JsonProperty("fuels")]
        public Fuel Fuels { get; set; }
        [JsonProperty("powerplants")]
        public List<Powerplant> Powerplants { get; set; }
    }
}
