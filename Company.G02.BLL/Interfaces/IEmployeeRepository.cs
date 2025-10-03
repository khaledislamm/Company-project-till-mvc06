using Company.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {

        Task<List<Employee>> GetByNameAsync(string name);

        //Employee? GetByEmail(string Names);
        //IEnumerable<Employee> GetAll();
        //Employee? Get(int id);
        //int Add(Employee model);
        //int Update(Employee model);
        //int Delete(Employee model);
    }
}
