using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace AnturaSemester.Models
{
    public class CalendarCell
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }

        public int UsersID { get; set; }
        public string AbsenceName { get; set; }
        public string Approval { get; set; }
        public Users User { get; set; }



    }
}
