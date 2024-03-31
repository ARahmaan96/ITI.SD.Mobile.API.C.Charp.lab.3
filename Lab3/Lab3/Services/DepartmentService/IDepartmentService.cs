using Lab3.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Services.DepartmentService
{
    public interface IDepartmentService
    {
        public List<DepartmentDTO> Get();
        public DepartmentDTO? Get(int id);
        public DepartmentDTO? Put(int id, DepartmentDTO department);
        public DepartmentDTO? Post(DepartmentDTO department);
        public DepartmentDTO? Delete(int id);
    }
}
