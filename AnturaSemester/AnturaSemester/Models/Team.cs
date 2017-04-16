using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnturaSemester.Models
{

    public class Team
    {
        [Key]
        public int ID { get; set; }
        public string TeamName { get; set; }
        

        
        public ICollection<UserTeam> UserTeams { get; set; }

        public Users Users { get; set; }
        
    }
}
