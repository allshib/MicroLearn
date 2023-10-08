using AutoMapper;
using Micro.CommandService.Data;
using Micro.CommandService.Dtos;
using Micro.CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Micro.CommandService.Controllers
{
    [ApiController]
    [Route("api/c/platforms/{platformId}/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommandsForPlatforms(int platformId)
        {
            Console.WriteLine($"Получаем команды для платформы с id = {platformId}");

            if (!(await _repo.PlatformExist(platformId)))
            {
                return NotFound();
            }

            var commands = _repo.GetCommandsForPlatform(platformId);

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public async Task<ActionResult<CommandReadDto>> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"Получаем команду для платформы с id = {platformId} и id команды = {commandId}");

            if (!(await _repo.PlatformExist(platformId)))
            {
                return NotFound();
            }

            var command = await _repo.GetCommand(platformId, commandId);

            if(command == null) 
                return NotFound();

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommand(int platformId, CommandCreateDto commandDto)
        {
            Console.WriteLine($"Создаем команду для платформы с id = {platformId}");

            if (!(await _repo.PlatformExist(platformId)))
            {
                return NotFound();
            }
            var command = _mapper.Map<Command>(commandDto);
            await _repo.CreateCommand(platformId, command);

            if (await _repo.SaveChanges())
            {
                var commandRead = _mapper.Map<CommandReadDto>(command);
                return CreatedAtRoute(nameof(GetCommandForPlatform), new {platformId = platformId, commandId = commandRead.Id}, commandRead);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
