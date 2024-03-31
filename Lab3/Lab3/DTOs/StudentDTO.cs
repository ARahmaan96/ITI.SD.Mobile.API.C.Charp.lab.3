using Lab3.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3.DTOs
{
    public class StudentDTO
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public int? DeptId { get; set; }

        public string? Department { get; set; }

        public List<string>? Courses { get; set; }

        public StudentDTO() { }

        public StudentDTO(Student std)
        {
            Id = std.Id;
            Name = std.Name;
            DeptId = std.DeptId;
            Department = std?.Department?.Name ?? String.Empty;

            Courses = std?.Courses.Select(c => c.Name).ToList() ?? [];
        }

    }
}
