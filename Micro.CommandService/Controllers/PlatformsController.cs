using Microsoft.AspNetCore.Mvc;

namespace Micro.CommandService.Controllers
{
    [ApiController]
    [Route("api/c/[controller]")]
    public class PlatformsController : ControllerBase
    {

        public PlatformsController()
        {
            
        }
        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("TestCommand # Command Service");

            return Ok("OK");
        }
    }
}
