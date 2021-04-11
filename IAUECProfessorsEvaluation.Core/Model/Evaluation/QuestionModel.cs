using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Core.Model.Evaluation
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int ParentId { get; set; }
        public string QuestionTitle { get; set; }
        public string AnswerTitle { get; set; }
        public bool IsLastQuestion { get; set; }
    }
}
