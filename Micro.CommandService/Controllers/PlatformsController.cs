using AutoMapper;
using Micro.CommandService.Data;
using Microsoft.AspNetCore.Mvc;

namespace Micro.CommandService.Controllers
{
    [ApiController]
    [Route("api/c/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommandRepo _repo;

        public PlatformsController(ICommandRepo repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("TestCommand # Command Service");

            return Ok("OK");
        }
        [HttpGet]
        public async Task<ActionResult> GetPlatforms()
        {
            Console.WriteLine("Получаем платформы");


            var platforms = await _repo.GetAllPlatforms();

            return Ok(platforms);
        }
    }
}
