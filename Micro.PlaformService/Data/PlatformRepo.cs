using Micro.PlaformService.Models;

namespace Micro.PlaformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext context;
        public PlatformRepo(AppDbContext context) {
            this.context = context;
        }

        public void CreatePlaform(Platform platform)
        {
            if (platform == null) throw new ArgumentException(nameof(platform));

            context.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return context.Platforms.FirstOrDefault(p=>p.Id == id); 
        }

        public bool SaveChanges()
        {
            return(context.SaveChanges() >= 0);
        }
    }
}
