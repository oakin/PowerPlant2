using PowerPlant.Domain;

namespace PowerPlant.DataAccess
{
    public class PowerplantProduction
    {
        public string Name { get; set; }
        public PowerplantType PowerplantType { get; set; }
        public double PowerGenerated { get; set; }
        public double Cost { get; set; }
        public double MinimumPowerGenerated { get; set; }
        public double Efficiency { get; set; }
    }
}
