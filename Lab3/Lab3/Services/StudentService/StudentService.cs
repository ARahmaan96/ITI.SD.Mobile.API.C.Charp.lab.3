using Lab3.Context;
using Lab3.DTOs;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private CompanyContext _dbContext;
        public StudentService(CompanyContext context)
        {
            _dbContext = context;
        }

        public List<StudentDTO> Get()
        {
            List<StudentDTO> Results = new();
            foreach (var std in _dbContext.Students.Include(S => S.Department).Include(S => S.Courses))
                Results.Add(new StudentDTO(std));
            return Results;
        }

        public StudentDTO? Get(int id)
        {
            Student? std = _dbContext.Students.Include(S => S.Department).Include(S => S.Courses).FirstOrDefault(S => S.Id == id);

            if (std == null)
                return null;

            return new StudentDTO(std);
        }

        public StudentDTO? Post(StudentDTO student)
        {
            Student newStudent = new Student
            {
                Name = student?.Name ?? string.Empty,
                DeptId = student?.DeptId
            };

            _dbContext.Students.Add(newStudent);
            _dbContext.SaveChanges();

            return new StudentDTO(newStudent);
        }

        public StudentDTO? Put(int id, StudentDTO student)
        {
            Student? existingStudent = _dbContext.Students.FirstOrDefault(s => s.Id == id);

            if (existingStudent == null)
                return null;

            existingStudent.Name = student?.Name ?? string.Empty;
            existingStudent.DeptId = student?.DeptId ?? 0;

            _dbContext.SaveChanges();

            return new StudentDTO(existingStudent);
        }

        public StudentDTO? Delete(int id)
        {
            Student? deletedStudent = _dbContext.Students.FirstOrDefault(s => s.Id == id);

            if (deletedStudent == null)
                return null;

            _dbContext.Students.Remove(deletedStudent);
            _dbContext.SaveChanges();

            return new StudentDTO(deletedStudent);
        }
    }
}
