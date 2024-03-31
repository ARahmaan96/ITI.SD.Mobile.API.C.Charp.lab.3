using Lab3.DTOs;
using Lab3.Services.CourseServies;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        ICourseServies _courseService;
        public CourseController(ICourseServies courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_courseService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            CourseDTO? std = _courseService.Get(id);

            if (std == null)
                return NotFound();

            return Ok(std);
        }

        [HttpPost]
        public IActionResult Post(CourseDTO Course)
        {
            CourseDTO? newCourse = _courseService.Post(Course);

            if (newCourse == null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = newCourse.Id }, newCourse);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, CourseDTO Course)
        {
            CourseDTO? existingCourse = _courseService.Put(id, Course);

            if (existingCourse == null)
                return NotFound();

            return Ok(existingCourse);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            CourseDTO? deletedCourse = _courseService.Delete(id);

            if (deletedCourse == null)
                return BadRequest();

            return Ok(deletedCourse);
        }
    }
}
