using PowerPlant.DataAccess;
using PowerPlant.Domain;

namespace PowerPlant.Services
{
    public interface IProductionPlanGeneratorService
    {
        object GenerateProductionPlan(double load, List<PowerplantProductionDto> powerplantProductions);
    }
}
