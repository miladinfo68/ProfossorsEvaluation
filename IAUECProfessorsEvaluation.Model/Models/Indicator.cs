using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class Indicator : BaseClass
    {
        public string Name { get; set; }

        public virtual Ratio Ratio { get; set; }
        public virtual ObjectType ObjectType { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
        public string CountOfType { get; set; }
    }
}