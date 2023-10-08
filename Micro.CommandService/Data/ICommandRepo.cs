using Micro.CommandService.Models;

namespace Micro.CommandService.Data
{
    public interface ICommandRepo
    {
        Task<bool> SaveChanges();

        //Platforms
        Task<IEnumerable<Platform>> GetAllPlatforms();
        Task CreatePlatform(Platform platform);
        Task<bool> PlatformExist(int platformId);

        //Commands
        Task<IEnumerable<Command>> GetCommandsForPlatform(int platformId);
        Task<Command> GetCommand(int platformId, int commandId);
        Task CreateCommand(int platformId, Command command);
    }
}
