using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class IndexModel
    {
        public string topProfessorName1 { get; set; }
        public string topProfessorName2 { get; set; }
        public string topProfessorName3 { get; set; }
        public int professorCountInRank1 { get; set; }
        public int professorCountInRank2 { get; set; }
        public int professorCountInRank3 { get; set; }
        public int professorCountInRank4 { get; set; }
        public int professorCountInRank5 { get; set; }


        public string topEducationalGroupName1 { get; set; }
        public string topEducationalGroupName2 { get; set; }
        public string topEducationalGroupName3 { get; set; }
        public int educationalGroupCountInRank1 { get; set; }
        public int educationalGroupCountInRank2 { get; set; }
        public int educationalGroupCountInRank3 { get; set; }
        public int educationalGroupCountInRank4 { get; set; }
        public int educationalGroupCountInRank5 { get; set; }

        public string topCollegeName1 { get; set; }
        public string topCollegeName2 { get; set; }
        public string topCollegeName3 { get; set; }
        public int collegeCountInRank1 { get; set; }
        public int collegeCountInRank2 { get; set; }
        public int collegeCountInRank3 { get; set; }
        public int collegeCountInRank4 { get; set; }
        public int collegeCountInRank5 { get; set; }

    }
}