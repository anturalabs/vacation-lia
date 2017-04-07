using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnturaSemester.Models
{
    public class Department
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        //public Users Users { get; set; }

        public Departments? UserDepartment { get; set; }

        public ICollection<Users> Users { get; set; }

        public enum Departments { ProductDevelopment, Marketing, Consulting };
        
    }
}
