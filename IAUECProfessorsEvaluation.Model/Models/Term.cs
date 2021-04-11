using System;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class Term : BaseClass
    {
        public string Name { get; set; }
        public string TermCode { get; set; }
        public bool IsCurrentTerm { get; set; }
        public DateTime? ExamStartDate { get; set; }
        public DateTime? ExamEndDate { get; set; }
    }
}