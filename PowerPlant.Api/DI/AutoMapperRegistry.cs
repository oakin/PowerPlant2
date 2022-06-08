using AutoMapper;
using PowerPlant.Domain;
using Lamar;
using System.Reflection;

namespace PowerPlant.Api
{
    public class AutoMapperRegistry : ServiceRegistry
    {
        public AutoMapperRegistry()
        {
            var assemblyNames = new List<string>
            {
                "PowerPlant.Domain",
                "PowerPlant.Api",
               
            };

            var asssm = assemblyNames.Select(assemblyName => Assembly.Load(assemblyName)).ToList();

            

            var types = assemblyNames.Select(assemblyName => Assembly.Load(assemblyName)).ToList()
                                     .SelectMany(assembly => assembly.GetLoadableTypes()).ToList();

            var profiles = from t in types
                           where typeof(Profile).IsAssignableFrom(t)
                           select (Profile)Activator.CreateInstance(t);

            //For each Profile, include that profile in the MapperConfiguration
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }

            });

            //Create a mapper that will be used by the DI container
            var mapper = config.CreateMapper();

            //Register the DI interfaces with their implementation
            For<AutoMapper.IConfigurationProvider>().Use(config);
            For<IMapper>().Use(mapper);

        }


       
    }
}
