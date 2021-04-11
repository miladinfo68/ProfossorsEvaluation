using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Core.Model.Evaluation
{
    public class SubQuestionDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
    }
}
