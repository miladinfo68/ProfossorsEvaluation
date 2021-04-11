using IAUECProfessorsEvaluation.Core.Model.Evaluation;
using IAUECProfessorsEvaluation.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public static class ExtensionModel
    {
        #region HelperMethod
        private static DateTime ToGregorianDate(this string persionDate)
        {
            if (string.IsNullOrEmpty(persionDate))
                return DateTime.Now;
            var date = persionDate.Split('/');
            var y = Convert.ToInt32(date[0]);
            var m = Convert.ToInt32(date[1]);
            var d = Convert.ToInt32(date[2]);
            PersianCalendar pc = new PersianCalendar();
            DateTime dt = new DateTime(y, m, d, pc);
            return dt;
        }
        private static string ToPersionDate(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return $"{pc.GetYear(date)}/{pc.GetMonth(date)}/{pc.GetDayOfMonth(date)}";
        }
        #endregion

        #region EvaluationQuestion
        public static EvaluationQuestion ToModel(this EvaluationQuestionModel model, Model.Models.Term term)
        {
            if (model == null)
                return null;
            return new EvaluationQuestion
            {
                Text = model.Title,
                Term = term
            };
        }
        public static EvaluationQuestionModel ToModel(this EvaluationQuestion model)
        {
            if (model == null)
                return null;
            return new EvaluationQuestionModel
            {
                Title = model.Text,
                TermId = model.Term?.Id ?? 0,
                Id = model.Id,
                IsLastQuestion = model.IsLastQuestion,
                TermText = model.Term?.Name,
                TypeTitle = model.EvaluationType?.Title,
                TypeId = model.EvaluationType?.Id ?? 0,
                ChartId = model.EvaluationChartTypeId,
                QuestionType = model.QuestionType,
                IsPossibilityToInsertComment = model.IsPossibilityToInsertComment,
                StartDate = model.StartDate.ToPersionDate(),
                EndDate = model.EndDate.ToPersionDate()

            };
        }
        public static EvaluationQuestionModel ToModel(this EvaluationQuestionViewModel model)
        {
            if (model == null)
                return null;
            return new EvaluationQuestionModel
            {
                Title = model.Text,
                TermId = model.TermId,
                Id = model.Id,
                TermText = model.TermText,
                TypeTitle = model.Title,
            };
        }
        public static EvaluationQuestion ToModel(this string text, Model.Models.Term term, EvaluationType evaluationType, int parentId, bool isLastQuestion, int ChartId, bool isPossibilityToInsertComment, int questionType, string startDate = null, string endDate = null)
        {
            if (text == null && term == null)
                return null;
            return new EvaluationQuestion
            {
                Text = text,
                Term = term,
                IsLastQuestion = isLastQuestion,
                ParentId = parentId,
                EvaluationType = evaluationType,
                EvaluationTypeId = evaluationType.Id,
                TermId = term.Id,
                EvaluationChartTypeId = ChartId,
                QuestionType = questionType,
                IsPossibilityToInsertComment = isPossibilityToInsertComment,
                StartDate = startDate.ToGregorianDate(),
                EndDate = endDate.ToGregorianDate()
            };
        }

        public static EvaluationQuestion ToModel(this EvaluationQuestion questionModel, QuestionViewModel model, Model.Models.Term term, EvaluationType type)
        {
            if (questionModel == null || model == null)
                return null;
            questionModel.Text = model.question;
            questionModel.Term = term;
            questionModel.EvaluationType = type;
            questionModel.IsLastQuestion = model.isLastQuestion;
            questionModel.EvaluationChartTypeId = model.ChartId;
            questionModel.IsPossibilityToInsertComment = model.isPossibilityToInsertComment;
            questionModel.QuestionType = model.QuestionType;
            questionModel.StartDate = model.StartDate.ToGregorianDate();
            questionModel.EndDate = model.EndDate.ToGregorianDate();
            return questionModel;
        }
        #endregion

        #region EvaluationAnswer
        public static EvaluationAnswerModel ToModel(this EvaluationAnswer model)
        {
            if (model == null)
                return null;
            return new EvaluationAnswerModel
            {
                Id = model.Id,
                Title = model.Text,
                Score = model.Score,
                Order = model.Order
            };
        }
        public static EvaluationAnswer ToModel(this string title, EvaluationQuestion question)
        {
            if (title == null || title == "-")
                return null;
            return new EvaluationAnswer
            {
                Text = title.Split('-')[0],
                Score = title.Split('-')[1] != "" ? Convert.ToInt32(title.Split('-')[1]) : 0,
                Question = question,
            };
        }
        #endregion

        #region QuestionWithRelateAnswer
        public static QuestionWithRelateAnswer ToModel(this EvaluationQuestionModel questionModel, List<EvaluationAnswerModel> answers)
        {
            if (questionModel == null)
                return null;
            return new QuestionWithRelateAnswer
            {
                EvaluationQuestionModel = questionModel,
                EvaluationAnswersModel = answers
            };
        }
        #endregion

        #region QuestioDTO
        public static QuestionDTO ToModel(this QuestionModel item, List<QuestionModel> models)
        {

            if (models == null || item.ParentId != 0 || item.AnswerId == 0)
                return null;
            return new QuestionDTO
            {
                Id = item.QuestionId,
                Text = item.QuestionTitle,
                IsLastQuestion = item.IsLastQuestion,
                AnswerOfQuestions = models.ToAnswerModel(item.QuestionId),
                SubQuestionDTOs = models.ToSubQuestionModel(item.QuestionId)
            };


        }
        public static List<AnswerOfQuestionDTO> ToAnswerModel(this List<QuestionModel> models, int questioId)
        {
            var answerOfQuestionDTOLsit = new List<AnswerOfQuestionDTO>();
            foreach (var item in models)
            {
                if (item.QuestionId == questioId)
                {
                    var newAnswer = new AnswerOfQuestionDTO
                    {
                        QuestionId = item.QuestionId,
                        Id = item.AnswerId,
                        Text = item.AnswerTitle
                    };
                    answerOfQuestionDTOLsit.Add(newAnswer);
                }
            }
            return answerOfQuestionDTOLsit;
        }
        public static List<SubQuestionDTO> ToSubQuestionModel(this List<QuestionModel> models, int questionId)
        {
            var subQuestionLsit = new List<SubQuestionDTO>();
            foreach (var item in models)
            {
                if (item.ParentId == questionId)
                {
                    var newSubQuestionDTO = new SubQuestionDTO
                    {
                        Id = item.QuestionId,
                        QuestionId = item.ParentId,
                        Text = item.QuestionTitle
                    };
                    subQuestionLsit.Add(newSubQuestionDTO);
                }
            }
            return subQuestionLsit;
        }
        #endregion

        #region QuestionAnswer
        public static List<EvaluationQuestionAnswer> ToModel(this string[] answers, string StudentCode, string TermId, int? professorId, int? classId, int termId)
        {
            var studentAnswerOfQuestionList = new List<EvaluationQuestionAnswer>();
            if (answers.Length == 0)
                return null;
            foreach (var answer in answers)
            {
                studentAnswerOfQuestionList.Add(new EvaluationQuestionAnswer
                {
                    QuestionId = Convert.ToInt32(answer.Split(',')[0]),
                    SubQuestionId = Convert.ToInt32(answer.Split(',')[1]),
                    AnswerId = Convert.ToInt32(answer.Split(',')[2]),
                    StudentCode = StudentCode,
                    ProfessorId = professorId,
                    ClassId = classId,
                    TermId = termId
                });
            }
            return studentAnswerOfQuestionList;
        }
        #endregion

        #region Comment
        public static EvaluationComment ToModel(this string comment, string studentCode, string termId)
        {
            return new EvaluationComment
            {
                Comment = comment,
                StudentCode = studentCode,
                Term = termId
            };
        }
        #endregion

        #region Questionanswercomment
        public static List<EvaluationQuestionAnswerComment> ToModel(this string[] answerquestionCommnet, int termId, string personalCode)
        {

            if (answerquestionCommnet.Length == 0)
                return null;
            var evaluationQuestionAnswercomments = new List<EvaluationQuestionAnswerComment>();
            foreach (var item in answerquestionCommnet)
            {
                evaluationQuestionAnswercomments.Add(new EvaluationQuestionAnswerComment
                {
                    QuestionId = Convert.ToInt32(item.Split(',')[0]),
                    AnswerComment = item.Split(',')[1],
                    PersonalCode = personalCode,
                    TermId = termId
                });
            }
            return evaluationQuestionAnswercomments;
        }
        #endregion

    }
    #region RelatedModel
    public class EvaluationQuestionModel
    {
        public int Id { get; set; }
        public int TermId { get; set; }
        public string Title { get; set; }
        public bool IsLastQuestion { get; set; }
        public string TermText { get; set; }
        public int TypeId { get; set; }
        public string TypeTitle { get; set; }
        public int ChartId { get; internal set; }
        public int QuestionType { get; set; }
        public bool IsPossibilityToInsertComment { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
    public class EvaluationAnswerModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int QuestionId { get; set; }
        public int Score { get; set; }
        public int Order { get; set; }
    }
    public class QuestionWithRelateAnswer
    {
        public EvaluationQuestionModel EvaluationQuestionModel { get; set; }
        public List<EvaluationAnswerModel> EvaluationAnswersModel { get; set; }
    }



    #endregion

    #region ViewModel
    public class QuestionViewModel
    {
        public string question { get; set; }
        public int typeId { get; set; }
        public int termId { get; set; }
        public string[] answers { get; set; }
        public bool isLastQuestion { get; set; }
        public bool isPossibilityToInsertComment { get; set; }
        public int QuestionType { get; set; }
        public int questionId { get; set; }
        public int ChartId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
    public class SubQuestionViewModel
    {
        public string[] subQuestions { get; set; }
        public int questionId { get; set; }
        public int termId { get; set; }
        public int typeId { get; set; }
        public int chartId { get; set; }
    }
    public class AnswerViewModel
    {
        public string[] Answers { get; set; }
        public string Comment { get; set; }
        public string TermId { get; set; }
    }
    public class ProfessorClassViewModel
    {
        public int Id { get; set; }
        public int ProfessorId { get; set; }
        public int ClassId { get; set; }
        public string ProfessorFullName { get; set; }
        public string classTitle { get; set; }
    }
    #endregion

}