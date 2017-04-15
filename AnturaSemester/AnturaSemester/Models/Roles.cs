using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnturaSemester.Models
{
    public class Roles

    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Role")]
        public string RoleName { get; set; }
       
        public ICollection<UserRoles> UsersRoles { get; set; }
    }
    
}
