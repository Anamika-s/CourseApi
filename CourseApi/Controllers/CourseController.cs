using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        ILogger<CourseController> _logger;
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
  ;                return list;
            }
            else
            {
                _logger.LogInformation("info");
                //_logger.LogCritical("crtical");
                //_logger.LogError("ERror");
                return new List<string>()
                {
                "abc", "def","zyz"
            };

            }

        }
    }
}
