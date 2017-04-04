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
        public int UsersID { get; set; }
        public int ID { get; set; }
        //public Team UserTeams { get; set; }
       
        public enum TeamEnum { ATeam, GoldTeam, Wombats}
        public ICollection<Team> UserTeams { get; set; }

        public Users Users { get; set; }
        public TeamEnum? UsersTeams { get; set; }
    }
}
