using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class EvaluationQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public int TermId { get; set; }
        public virtual Term Term { get; set; }
        public bool IsLastQuestion { get; set; }
        public int ParentId { get; set; }
        public bool IsPossibilityToInsertComment { get; set; }
        public int QuestionType { get; set; }
        public int EvaluationTypeId { get; set; }
        public virtual EvaluationType EvaluationType { get; set; }
        public int EvaluationChartTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
