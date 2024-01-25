using CourseApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        static List<Student> list = null;
        public StudentController() {
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
        public List<Student> Get()
        {
                return list;    
          }

        [HttpGet]
        [Route("{id}")]
        public  Student GetById(int id)
        {
            return list.FirstOrDefault(x => x.Id == id); ;
        }
        [HttpPost]
        public void AddRecord(Student student)
        {
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
