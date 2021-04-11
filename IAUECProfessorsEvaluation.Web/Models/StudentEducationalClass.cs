using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class StudentEducationalClass : BaseClass
    {
        public EducationalClass EducationalClass { get; set; }
        public int? Grade { get; set; }
        public int? ProfessorEvaluationScore { get; set; }
        public Term Term { get; set; }
        public int? StudentId { get; set; }
    }
}