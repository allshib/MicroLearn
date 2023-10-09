using AutoMapper;
using Micro.PlaformService.AsyncDataServices;
using Micro.PlaformService.Data;
using Micro.PlaformService.Dtos;
using Micro.PlaformService.Models;
using Micro.PlaformService.SyncDataServicesHttp;
using Microsoft.AspNetCore.Mvc;

namespace Micro.PlaformService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : Controller
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _dataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(IPlatformRepo repository, IMapper mapper, ICommandDataClient dataClient, IMessageBusClient messageBusClient) {
            _repository = repository;
            _mapper = mapper;
            _dataClient = dataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms");
            var platformItems = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platform =_repository.GetPlatformById(id);

            if(platform != null) 
                return Ok(_mapper.Map<PlatformReadDto>(platform));

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformCreateDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        { 
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repository.CreatePlaform(platformModel);



            if (!_repository.SaveChanges())
                this.BadRequest();

            

            var platformRead = _mapper.Map<PlatformReadDto>(platformModel);
            


            //Sync
            try
            {
                await _dataClient.SendPlatformToCommand(platformRead);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Не удалось отправить синхронно: {ex.Message}");
            }

            

            //Async
            try
            {
                var platformPublish = _mapper.Map<PlatformPublishedDto>(platformRead);
                platformPublish.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(platformPublish);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось отправить асинхронно: {ex.Message}");
            }




            return CreatedAtRoute(nameof(GetPlatformById), new {Id = platformRead.Id}, platformRead);
        }
    }
}
