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
        public enum AbsenceType { Holiday, VAB, Sick, ParentalLeave }
       // public string CommentField { get; set; }
        public string Approval { get; set; }
        public enum ApprovalState { Approved, Denied, Ongoing }
        public Users User { get; set; }

    }
}
