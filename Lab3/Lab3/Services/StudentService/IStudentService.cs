using Lab3.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Services.StudentService
{
    public interface IStudentService
    {
        public List<StudentDTO> Get();
        public StudentDTO? Get(int id);
        public StudentDTO? Put(int id, StudentDTO student);
        public StudentDTO? Post(StudentDTO student);
        public StudentDTO? Delete(int id);
    }
}
