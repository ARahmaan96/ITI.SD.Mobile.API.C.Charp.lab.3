using Lab3.Models;

namespace Lab3.DTOs
{
    public class CourseDTO
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public List<string>? Students { get; set; }

        public CourseDTO() { }
        public CourseDTO(Course crs)
        {
            Id = crs.Id;
            Name = crs.Name;

            Students = crs?.Students.Select(S => S.Name).ToList() ?? [];
        }
    }
}
