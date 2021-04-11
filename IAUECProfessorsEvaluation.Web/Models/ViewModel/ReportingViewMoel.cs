using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models.ViewModel
{
    public class ReportingViewMoel
    {
        public string CollegeList { get; set; }
        public bool AllCollege { get; set; }

        public string GroupList { get; set; }
        public bool AllGroup { get; set; }

        public string ProfessoreList { get; set; }
        public bool AllProfessore { get; set; }

        public List<IndicatorViewModel> IndicatorsForGroup { get; set; }
        public List<IndicatorViewModel> IndicatorsForProfessor { get; set; }

        public List<TermViewModel> Terms { get; set; }
        [Required]
        public int? TermId { get; set; }
        [Required]
        public int? ReportLevel { get; set; }

        public List<ReportLevle> ReportLevleType { get; set; }

        public bool AllGroupIndicator { get; set; }
        public bool AllProfessorIndictor { get; set; }

        public int? Ordering { get; set; }
        [Required]
        public int OrderingType { get; set; }
        public bool AllOrdering { get; set; }

        public List<SortingViewModel> SortingList { get; set; }
        public bool IsSelectedScorePartGroup { get; set; }
        public bool IsSelectedIndicatorPartGroup { get; set; }
        public bool IsSelectedScorePartProfessor { get; set; }
        public bool IsSelectedIndicatorPartProfessor { get; set; }
        public int CountShow { get; set; }

        public List<int> ProList { get; set; }
        public List<int> GrpList { get; set; }
        public List<int> ColList { get; set; }

    }
}