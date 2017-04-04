using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnturaSemester.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }

        [Display(Name = "Role(s)")]
        public ICollection<UserRoles> UsersRole { get; set; }

        //[Display(Name = "Department")]
        //public ICollection<Department> UsersDepartment { get; set; }

        //[Display(Name = "Team")]
        //public ICollection<Team> UsersTeam {get; set;}
    
    }
}

