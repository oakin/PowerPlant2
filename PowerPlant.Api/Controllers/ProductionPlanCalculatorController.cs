using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PowerPlant.DataAccess;
using PowerPlant.Domain;
using PowerPlant.Services;


namespace ProductionPlan.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductionPlanCalculatorController : ControllerBase
    {
        private readonly IPowerGenerationCalculatorService _powerGenerationCalculatorservice;
        private readonly IProductionPlanGeneratorService _productionPlanGeneratorservice;
        private readonly Serilog.ILogger _logger;
        private readonly IBroadcastService _broadcastService;

        public ProductionPlanCalculatorController(
            IPowerGenerationCalculatorService powerGenerationCalculatorservice, 
            IProductionPlanGeneratorService productionPlanGeneratorservice,
            Serilog.ILogger logger, 
            IBroadcastService broadcastService)
        {
            _powerGenerationCalculatorservice = powerGenerationCalculatorservice;
            _productionPlanGeneratorservice = productionPlanGeneratorservice;
            _logger = logger;
            _broadcastService = broadcastService;
        }

        [HttpPost("productionplan", Name = "Production Plan Calculator")]
        public IActionResult CalculateProductionPlan([FromBody] Payload payload)
        {
            try
            {
                List<PowerplantProductionDto> powerplantProductions = _powerGenerationCalculatorservice.CalculatePowerplantsProduction(payload.Fuels, payload.Powerplants);
                var result = _productionPlanGeneratorservice.GenerateProductionPlan(payload.Load, powerplantProductions);

                try
                {
                    _broadcastService.BroadcastRequestAndResponse(JsonConvert.SerializeObject(payload), JsonConvert.SerializeObject(result));
                }
                catch (Exception be)
                {
                    _logger.Error(be, $"Couldn't broadcast message. Error: {be.Message}");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error occured! Error message: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"We're sorry! An error has occured.. Error message: {e.Message}");
            }
            
        }
    }
}
