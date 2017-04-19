using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnturaSemester.Models
{
    public class SemesterViewModel
    {
        public int ID { get; set; }
        public string AbsenceName { get; set; }
        public enum AbsenceType { Holiday, VAB, Sick, Workrelated }
        public string Approval { get; set; }
        public enum ApprovalState { Approved, Denied, Ongoing }
        public bool RepeatChosen { get; set; }
    }
}
