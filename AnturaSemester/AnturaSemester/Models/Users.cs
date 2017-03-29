using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnturaSemester.Models
{
    public class Users
    {
        [Key]
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public ICollection<Roles> UserRole { get; set; }
        


    }
}

