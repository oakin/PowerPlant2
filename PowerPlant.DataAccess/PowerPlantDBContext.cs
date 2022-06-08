using Microsoft.EntityFrameworkCore;


namespace PowerPlant.DataAccess
{

    public class PowerPlantDBContext : DbContext, IPowerPlantDBContext
    {
        public PowerPlantDBContext(DbContextOptions<PowerPlantDBContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        



        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

       
       
    }
}
