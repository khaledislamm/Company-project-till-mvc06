﻿using Company.G02.DAL.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL.Interfaces
{
    public interface IDepratmentRepository : IGenericRepository<Department>
    {
       // IEnumerable<Department> GetAll();
       //Department? Get(int id);
       // int Add(Department model);
       // int Update(Department model);
       // int Delete(Department model);
    }
}
