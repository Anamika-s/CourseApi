using CourseApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentActionController : ControllerBase
    {
        static List<Student> list = null;
        public StudentActionController()
        {
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
        [HttpGet]
        public ActionResult<List<Student>> Get()
        {
            if (list.Count == 0)
                return NotFound();
            else
                return Ok(list);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Student> GetById(int id)
        {
            var obj = list.FirstOrDefault(x => x.Id == id); ;
            if (obj == null)
                return null;
            else
                return Ok(obj);
        }
        [HttpPost]
        public ActionResult<string> AddRecord(Student student)
        {
            list.Add(student);
            //return Created("Added", student);
            return "REcord added";
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult EditStudent(int id, Student student)
        {
            var obj = GetById(id);
            if (obj != null)
            {
                foreach (var temp in list)
                {
                    if (temp.Id == id)
                    {
                        temp.Name = student.Name;
                        temp.Marks = student.Marks;
                    }
                }
                return Ok();
            }
            else
                return NotFound();
                //list.Remove(obj);
                
            }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Edit(int id, JsonPatchDocument<Student> patchDocument)
        {
            if (patchDocument == null || id < 1) return BadRequest();
            Student obj1 = list.FirstOrDefault(x => x.Id == id);
            if (obj1 == null) return NotFound();
            var student = new Student
            {
                Id = obj1.Id,
                Name = obj1.Name,
                Marks = obj1.Marks

            };
            
                patchDocument.ApplyTo(student, ModelState);
                if (!ModelState.IsValid) return BadRequest();
                else
                {  obj1.Name = student.Name;
                    obj1.Marks = student.Marks;
                    return Ok();
                }
            
             //list.Remove(obj);

        }



        [HttpDelete("{id}")]

        public IActionResult DeleteStduent(int id)
        {
            var obj = GetById(id);
            if (obj != null)
            {
                //list.Remove(obj);
                return Ok();
            }
            else
                return NotFound();
        }



    }

}
