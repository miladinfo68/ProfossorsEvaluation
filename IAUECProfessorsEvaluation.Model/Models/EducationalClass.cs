using System;
using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class EducationalClass : BaseClass
    {
        public int? CodeClass { get; set; }
        public string Name { get; set; }
        public Professor Professor { get; set; }
        public Term Term { get; set; }
        public virtual ICollection<StudentEducationalClass> StudentEducationalClasses { get; set; }
        //public int? ProfessorDelay { get; set; }
        //public int? ProfessorEarlier { get; set; }
        public int? ProfessorDelayAndEarlier { get; set; }
        public DateTime? HoldingExamDate { get; set; }
        public DateTime? DeclaringScoreDate { get; set; }
        public DateTime? LoadingQuestionDate { get; set; }
        public int? EvaluationScore { get; set; }
        public int? OnlineHeldingCount { get; set; }
        public int? PersentHeldingCount { get; set; }
        public int? OthersHeldingCount { get; set; }
        public EducationalGroup EducationalGroup { get; set; }
        public DateTime? ReceiveExamPaperDate { get; set; }
        public DateTime? AggregationExamPaperDate { get; set; }
        public DateTime? LessonPlanSendDate { get; set; }
        public int? ContentType { get; set; }
        public decimal? HoldingType { get; set; }



    }
}