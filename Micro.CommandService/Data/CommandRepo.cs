using Micro.CommandService.Models;
using Microsoft.EntityFrameworkCore;

namespace Micro.CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateCommand(int platformId, Command command)
        {
            if (command == null)
                throw new ArgumentException(nameof(command));

            command.PlatformId = platformId;

            await _context.Commands.AddAsync(command);
        }

        public async Task CreatePlatform(Platform platform)
        {
            if(platform == null)
                throw new ArgumentException(nameof(platform));
            
            await _context.Platforms.AddAsync(platform);
        }

        public async Task<IEnumerable<Platform>> GetAllPlatforms()
        {
            return await _context.Platforms.ToListAsync();
        }

        public async Task<Command> GetCommand(int platformId, int commandId)
        {
           var command = await _context.Commands.FirstOrDefaultAsync(x=>x.Id == commandId && x.PlatformId == platformId);

            if(command == null)
            {
                throw new Exception("Не удалось найти");
            }

            return command;
        }

        public async Task<IEnumerable<Command>> GetCommandsForPlatform(int platformId)
        {
            return await _context.Commands.Where(x=>x.PlatformId == platformId).ToListAsync();
        }

        public async Task<bool> PlatformExist(int platformId)
        {
            return await _context.Platforms.AnyAsync(x=>x.Id == platformId);
        }

        public async Task<bool> SaveChanges()
        {
            return ( (await _context.SaveChangesAsync()) >= 0);
        }
    }
}
