using CourseApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        //private readonly IMemoryCache _memoryCache;
        static List<Student> list = null;


        public StudentController(/*IMemoryCache memoryCache*/)
        {
            //_memoryCache = memoryCache;

            if (list == null)
            {
                list = new List<Student>()
                {
                    new Student() { Id = 1, Name = "Ajay", Marks = 90 },

                    new Student() { Id = 2, Name = "Ajay", Marks = 90 },

                    new Student() { Id = 3, Name = "Ajay", Marks = 90 },

                    new Student() { Id = 4, Name = "Ajay", Marks = 90 }
                };
            }
        }


        //[HttpGet]
        //public List<Student> Get()
        //{
        //    var _log4net = log4net.LogManager.GetLogger(typeof(StudentController));
        //    _log4net.Error(list.Count);
        //    return list;
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var cacheKey = "employeeList";
            //checks if cache entries exists
            //if (!_memoryCache.TryGetValue(cacheKey, out List<Student> employeeList))
            {
                //calling the server
                //employeeList = await _context.Employees.ToListAsync();
                var employeeList = list.ToList();
                //setting up cache options
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };
                //setting cache entries
                //_memoryCache.Set(cacheKey, employeeList, cacheExpiryOptions);
            }
            

            return Ok(list.ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public Student GetById(int id)
        {
            return list.FirstOrDefault(x => x.Id == id); ;
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public void AddRecord(Student student)
        {
            var _log4net = log4net.LogManager.GetLogger(typeof(StudentController));
            _log4net.Info("New Record has been added");
            list.Add(student);
        }

        [HttpPut]
        [Route("{id}")]
        public void EditStudent(int id, Student student)
        {

        }

        [HttpDelete("{id}")]

        public void DeleteStduent(int id)
        {
            var obj = GetById(id);
            if (obj != null)

                list.Remove(obj);
        }


    }
}
