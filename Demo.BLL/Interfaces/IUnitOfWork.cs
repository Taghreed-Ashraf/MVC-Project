using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository _EmployeeRepository { get; set; }

        public IDepartmentRepository _DepartmentRepository { get; set; }
    }
}
