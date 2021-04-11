using IAUECProfessorsEvaluation.Model.Models;
using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Data.ReportModel
{
    public class GroupReportModel
    {
        public int? RowNumber { get; set; }
        public int CollegeId { get; set; }
        public int CollegeCode { get; set; }
        public string CollegeName { get; set; }
        public int CollegeScore { get; set; }
        public int GroupId { get; set; }
        public int GroupCode { get; set; }
        public string GroupName { get; set; }
        public int IndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public int? CurrentScore { get; set; }
        public int? GroupScore { get; set; }
        public int? AvgProfessorScoreGroup { get; set; }
        public int CountShow { get; set; }
        public int RatioValue { get; set; }
        public string RatioName { get; set; }
        public List<string> FlawIndicatiors { get; set; }
        public int? ScoreId { get; set; }
        public string GroupManagerFirstName { get; set; }
        public string GroupManagerLastName { get; set; }
    }
}