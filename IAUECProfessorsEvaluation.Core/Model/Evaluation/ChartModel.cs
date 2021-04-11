using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Core.Model.Evaluation
{
    public class ChartModel
    {
        
        public ChartQuestionModel Question { get; set; }
        public List<ChartSubQuestionModel> SubQuestions { get; set; }
        public List<ChartAnswer> Answers { get; set; }
        public List<AnswerPercentage> AnswerPercentage { get; set; }
    }
    public class ChartQuestionModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ParentId { get; set; }
        public string ParentQuestionText { get; set; }
        public int EvaluationChartTypeId { get; set; }
    }
    public class ChartSubQuestionModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    public class ChartAnswer
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    public class AnswerPercentage
    {
        public string Value { get; set; }
        public double Item { get; set; }
      
    }
}
