namespace Lab3.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    }
}
