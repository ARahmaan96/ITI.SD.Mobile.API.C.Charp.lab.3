using Lab3.DTOs;
using Lab3.Models;
using Lab3.Services.StudentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_studentService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            StudentDTO? std = _studentService.Get(id);

            if (std == null)
                return NotFound();

            return Ok(std);
        }

        [HttpPost]
        public IActionResult Post(StudentDTO student)
        {
            StudentDTO? newStudent = _studentService.Post(student);

            if (newStudent == null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = newStudent.Id }, newStudent);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, StudentDTO student)
        {
            StudentDTO? existingStudent = _studentService.Put(id, student);

            if (existingStudent == null)
                return NotFound();

            return Ok(existingStudent);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            StudentDTO? deletedStudent = _studentService.Delete(id);

            if (deletedStudent == null)
                return BadRequest();

            return Ok(deletedStudent);
        }
    }
}
