using AutoMapper;
using Lamar;
using Serilog.Core;

namespace PowerPlant.Api
{
    public class ApiRegistry : ServiceRegistry
    {
     

        public ApiRegistry()
        {
            //For<IControllerManager>().Use<ControllerManager>();

            //For<ISessionManager>().Use<SessionManager>();
            //Policies.SetAllProperties(y => y.OfType<ISessionManager>());



            For<IMapper>().Use<Mapper>();
            Policies.SetAllProperties(y => y.OfType<IMapper>());


            //For<IkinciElMobil.UI.Infrastructure.Validation.IValidatorFactory>().Use<ValidatorFactory>();
            //Policies.SetAllProperties(y => y.OfType<IkinciElMobil.UI.Infrastructure.Validation.IValidatorFactory>());





            //For<IConfiguration>().Use(x => configuration);




            // external login için
            //For<IUserStore<ApplicationUser>>().Use<EmptyUserStore<ApplicationUser>>();
            //For<IRoleStore<IdentityRole>>().Use<EmptyRoleStore<IdentityRole>>();


        }
    }
}
