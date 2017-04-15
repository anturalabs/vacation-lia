using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnturaSemester.Models
{
    public class Users
    {
        [Key]
        public int ID { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zåäöA-ZÅÄÖ''-'\s]*$")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-ZÅÄÖ]+[a-zåäöA-ZÅÄÖ''-'\s]*$")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }

        [Display(Name = "Role(s)")]
        public ICollection<UserRoles> UsersRole { get; set; }

        [Display(Name = "Department")]
        public ICollection<UserDepartment> UsersDepartment { get; set; }

        [Display(Name = "Team")]
        public ICollection<UserTeam> UsersTeam {get; set;}
    
    }
}

