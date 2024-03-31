using Lab3.Context;
using Lab3.DTOs;
using Lab3.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private CompanyContext _dbContext;
        public DepartmentService(CompanyContext context)
        {
            _dbContext = context;
        }

        public List<DepartmentDTO> Get()
        {
            List<DepartmentDTO> Results = new();
            foreach (var dept in _dbContext.Departments.Include(D => D.Students))
                Results.Add(new DepartmentDTO(dept));
            return Results;
        }

        public DepartmentDTO? Get(int id)
        {
            Department? dept = _dbContext.Departments.Include(D => D.Students).FirstOrDefault(D => D.Id == id);

            if (dept == null)
                return null;

            return new DepartmentDTO(dept);
        }

        public DepartmentDTO? Post(DepartmentDTO department)
        {
            Department newDepartment = new Department
            {
                Name = department?.Name ?? string.Empty,
            };

            _dbContext.Departments.Add(newDepartment);
            _dbContext.SaveChanges();

            return new DepartmentDTO(newDepartment);
        }

        public DepartmentDTO? Put(int id, DepartmentDTO department)
        {
            Department? existingDepartment = _dbContext.Departments.FirstOrDefault(D => D.Id == id);

            if (existingDepartment == null)
                return null;

            existingDepartment.Name = department?.Name ?? string.Empty;

            _dbContext.SaveChanges();

            return new DepartmentDTO(existingDepartment);
        }

        public DepartmentDTO? Delete(int id)
        {
            Department? deletedDepartment = _dbContext.Departments.FirstOrDefault(D => D.Id == id);

            if (deletedDepartment == null)
                return null;

            _dbContext.Departments.Remove(deletedDepartment);
            _dbContext.SaveChanges();

            return new DepartmentDTO(deletedDepartment);
        }
    }
}
