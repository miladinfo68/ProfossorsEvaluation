using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class EducationalGroupRankDetails
    {
        public List<EducationalGroupScore> EducationalGroupScores { get; set; }
        public EducationalGroup EducationalGroupInfo { get; set; }
    }
}