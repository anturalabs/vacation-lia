using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnturaSemester.Models
{
    public class UserRoles
    {
        public int UserRolesID { get; set; }
        public int UsersID { get; set; }
        public int RolesID { get; set; }

        public Users User { get; set; }
        public Roles Role { get; set; }

    }
}
