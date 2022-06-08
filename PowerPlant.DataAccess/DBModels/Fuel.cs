using Newtonsoft.Json;

namespace PowerPlant.DataAccess
{
    public class Fuel
    {
        [JsonProperty("gas(euro/MWh)")]
        public double GasCost { get; set; }
        [JsonProperty("kerosine(euro/MWh)")]
        public double KerosineCost { get; set; }
        [JsonProperty("co2(euro/ton)")]
        public double CO2Emission { get; set; }
        [JsonProperty("wind(%)")]
        public double WindEfficiency { get; set; }
    }
}
