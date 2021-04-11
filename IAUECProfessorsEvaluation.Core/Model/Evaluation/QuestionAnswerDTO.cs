using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Core.Model.Evaluation
{
    public class QuestionAnswerDTO
    {
        public int Id { get; set; }
        public int SubQuestionId { get; set; }
        public int AnswerId { get; set; }

    }
}
