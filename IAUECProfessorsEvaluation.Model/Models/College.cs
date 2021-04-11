using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class College : BaseClass
    {

        public int? CollegeCode { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EducationalGroup> EducationalGroups { get; set; }
        public virtual ICollection<CollegeScore> CollegeScores { get; set; }


    }
}