using Microsoft.EntityFrameworkCore;
namespace PowerPlant.DataAccess
{
    public interface IPowerPlantDBContext
    {
      
      
      

        Task<int> SaveChangesAsync();
    }
}
