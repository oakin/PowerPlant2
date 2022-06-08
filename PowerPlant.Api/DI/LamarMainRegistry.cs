using PowerPlant.DataAccess;
using Lamar;
using Microsoft.EntityFrameworkCore;
using Serilog.Core;
using PowerPlant.Domain;

namespace PowerPlant.Api
{
    internal class LamarMainRegistry : ServiceRegistry
    {
        private IConfigurationRoot _configuration;

        public LamarMainRegistry(IConfiguration configuration) 
        {

            Scan(x =>
            {
                x.Assembly(typeof(Program).Assembly);
                x.WithDefaultConventions();
                x.Assembly("PowerPlant.Domain");
                x.Assembly("PowerPlant.DataAccess");
                x.Assembly("PowerPlant.Service");
                x.Assembly("PowerPlant.Api");
            });

    
            IncludeRegistry<AutoMapperRegistry>();
            IncludeRegistry<ApiRegistry>();
     



            //For<IValidatorFactory>().Use<ValidatorFactory>();
            //Policies.SetAllProperties(y => y.OfType<IValidatorFactory>());

            For<IConfigurationRoot>().Use(x => _configuration);

            For<Serilog.ILogger>().Use(ctx => Serilog.Log.Logger);

            //For<IComparer<Domain.PowerplantProductionDto>>().Use(ctx=> PowerplantCostComparer);

            var connectionString = configuration.GetConnectionString("PowerPlantDBContext");
            var optionsBuilder = new DbContextOptionsBuilder<PowerPlantDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            //For<UIRegistry>().Use<UIRegistry>();

            For<IPowerPlantDBContext>().Use<PowerPlantDBContext>()
                              .Ctor<DbContextOptions<PowerPlantDBContext>>("options")
                                          .Is(optionsBuilder.Options).Singleton();

            //Policies.SetAllProperties(y => y.OfType<ILogService>());

        }
    }
}