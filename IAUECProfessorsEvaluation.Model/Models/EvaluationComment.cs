using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class EvaluationComment
    {
        public int Id { get; set; }
        public string  Comment { get; set; }
        public string Term { get; set; }
        public string StudentCode { get; set; }
    }
}
