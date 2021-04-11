namespace IAUECProfessorsEvaluation.Data.ReportModel
{
    public class CollegeReportModel
    {
        public int? RowNumber { get; set; }
        public string CollegeId { get; set; }
        public string CollegeName { get; set; }
        public int IndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public int? AvgScore { get; set; }
        public int? CollegeScore { get; set; }
    }
}