using Micro.PlaformService.Models;

namespace Micro.PlaformService.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();

        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);
        void CreatePlaform(Platform platform);
    }
}
