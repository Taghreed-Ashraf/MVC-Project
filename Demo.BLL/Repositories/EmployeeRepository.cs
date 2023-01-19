using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCAppDbContext _dbContext;

        public EmployeeRepository(MVCAppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmpByName(string Name)
          => _dbContext.Employees.Where(E => E.Name.Contains(Name)).Include(E => E.Department);

        public IQueryable<Employee> GetEmpByDeptName(string deptName)
        {
            return null;
        }


    }
}
