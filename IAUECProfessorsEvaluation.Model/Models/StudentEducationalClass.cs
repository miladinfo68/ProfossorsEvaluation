using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class StudentEducationalClass : BaseClass
    {
        
        public EducationalClass EducationalClass { get; set; }
        public decimal? Grade { get; set; }
        public decimal? ProfessorEvaluationScore { get; set; }
        public Term Term { get; set; }
        public int? StudentId { get; set; }
    }
}