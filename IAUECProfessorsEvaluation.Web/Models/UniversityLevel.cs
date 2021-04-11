using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class UniversityLevel : BaseClass
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public Score Score { get; set; }
    }
}