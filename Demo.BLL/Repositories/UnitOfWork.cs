using Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository _EmployeeRepository { get; set; }
        public IDepartmentRepository _DepartmentRepository { get; set; }

        public UnitOfWork(IEmployeeRepository EmployeeRepository, IDepartmentRepository DepartmentRepository)
        {
            _DepartmentRepository = DepartmentRepository;
            _EmployeeRepository = EmployeeRepository;
        }

    }
}
