using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class EvaluationQuestionAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        //public virtual EvaluationQuestion Question { get; set; }
        public int? SubQuestionId { get; set; }
        //public virtual EvaluationSubQuestion SubQuestion { get; set; }
        public int AnswerId { get; set; }
        //public virtual EvaluationAnswer Answer { get; set; }
        //public string Term { get; set; }
        public string StudentCode { get; set; }
        public int? ProfessorId { get; set; }
        public int? ClassId { get; set; }
        public int TermId { get; set; }
    }
}
