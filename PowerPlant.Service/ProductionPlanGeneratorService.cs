using PowerPlant.DataAccess;
using PowerPlant.Domain;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerPlant.Services
{
    public class ProductionPlanGeneratorService : IProductionPlanGeneratorService
    {
        private readonly ILogger _logger;
        private readonly IComparer<PowerplantProductionDto> _comparer;

        public ProductionPlanGeneratorService(ILogger logger, IComparer<PowerplantProductionDto> comparer)
        {
            _logger = logger;
            _comparer = comparer;
        }

        public object GenerateProductionPlan(double load, List<PowerplantProductionDto> powerplantProductions)
        {
            try
            {
                powerplantProductions = powerplantProductions
                    .OrderBy(x=>x, _comparer)
                    .ThenByDescending(x => x.PowerGenerated).ToList();
                List<PowerplantProductionDto> usedPowerplants = new List<PowerplantProductionDto>();

                double currentLoadToGet = load;

                foreach (PowerplantProductionDto powerplant in powerplantProductions)
                {
                    if (currentLoadToGet <= 0)
                    {
                        usedPowerplants.Add(new PowerplantProductionDto
                        {
                            PowerGenerated = 0,
                            Name = powerplant.Name
                        });
                        continue;
                    }

                    if (powerplant.PowerGenerated <= currentLoadToGet)
                    {
                        usedPowerplants.Add(powerplant);
                        currentLoadToGet = currentLoadToGet - powerplant.PowerGenerated;
                        continue;
                    }

                    if (powerplant.PowerGenerated > currentLoadToGet)
                    {
                        if (powerplant.PowerplantType != PowerplantType.Gas)
                        {
                            usedPowerplants.Add(powerplant);
                            currentLoadToGet = 0;
                            continue;
                        }

                        PowerplantProductionDto powerplantProduction = new PowerplantProductionDto()
                        {
                            Name = powerplant.Name,
                            PowerGenerated = Math.Round(Math.Max(powerplant.MinimumPowerGenerated, currentLoadToGet), 1)
                        };
                        usedPowerplants.Add(powerplantProduction);

                        currentLoadToGet -= powerplantProduction.PowerGenerated;
                    }
                }

                if (currentLoadToGet > 0)
                {
                    throw new InvalidOperationException($"Couldn't generate power enough to match the load! Power left to be generated: {currentLoadToGet}");
                }

                return usedPowerplants.Select(x => new { Name = x.Name, P = x.PowerGenerated }).ToList();
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error occured on Production Plan Generator service level: {e.Message}");
                throw;
            }
            
        }
    }
}
