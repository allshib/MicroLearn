using AutoMapper;
using Micro.PlaformService.Data;
using Micro.PlaformService.Dtos;
using Micro.PlaformService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Micro.PlaformService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : Controller
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
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
        public ActionResult<PlatformCreateDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        { 
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repository.CreatePlaform(platformModel);

            if (!_repository.SaveChanges())
                this.BadRequest();

            var platformRead = _mapper.Map<PlatformReadDto>(platformModel);
            return CreatedAtRoute(nameof(GetPlatformById), new {Id = platformRead.Id}, platformRead);
        }
    }
}
