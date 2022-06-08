using PowerPlant.DataAccess;
using PowerPlant.Domain;
using System.Collections.Generic;

namespace PowerPlant.Services
{
    public interface IPowerGenerationCalculatorService
    {
        List<PowerplantProductionDto> CalculatePowerplantsProduction(Fuel fuel, List<Powerplant> powerplants);
    }
}
