using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models.ReportModel
{
    public class CollegeReportModel
    {
        public int? RowNumber { get; set; }
        public string CollegeName { get; set; }
        public int? TotalScore { get; set; }
    }
}