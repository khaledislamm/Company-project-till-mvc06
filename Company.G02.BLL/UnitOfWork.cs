using Company.G02.BLL.Interfaces;
using Company.G02.BLL.Reposatiories;
using Company.G02.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;

        public IDepratmentRepository DepratmentRepository { get; } // NULL

        public IEmployeeRepository EmployeeRepository { get; } // NULL

        public UnitOfWork(CompanyDbContext context)
        {
            _context = context;
            DepratmentRepository = new DepartmentRepository(_context);
            EmployeeRepository = new EmployeeRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
