using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnturaSemester.Models
{
    public class UserDepartment
    {
        public int UserDepartmentID { get; set; }
        public int UsersID { get; set; }
        public int DepartmentID { get; set; }

        public Users User { get; set; }
        public Department Departments { get; set; }
    }
}
