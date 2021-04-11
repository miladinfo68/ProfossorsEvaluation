using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Core.Model.Evaluation
{
    public class EvaluationQuestionViewModel
    {
        public int Id { get; set; }
        public int TermId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string TermText { get; set; }
    }
}
