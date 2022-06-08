using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PowerPlant.Domain;

namespace PowerPlant.DataAccess
{
    public class Powerplant
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PowerplantType PowerplantType { get; set; }
        [JsonProperty("efficiency")]
        public double Efficiency { get; set; }
        [JsonProperty("pmin")]
        public double MinimumPower { get; set; }
        [JsonProperty("pmax")]
        public double MaximumPower { get; set; }
    }
}
