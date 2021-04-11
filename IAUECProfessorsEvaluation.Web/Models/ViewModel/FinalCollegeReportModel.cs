using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAUECProfessorsEvaluation.Data.ReportModel;

namespace IAUECProfessorsEvaluation.Web.Models.ViewModel
{
    public class FinalCollegeReportModel
    {
        public List<GeneralReportModel> Generalresualt { get; set; }
        public List<CollegeReportModel> OrderedCollegeresualt { get; set; }
    }
    public class FinalGroupReportModel
    {
        public List<GeneralReportModel> Generalresualt { get; set; }
        public List<GroupReportModel> OrderedGroupResualt { get; set; }
    }
    public class FinalProfessorReportModel
    {
        public List<GeneralReportModel> Generalresualt { get; set; }
        public List<ProfessorReportModel> OrderedProfessorResualt { get; set; }
    }
}