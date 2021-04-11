using IAUECProfessorsEvaluation.Core.Model.Evaluation;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Service.IService
{
    public interface IEvaluationQuestionService : IBaseService<EvaluationQuestion>
    {
        List<QuestionDTO> GetQuestionsTest(int typeId);
        List<DescriptiveQuestionModel> GetDescriptiveQuestions(int typeId);
        bool IsQuestionExist(int typeId);

        bool IsUserevaluated(string studentCode);

        bool InsertStudentAnswers(List<EvaluationQuestionAnswer> model);
        void InsertStudentComment(string comment, string studentCode, string termId);
        bool IsUserEvaluateProfessor(int professorId, int classId, string studentCode);
        bool IsProfessorQuestionExist(int professorId, int classId);
        int GetCurrentTermId();
        List<ChartModel> GetChartListModel(int? professorId, int? classId, int evaluationTypeId, int termId, string startDate, string endDate);
        int GetProfessorScore(int professorId, int classId, int termId, string startDate, string endDate);
        int GetScoreCount(int termId, string startDate, string endDate);
        bool SaveQuestionFor(int fromTypeId, int fromTermId, int toTypeId, int totermId);
        bool InsertanswerQuestioncomment(List<EvaluationQuestionAnswerComment> answerQuestionComment);
        void ChangeLastQuestion(Term term, EvaluationType type);
        bool IsQuestionExistByTermId(int termId, int evaluationTypeId);
        bool IsUserEvaluated(string stCode, int termId, int evaluationTypeId);
        IEnumerable<EvaluationQuestionViewModel> GetAllQuestions(int termId, int typeId, string startDate, string endDate);
    }
}
