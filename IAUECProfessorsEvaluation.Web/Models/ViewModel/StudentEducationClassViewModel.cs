using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models.ViewModel
{
    public class StudentEducationClassViewModel
    {
        public string stcode { get; set; }
        public decimal EducationClassId { get; set; }
        public decimal ProfessorId { get; set; }
        public string ProfessorFirstName { get; set; }
        public string ProfessorLastName { get; set; }
        public string EducationClassName { get; set; }
        public string ProfessorFullName => ProfessorFirstName + " " + ProfessorLastName;
    }
}