using AngularWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AngularWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly AppDBContext appDBContext;

        public ConnectionController(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        [HttpGet("establish-dbconnection")]
        public ActionResult<string> EstablishConnection()
        {
            int retryCount = 0;
            Console.WriteLine("Establishing Database Connection....");
            while (!appDBContext.Database.CanConnect() && retryCount < 5)
            {
                retryCount += 1;
                Console.WriteLine(retryCount);
            }
            if (retryCount >= 5)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Connecting to DB");
            else
            {
                Console.WriteLine("\nEstablished Database Connection Successfully");
                return StatusCode(StatusCodes.Status202Accepted, JsonSerializer.Serialize("Connected to DB and API"));
            }
        }
    }
}
