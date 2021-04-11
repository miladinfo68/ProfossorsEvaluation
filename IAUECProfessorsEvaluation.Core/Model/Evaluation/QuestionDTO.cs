using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Core.Model.Evaluation
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Term { get; set; }
        public string TermId { get; set; }
        public bool IsLastQuestion { get; set; }
        public int ParentId { get; set; }
        public bool IsPossibilityToInsertComment { get; set; }
        public List<AnswerOfQuestionDTO> AnswerOfQuestions { get; set; }
        public List<SubQuestionDTO> SubQuestionDTOs { get; set; }
    }
    
    public static class ExtchengeModel
    {
        public static SubQuestionDTO ToModel(this QuestionDTO model)
        {
            return new SubQuestionDTO
            {
                Id = model.Id,
                QuestionId = model.ParentId,
                Text = model.Text
            };
        }
        
    }
}
