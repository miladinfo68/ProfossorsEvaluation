using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Model.SyncModel
{
    public class EducationalClassSyncModel
    {
        public int? CodeClass { get; set; }
        public string Name { get; set; }
        public int? ProfessorId { get; set; }
        public string Term { get; set; }
        //public int ProfessorDelay { get; set; }
        //public int ProfessorEarlier { get; set; }

        public int? ProfessorDelayAndEarlier { get; set; }
        public DateTime? HoldingExamDate { get; set; }
        public string DeclaringScoreDate { get; set; }
        public DateTime? LoadingQuestionDate { get; set; }

        public int? OnlineHeldingCount { get; set; }
        public bool IsActive { get; set; }
        public int? GroupId { get; set; }
        
        public int? OthersHeldingCount { get; set; }
        public int? PresentHeldingCount { get; set; }
        public int? ContentType { get; set; }
        public decimal? HoldingType { get; set; }
        public DateTime? ReceiveExamPaperDate { get; set; }
        public DateTime? AggregationExamPaperDate { get; set; }
        public DateTime? LessonPlanSendDate { get; set; }
    }
}
