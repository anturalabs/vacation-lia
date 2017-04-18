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
        [Key]
        public int ID { get; set; }
        [Display(Name = "Department's Name")]
        public string DepartmentName { get; set; }
        

        public ICollection<UserDepartment> UserDepartments { get; set; }

    }
}
