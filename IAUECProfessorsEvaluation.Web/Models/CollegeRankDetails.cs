using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class CollegeRankDetails
    {
        public List<CollegeScore> CollegeScores { get; set; }
        public College CollegeInfo { get; set; }
    }
}