using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class EvaluationAnswer 
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public int Order { get; set; }
        public EvaluationQuestion Question { get; set; }
    }
}
