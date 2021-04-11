using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Data.ReportModel
{
    //public class ReportParameter
    //{
    //    public int? TermId { get; set; }
    //    public int[] ProfessorIndicatorList { get; set; }
    //    public List<int> CollegeList { get; set; }
    //    public List<int> GroupList { get; set; }
    //    public string AllColleges { get; set; }
    //    public string AllGroups { get; set; }
    //    public string AllProfessorIndicators { get; set; }

    //}
    public class ProfessorDetialReportModel
    {
        public int? RowNumber { get; set; }
        public int ProfessorId { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public string ProfessorCode { get; set; }
        public int IndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public int? AvgScore { get; set; }
        public int? ProfessorScore { get; set; }
        public int RationValue { get; set; }
        public string RationName { get; set; }
        public bool Gender { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public List<string> FlawIndicatiors { get; set; }
        public int? ScoreId { get; set; }
    }




    public class ProfessorReportModel
    {
        public int? RowNumber { get; set; }
        public string CollegeId { get; set; }
        public string CollegeName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int ProfessorId { get; set; }
        public string ProfessorName { get; set; }
        public string ProfessorLastName { get; set; }
        public string ProfessorFullName { get; set; }
        public int IndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public string ScoreName { get; set; }
        public string ScoreId { get; set; }
        public int? CurrentScore { get; set; }
        public int? ProfessorScore { get; set; }
        public int? AvgScore { get; set; }

       
    }
}