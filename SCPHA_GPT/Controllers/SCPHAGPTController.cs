using Microsoft.AspNetCore.Mvc;

namespace SCPHA_GPT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SCPHAGPTController : ControllerBase
    {

        private readonly ILogger<SCPHAGPTController> _logger;

        public SCPHAGPTController(ILogger<SCPHAGPTController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetSCPHAGPT")]
        public IEnumerable<SCHPAGPT> Get()
        {
            return Enumerable.Empty<SCHPAGPT>();
        }
    }
}