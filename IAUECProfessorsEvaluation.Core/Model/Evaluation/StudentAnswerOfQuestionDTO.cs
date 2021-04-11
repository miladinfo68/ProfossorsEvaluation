using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Core.Model.Evaluation
{
    public class StudentAnswerOfQuestionDTO
    {
        public int QuestionId { get; set; }
        public int SubQuestionId { get; set; }
        public int AnswerId { get; set; }
        public int UserId { get; set; }
        public string Term { get; set; }
    }

}
