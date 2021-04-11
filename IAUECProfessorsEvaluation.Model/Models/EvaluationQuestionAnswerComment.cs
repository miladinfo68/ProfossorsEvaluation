using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class EvaluationQuestionAnswerComment
    {
        public int Id { get; set; }
        public int TermId { get; set; }
        public string PersonalCode { get; set; }
        public string AnswerComment { get; set; }
        public int QuestionId { get; set; }
    }
}
