using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        //private static Logger logger = LogManager.GetLogger("CourseController");

        private ILogger<CourseController> _logger;
        //ILogger<CourseController> _logger;
        public CourseController(ILogger<CourseController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<string> Get()
        {
            List<string> list = new List<string>();
            if (list.Count == 0)
            {
                _logger.LogError("List is e,p;lty");
                //list = new List<string>()
                ; return list;
            }
            else
            {
                _logger.LogInformation("info");
                _logger.LogCritical("crtical");
                //_logger.LogError("ERror");
                return new List<string>()
                {
                "abc", "def","zyz"
            };

            }

        }
    }
}
