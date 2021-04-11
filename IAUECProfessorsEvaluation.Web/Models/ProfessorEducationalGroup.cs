using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class ProfessorEducationalGroup
    {
        public Professor Professor { get; set; }
        public EducationalGroup EducationalGroup { get; set; }
        public Term Term { get; set; }
    }
}