﻿using CourseApi.Filters;
using CourseApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[CustomAuthorizationFilter]
    public class StudentIActionController : ControllerBase
    {
        static List<Student> list = null;
        public StudentIActionController() {
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
        public IActionResult Get()
        {
            if (list.Count == 0)
                return NotFound();
            else
                return Ok(list);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var obj = list.FirstOrDefault(x => x.Id == id); ;
            if (obj == null)
                return NotFound();
            else
                return Ok(obj);
        }
        [HttpPost]
        [Authorize(Roles="Admin")]
        public IActionResult AddRecord(Student student)
        {
            list.Add(student);
            return Created("Added", student);
        }

        [HttpPut]
        [Route("{id}")]
        public void EditStudent(int id, Student student)
        {

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
