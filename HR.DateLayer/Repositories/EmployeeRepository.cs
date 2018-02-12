using HR.DateLayer.DbLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.DateLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>
    {
        public EmployeeRepository(DbContext context) : base(context) { }
    }
    public class PhotoRepository : GenericRepository<Photo>
    {
        public PhotoRepository(DbContext context) : base(context) { }
    }
}
