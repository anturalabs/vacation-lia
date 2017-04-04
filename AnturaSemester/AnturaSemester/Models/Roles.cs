using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnturaSemester.Models
{
    //public enum Role { Tester, Admin, Developer, ProjectManager };

    public class Roles

    {
        [Key]
        public int RoleID { get; set; }
        public Role? UserRole { get; set; }
        //public ICollection<Roles> UsersRole { get; set; }
        public enum Role { Tester, Admin, Developer, ProjectManager };
        
       // public int UserID { get; set; }
       // [ForeignKey("UserID")]
       // public Users Users { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }

    }
    
}
