using Lab3.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Services.CourseServies
{
    public interface ICourseServies
    {
        public List<CourseDTO> Get();
        public CourseDTO? Get(int id);
        public CourseDTO? Put(int id, CourseDTO course);
        public CourseDTO? Post(CourseDTO course);
        public CourseDTO? Delete(int id);
    }
}
