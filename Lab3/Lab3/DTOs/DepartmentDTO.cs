using Lab3.Models;

namespace Lab3.DTOs
{
    public class DepartmentDTO
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public List<String>? Students { get; set; }

        public DepartmentDTO() { }
        public DepartmentDTO(Department dept)
        {
            Id = dept.Id;
            Name = dept.Name;

            Students = dept.Students.Select(S => S.Name).ToList();
        }
    }
}
