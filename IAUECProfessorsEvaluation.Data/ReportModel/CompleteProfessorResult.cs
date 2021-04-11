using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Data.ReportModel
{
    public class CompleteProfessorResult
    {
        public List<ProfessorDetialReportModel> General { get; set; }
        public List<ProfessorDetialReportModel> Detial { get; set; }
    }
}