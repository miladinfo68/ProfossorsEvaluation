using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class ProfessorInCollege
    {
        public List<Professor> ProfessorList { get; set; }
        public List<College> CollegeList { get; set; }
    }
}