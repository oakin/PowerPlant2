using PowerPlant.DataAccess;
using PowerPlant.Domain;
using Serilog;
using System;
using System.Collections.Generic;

namespace PowerPlant.Services
{
    public class PowerGenerationCalculatorService : IPowerGenerationCalculatorService
    {
        private readonly ILogger _logger;

        public PowerGenerationCalculatorService(ILogger logger)
        {
            _logger = logger;
        }

        public List<PowerplantProductionDto> CalculatePowerplantsProduction(Fuel fuel, List<Powerplant> powerplants)
        {
            try
            {
                List<PowerplantProductionDto> result = new List<PowerplantProductionDto>();

                foreach (Powerplant powerplant in powerplants)
                {
                    PowerplantProductionDto powerplantProduction = new PowerplantProductionDto
                    {
                        Name = powerplant.Name,
                        PowerplantType = powerplant.PowerplantType,
                        Efficiency = powerplant.Efficiency
                    };

                    switch (powerplant.PowerplantType)
                    {
                        case PowerplantType.Windturbine:
                            powerplantProduction.PowerGenerated = Math.Round(powerplant.MaximumPower * fuel.WindEfficiency / 100.0,1);
                            break;
                        case PowerplantType.Gas:
                            powerplantProduction.Cost = (powerplant.MaximumPower / powerplant.Efficiency) * fuel.GasCost;
                            powerplantProduction.PowerGenerated = powerplant.MaximumPower;
                            powerplantProduction.MinimumPowerGenerated = powerplant.MinimumPower;
                            break;
                        case PowerplantType.Turbojet:
                            powerplantProduction.Cost = (powerplant.MaximumPower / powerplant.Efficiency) * fuel.KerosineCost;
                            powerplantProduction.PowerGenerated = powerplant.MaximumPower;
                            break;
                        default:
                            throw new InvalidOperationException($"Powerplant type: {powerplant.PowerplantType} is unknown!");
                    }

                    result.Add(powerplantProduction);
                }

                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error occured on Power Generation Calculator service level: {e.Message}");
                throw;
            }
            
        }
    }
}
