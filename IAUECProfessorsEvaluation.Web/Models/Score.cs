using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class Score : BaseClass
    {
        public string Name { get; set; }

        public float? MaxValue { get; set; }
        public float? MinValue { get; set; }
        //public float? UpperBound { get; set; }
        //public float? LowerBound { get; set; }
        public float Point { get; set; }

        public virtual Indicator Indicator { get; set; }
    }
}