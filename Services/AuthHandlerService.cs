using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public class AuthHandlerService
    {
        private DimensionDataAPIContext _context;

        public AuthHandlerService(DimensionDataAPIContext context)
        {
            _context = context;
        }

        public bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }
        public bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

    }
}
