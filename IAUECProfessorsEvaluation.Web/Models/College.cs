using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class College : BaseClass
    {
        public int? CollegeCode { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EducationalGroup> EducationalGroups { get; set; }
        public virtual ICollection<CollegeScore> CollegeScores { get; set; }

        public virtual int TotalScores
        {
            get
            {
                var total = 0;
                total += this.CollegeScores.Sum(s => s.CurrentScore);
                total += this.EducationalGroups.SelectMany(s => s.EducationalGroupScores).Sum(s => s.CurrentScore);
                total += (int)this.EducationalGroups.SelectMany(s => s.EducationalClasses).Select(s=> s.Professor)
                    .SelectMany(s => s.ProfessorScores).Sum(s => s.CurrentScore);
                //total += this.EducationalGroups.Sum(x => x.EducationalGroupScores.Sum(xx => xx.CurrentScore));
                //total += (int)this.EducationalGroups.Sum(ss => ss.EducationalClasses.Select(sm=> sm.Professor).Sum(sss => sss.ProfessorScores.Sum(ssss => ssss.CurrentScore)));

                //foreach (var group in this.EducationalGroups)
                //{
                //    total += group.EducationalGroupScores.Sum(s => s.CurrentScore);
                //    foreach (var professor in group.Professors)
                //        total += professor.ProfessorScores.Sum(s => s.CurrentScore);
                //}
                return total;
            }
        }
        public virtual int RankInUniversity { get; set; }
    }
}