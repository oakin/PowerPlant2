using System.Collections.Generic;

namespace PowerPlant.Domain
{
    public class PowerplantCostComparer : IComparer<PowerplantProductionDto>
    {
        public int Compare(PowerplantProductionDto x, PowerplantProductionDto y)
        {
            // return wind turbine as smallest since it is cost free.
            if (x.PowerplantType == PowerplantType.Windturbine)
            {
                return -1;
            }

            if (y.PowerplantType == PowerplantType.Windturbine)
            {
                return 1;
            }

            //if they are same type, look at efficency and then decide higher efficency should cost less power generated.
            if (x.PowerplantType == y.PowerplantType)
            {
                return -1 * x.Efficiency.CompareTo(y.Efficiency);
            }

            //compare 1 gas and 1 turbo jet power plant

            return x.Cost.CompareTo((y.Cost * x.PowerGenerated) / y.PowerGenerated);
        }
    }
}
