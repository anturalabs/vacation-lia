using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnturaSemester.Models
{
    public class UserTeam
    {
        public int UserTeamID { get; set; }
        public int UsersID { get; set; }
        public int TeamID { get; set; }

        public Users User { get; set; }
        public Team Teams { get; set; }
    }
}
