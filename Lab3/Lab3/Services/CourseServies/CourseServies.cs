using Lab3.Context;
using Lab3.DTOs;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Services.CourseServies
{
    public class CourseServies : ICourseServies
    {
        private CompanyContext _dbContext;
        public CourseServies(CompanyContext context)
        {
            _dbContext = context;
        }

        public List<CourseDTO> Get()
        {
            List<CourseDTO> Results = new();
            foreach (var crs in _dbContext.Courses.Include(C => C.Students))
                Results.Add(new CourseDTO(crs));

            return Results;
        }

        public CourseDTO? Get(int id)
        {
            Course? crs = _dbContext.Courses.Include(C => C.Students).FirstOrDefault(C => C.Id == id);

            if (crs == null)
                return null;

            return new CourseDTO(crs);
        }

        public CourseDTO? Post(CourseDTO course)
        {
            Course newCourse = new Course
            {
                Name = course?.Name ?? string.Empty,
            };

            _dbContext.Courses.Add(newCourse);
            _dbContext.SaveChanges();

            return new CourseDTO(newCourse);
        }

        public CourseDTO? Put(int id, CourseDTO course)
        {
            Course? existingCourse = _dbContext.Courses.FirstOrDefault(C => C.Id == id);

            if (existingCourse == null)
                return null;

            existingCourse.Name = course?.Name ?? string.Empty;

            _dbContext.SaveChanges();

            return new CourseDTO(existingCourse);
        }
        public CourseDTO? Delete(int id)
        {
            Course? deletedCourse = _dbContext.Courses.FirstOrDefault(C => C.Id == id);

            if (deletedCourse == null)
                return null;

            _dbContext.Courses.Remove(deletedCourse);
            _dbContext.SaveChanges();

            return new CourseDTO(deletedCourse);
        }
    }

}
