using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnturaSemester.Models
{
    

    public class Roles

    {
        [Key]
        public Role UserRole { get; set; }
        public int UserID { get; set; }
        public enum Role { Tester, Admin, Programmer, ProjectManager};
        

       
        public Users Users { get; set; }
    }
}
