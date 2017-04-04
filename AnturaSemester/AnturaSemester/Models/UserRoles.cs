using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnturaSemester.Models
{
    public class UserRoles
    {
        public int UserRoleID { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
       
        public Users User { get; set; }
        public Roles Role { get; set; }

    }
}
