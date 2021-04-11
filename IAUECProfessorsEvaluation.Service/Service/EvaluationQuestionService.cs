using IAUECProfessorsEvaluation.Core.Model.Evaluation;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using System.Linq;
using IAUECProfessorsEvaluation.Data.Repository;
using Z.Dapper.Plus;
using System;
using System.Globalization;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class EvaluationQuestionService : BaseService<EvaluationQuestion>, IEvaluationQuestionService
    {
        #region field
        private string Connectionstr => DapperRepository.ConnectionString.Connection;
        #endregion

        #region HelperMethod
        private string ToGregorianDate(string persionDate)
        {
            if (string.IsNullOrEmpty(persionDate))
                return "";
            var date = persionDate.Split('/');
            var y = Convert.ToInt32(date[0]);
            var m = Convert.ToInt32(date[1]);
            var d = Convert.ToInt32(date[2]);
            PersianCalendar pc = new PersianCalendar();
            DateTime dt = new DateTime(y, m, d, pc);
            var dateSplit = dt.ToString().Split('/');
            var res = dateSplit[2].Split(' ')[0] + "-";
            if (dateSplit[0].Length < 2)
                res += "0" + dateSplit[0] + "-";
            else
                res += dateSplit[0] + "-";
            if (dateSplit[1].Length < 2)
                res += "0" + dateSplit[1];
            else
                res += dateSplit[1];
            return res;
        }
        private string ToPersionDate(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return $"{pc.GetYear(date)}/{pc.GetMonth(date)}/{pc.GetDayOfMonth(date)}";
        }

        private string GetCondition(int? termId = null, string startDate = null, string endDate = null, bool isChartQuery = false)
        {
            if (isChartQuery)
            {
                var conditionQuery = "";
                if (termId != null)
                    conditionQuery += $" and TermId={termId}";
                if (!string.IsNullOrEmpty(startDate))
                {
                    var date = ToGregorianDate(startDate);
                    conditionQuery += $" and StartDate<={date}";
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    var date = ToGregorianDate(endDate);
                    conditionQuery += $" and StartDate<={date}";
                }
                return conditionQuery;
            }
            return " and (TermId=(select top 1 Id from Terms where IsCurrentTerm=1) or (StartDate<=GETDATE() AND EndDate>=GETDATE()))";
        }
        #endregion

        public EvaluationQuestionService(IRepository<EvaluationQuestion> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {

        }

        public List<QuestionDTO> GetQuestionsTest(int typeId)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                var sqlQueryQuestion = $"select * from Evaluation.Question where ParentId=0 and QuestionType=2 and  EvaluationTypeId={typeId} " + GetCondition();
                var modelQuestions = conn.Query<QuestionDTO>(sqlQueryQuestion).Distinct().ToList();
                foreach (var item in modelQuestions)
                {
                    var sqlQueryAnswer = "select * from Evaluation.Answer where Question_Id=" + item.Id;
                    var modelAnswer = conn.Query<AnswerOfQuestionDTO>(sqlQueryAnswer).Distinct().ToList();
                    item.AnswerOfQuestions = modelAnswer;
                }
                foreach (var item in modelQuestions)
                {
                    var sqlSubQuestion = "select * from Evaluation.Question where ParentId=" + item.Id;
                    var modelSubQuestion = conn.Query<QuestionDTO>(sqlSubQuestion).ToList().Select(x => x.ToModel());
                    item.SubQuestionDTOs = modelSubQuestion.ToList();
                }
                return modelQuestions;
            }

        }

        public List<DescriptiveQuestionModel> GetDescriptiveQuestions(int typeId)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                var sqlQueryQuestion = $"select q.Id,q.Text from Evaluation.Question q where QuestionType=1 and EvaluationTypeId={typeId} " + GetCondition();
                var modelSubQuestion = conn.Query<DescriptiveQuestionModel>(sqlQueryQuestion).ToList();
                return modelSubQuestion;
            }
        }

        public bool IsQuestionExist(int typeId)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                var sqlQueryQuestion = $"select * from Evaluation.Question where ParentId=0 and EvaluationTypeId={typeId}"
                    + GetCondition();
                var modelQuestions = conn.Query<QuestionDTO>(sqlQueryQuestion).Distinct().ToList();
                return modelQuestions.Count() > 0;
            }
        }

        public bool IsUserevaluated(string studentCode)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                var sqlQueryQuestion = "select * from Evaluation.QuestionAnswer where termId=(select top 1 Id from Terms where IsCurrentTerm=1) and  StudentCode='" + studentCode + "' AND ProfessorId IS NULL AND ClassId IS NULL";
                var modelQuestions = conn.Query<QuestionAnswerDTO>(sqlQueryQuestion).Distinct().ToList();
                return modelQuestions.Count() > 0;
            }
        }

        public bool InsertStudentAnswers(List<EvaluationQuestionAnswer> model)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                foreach (var item in model)
                {
                    var sqlQueryQuestion = "insert into Evaluation.QuestionAnswer";

                    if (item.ProfessorId == null || item.ClassId == null)
                    {
                        sqlQueryQuestion += $"([SubQuestionId],[AnswerId],[QuestionId],[StudentCode],[TermId]) VALUES ({item.SubQuestionId},{item.AnswerId},{item.QuestionId},'{item.StudentCode}',{item.TermId})";
                    }

                    else
                        sqlQueryQuestion += $" ([SubQuestionId],[AnswerId],[QuestionId],[StudentCode],[ProfessorId],[ClassId],[TermId]) VALUES ({item.SubQuestionId},{item.AnswerId},{item.QuestionId},'{item.StudentCode}',{item.ProfessorId},{item.ClassId},{item.TermId})";
                    conn.Query<QuestionAnswerDTO>(sqlQueryQuestion);
                }
            }
            return true;

        }

        public void InsertStudentComment(string comment, string studentCode, string termId)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                var sqlQueryQuestion = "insert into Evaluation.Comment"
                + "([Comment],[Term],[StudentCode])"
                + $"VALUES ('{comment}','{termId}',{studentCode})";

                conn.Query<QuestionAnswerDTO>(sqlQueryQuestion);
            }
            //DapperPlusManager.Entity<EvaluationComment>().Table("[Evaluation].[Comment]");
            //using (var connection = new SqlConnection(Connectionstr))
            //{
            //    connection.BulkInsert(new List<EvaluationComment> { new EvaluationComment() { Comment = comment, StudentCode = studentCode, Term = termId } });
            //}
        }

        public bool IsUserEvaluateProfessor(int professorId, int classId, string studentCode)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                var sqlQueryQuestion = "select * from Evaluation.QuestionAnswer where TermId=(select top 1 Id from Terms where IsCurrentTerm=1) "
                                     + $"and ProfessorId = {professorId} and ClassId = {classId} and StudentCode = '{studentCode}'";
                var modelQuestions = conn.Query<QuestionAnswerDTO>(sqlQueryQuestion).Distinct().ToList();
                return modelQuestions.Count() > 0;
            }
        }

        public bool IsProfessorQuestionExist(int professorId, int classId)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                var sqlQueryQuestion = "select * from Evaluation.Question where  EvaluationTypeId = 2 "
                    + GetCondition();
                var modelQuestions = conn.Query<QuestionDTO>(sqlQueryQuestion).Distinct().ToList();
                return modelQuestions.Count() > 0;
            }
        }

        public int GetCurrentTermId()
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                var sqlQueryQuestion = "select top 1 Id from Terms where IsCurrentTerm=1";
                var modelQuestions = conn.Query<int>(sqlQueryQuestion).Distinct().FirstOrDefault();
                return modelQuestions;
            }
        }

        public List<ChartModel> GetChartListModel(int? professorId, int? classId, int evaluationTypeId, int termId, string startDate, string endDate)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                var chartModelList = new List<ChartModel>();

                var sqlQueryQuestion = "select * from Evaluation.Question";
                if (evaluationTypeId == 1)
                    sqlQueryQuestion += " where QuestionType!=1 and EvaluationTypeId=1 ";
                else
                    sqlQueryQuestion += " where QuestionType!=1 and EvaluationTypeId=2 ";
                sqlQueryQuestion += GetCondition(termId, startDate, endDate, true);
                // sqlQueryQuestion += " and ParentId!=0";
                List<ChartQuestionModel> questions = conn.Query<ChartQuestionModel>(sqlQueryQuestion).Distinct().ToList();
                foreach (var question in questions)
                {
                    var sqlQueryForCheckIfParentQuestion = "select * from Evaluation.Question q where q.ParentId=" + question.Id;
                    var sqlQuestionCheckCount = conn.Query<ChartQuestionModel>(sqlQueryForCheckIfParentQuestion).Distinct().ToList();
                    if (sqlQuestionCheckCount.Count == 0)
                    {


                        var chartModel = new ChartModel();
                        chartModel.AnswerPercentage = new List<AnswerPercentage>();
                        chartModel.Question = question;
                        var subQuestions = new List<ChartSubQuestionModel>();
                        var answers = new List<ChartAnswer>();
                        var sqlQuerySubQuestion = "";
                        if (question.ParentId != 0)
                        {
                            sqlQuerySubQuestion = $"select * from Evaluation.Question q where q.Id={question.Id} and q.ParentId=" + question.ParentId;
                            subQuestions = conn.Query<ChartSubQuestionModel>(sqlQuerySubQuestion).Distinct().ToList();
                            chartModel.SubQuestions = subQuestions;
                            var sqlQueryQuestionByParentId = $"select q.Text from Evaluation.Question q where q.Id={question.ParentId}";
                            question.ParentQuestionText = conn.Query<string>(sqlQueryQuestionByParentId).ToList().FirstOrDefault();
                        }
                        var sqlQueryAnswer = "";
                        if (question.ParentId != 0)
                        {
                            sqlQueryAnswer = "select * from Evaluation.Answer where Question_Id=" + question.ParentId;
                        }
                        else
                        {
                            sqlQueryAnswer = "select * from Evaluation.Answer where Question_Id=" + question.Id;
                        }

                        answers = conn.Query<ChartAnswer>(sqlQueryAnswer).Distinct().ToList();
                        chartModel.Answers = answers;


                        if (subQuestions.Count() > 0)
                        {
                            var sqlQueryTotalCount = $"select count(*) from Evaluation.QuestionAnswer qa where qa.QuestionId={question.ParentId} and qa.SubQuestionId={question.Id}";
                            if (evaluationTypeId == 2)
                                sqlQueryTotalCount += $" and qa.ClassId={classId} and qa.ProfessorId={professorId}";
                            else
                                sqlQueryTotalCount += " and qa.ClassId is null and qa.ProfessorId is null";
                            var totalCount = conn.Query<int>(sqlQueryTotalCount).FirstOrDefault();
                            foreach (var answer in answers)
                            {
                                var percentage = 0.0;
                                var sqlQueryAnswerCount = $"select count(*) from Evaluation.QuestionAnswer qa where qa.QuestionId={question.ParentId} and qa.SubQuestionId={question.Id} and  qa.AnswerId={answer.Id}";
                                if (evaluationTypeId == 2)
                                    sqlQueryAnswerCount += $" and qa.ClassId={classId} and qa.ProfessorId={professorId}";
                                else
                                    sqlQueryAnswerCount += " and qa.ClassId is null and qa.ProfessorId is null";
                                var answerCount = conn.Query<int>(sqlQueryAnswerCount).FirstOrDefault();
                                if (answerCount != 0 && totalCount != 0)
                                    percentage = ((answerCount * 100.0) / totalCount);
                                chartModel.AnswerPercentage.Add(new AnswerPercentage { Value = answer.Text, Item = percentage });
                            }


                        }
                        else
                        {
                            var sqlQueryTotalCount = $"select count(*) from Evaluation.QuestionAnswer qa where qa.QuestionId={question.Id} and qa.SubQuestionId=0";
                            if (evaluationTypeId == 2)
                                sqlQueryTotalCount += $" and qa.ClassId={classId} and qa.ProfessorId={professorId}";
                            else
                                sqlQueryTotalCount += " and qa.ClassId is null and qa.ProfessorId is null";
                            var totalCount = conn.Query<int>(sqlQueryTotalCount).FirstOrDefault();
                            foreach (var answer in answers)
                            {
                                var percentage = 0.0;
                                var sqlQueryAnswerCount = $"select count(*) from Evaluation.QuestionAnswer qa where qa.QuestionId={question.Id} and  qa.AnswerId={answer.Id} and qa.SubQuestionId=0";
                                if (evaluationTypeId == 2)
                                    sqlQueryAnswerCount += $" and qa.ClassId={classId} and qa.ProfessorId={professorId}";
                                else
                                    sqlQueryAnswerCount += " and qa.ClassId is null and qa.ProfessorId is null";
                                var answerCount = conn.Query<int>(sqlQueryAnswerCount).FirstOrDefault();
                                if (answerCount != 0 && totalCount != 0)
                                    percentage = ((answerCount * 100.0) / totalCount);
                                chartModel.AnswerPercentage.Add(new AnswerPercentage { Value = answer.Text, Item = percentage });
                            }
                        }
                        chartModelList.Add(chartModel);
                    }
                }
                return chartModelList;
            }
        }

        public int GetProfessorScore(int professorId, int classId, int termId, string startDate, string endDate)
        {
            var score = 0;
            using (var conn = new SqlConnection(Connectionstr))
            {
                var sqlQuery = $"select a.Score FROM Evaluation.Question q join Evaluation.Answer a ON q.Id = a.Question_Id JOIN Evaluation.QuestionAnswer qa on qa.AnswerId = a.Id where qa.ProfessorId = {professorId} and qa.ClassId = {classId}";
                if (termId != 0)
                    sqlQuery += $" and AND q.TermId={termId}";
                if (!string.IsNullOrEmpty(startDate))
                {
                    var date = ToGregorianDate(startDate);
                    sqlQuery += $" AND q.StartDate={date}";
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    var date = ToGregorianDate(endDate);
                    sqlQuery += $" AND q.StartDate={date}";
                }
                var answerScores = conn.Query<int>(sqlQuery).Distinct().ToList();
                foreach (var answerScore in answerScores)
                {
                    score += score + answerScore;
                }
            }
            return score;
        }
        public int GetScoreCount(int termId, string startDate, string endDate)
        {
            var score = 0;
            using (var conn = new SqlConnection(Connectionstr))
            {
                var sqlQuery = "select a.Score  from Evaluation.Question q join Evaluation.Answer a on a.Question_Id = q.Id where q.EvaluationTypeId = 2";
                if (termId != 0)
                    sqlQuery += $" and AND q.TermId={termId}";
                if (!string.IsNullOrEmpty(startDate))
                {
                    var date = ToGregorianDate(startDate);
                    sqlQuery += $" AND q.StartDate={date}";
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    var date = ToGregorianDate(endDate);
                    sqlQuery += $" AND q.StartDate={date}";
                }
                var answerScores = conn.Query<int>(sqlQuery).Distinct().ToList();
                foreach (var answerScore in answerScores)
                {
                    score += score + answerScore;
                }
            }
            return score;
        }

        public bool SaveQuestionFor(int fromTypeId, int fromTermId, int toTypeId, int totermId)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                try
                {
                    var sqlQuestions = $"select q.Id,q.ParentId from Evaluation.Question q where q.TermId={fromTermId} and EvaluationTypeId={fromTypeId} and q.ParentId=0";
                    var QuestionsId = conn.Query<QuestionParentModel>(sqlQuestions).Distinct().ToList();
                    foreach (var question in QuestionsId)
                    {
                        // insert new Question 
                        var sqlNewQuestionQuery = "insert into Evaluation.Question (Text,Description,IsLastQuestion,TermId,ParentId,EvaluationTypeId,IsPossibilityToInsertComment,QuestionType,EvaluationChartTypeId)" +
                          $" select Text,Description,IsLastQuestion,{ totermId},0,{ toTypeId},IsPossibilityToInsertComment,QuestionType,EvaluationChartTypeId from Evaluation.Question" +
                          $" where Id={question.Id} SELECT CAST(SCOPE_IDENTITY() as int)";
                        var newQuestionId = conn.Query<int>(sqlNewQuestionQuery).Distinct().FirstOrDefault();

                        // insert SubQuestion 
                        var newSubQuestionQuesry = "insert into Evaluation.Question (Text,Description,IsLastQuestion,TermId,ParentId,EvaluationTypeId,IsPossibilityToInsertComment,QuestionType,EvaluationChartTypeId)" +
                                $" select Text,Description,IsLastQuestion,{ totermId},{newQuestionId},{ toTypeId},IsPossibilityToInsertComment,QuestionType,EvaluationChartTypeId from Evaluation.Question" +
                                $" where ParentId={question.Id}";
                        conn.Query<int>(newSubQuestionQuesry);

                        //insert answers
                        var sqlAnswerQuery = $"select a.Id from Evaluation.Answer a where Question_Id={question.Id}";
                        var answersId = conn.Query<int>(sqlAnswerQuery).Distinct().ToList();
                        foreach (var answerId in answersId)
                        {
                            var sqlNewAnswerQuery = "insert into Evaluation.Answer ([Description],[Question_Id],[Text],[Score],[Order])"
                                + $" select [Description],{newQuestionId},[Text],[Score],[Order] from Evaluation.Answer where Id={answerId}";
                            conn.Query<int>(sqlNewAnswerQuery);
                        }

                    }
                    //var sqlQuery = "insert into Evaluation.Question (Text,Description,IsLastQuestion,TermId,ParentId,EvaluationTypeId,IsPossibilityToInsertComment,QuestionType,EvaluationChartTypeId)" +
                    //  $" select Text,Description,IsLastQuestion,{ totermId},ParentId,{ toTypeId},IsPossibilityToInsertComment,QuestionType,EvaluationChartTypeId from Evaluation.Question" +
                    //  $" where TermId ={ fromTermId} and EvaluationTypeId = { fromTypeId }";
                    //var answerScores = conn.Query<int>(sqlQuery).Distinct().ToList();
                    //var sqlQueryQuestionsId = $"select q.Id from Evaluation.Question q where q.TermId={fromTermId} and EvaluationTypeId={fromTypeId}";
                    //var QuestionsId = conn.Query<int>(sqlQueryQuestionsId).Distinct().ToList();
                    //foreach (var item in QuestionsId)
                    //{
                    //    var sqlQueryAnswer= "insert into Evaluation.Answer ([Description],[Question_Id],[Text],[Score],[Order])"
                    //        +$" select [Description],[Question_Id],[Text],[Score],[Order]"
                    //}
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool InsertanswerQuestioncomment(List<EvaluationQuestionAnswerComment> answerQuestionComment)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                foreach (var item in answerQuestionComment)
                {
                    if (item.AnswerComment != "")
                    {
                        var sqlQueryQuestion = "insert into [Evaluation].[QuestionAnswerComment]"
                              + "([TermId],[PersonalCode],[AnswerComment],[QuestionId])"
                              + $"VALUES ({item.TermId},'{item.PersonalCode}',N'{item.AnswerComment}',{item.QuestionId})";
                        conn.Query<QuestionAnswerDTO>(sqlQueryQuestion);
                    }

                }
            }
            return true;
            //DapperPlusManager.Entity<EvaluationQuestionAnswerComment>().Table("[Evaluation].[EvaluationQuestionAnswercomment]");
            //using (var connection = new SqlConnection(Connectionstr))
            //{
            //    connection.BulkInsert(answerQuestionComment);
            //}
            //return true;
        }

        public void ChangeLastQuestion(Term term, EvaluationType type)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                var sqlQueryQuestion = $"update Evaluation.Question set IsLastQuestion=0 where TermId={term.Id} and EvaluationTypeId={type.Id}";
                var modelQuestions = conn.Query<int>(sqlQueryQuestion).Distinct().FirstOrDefault();
            }
        }

        public bool IsQuestionExistByTermId(int termId, int evaluationTypeId)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                var sqlQueryQuestion = $"select * from Evaluation.Question where ParentId=0 and TermId={termId} and EvaluationTypeId={evaluationTypeId}";
                var modelQuestions = conn.Query<QuestionDTO>(sqlQueryQuestion).Distinct().ToList();
                return modelQuestions.Count() > 0;
            }
        }

        public bool IsUserEvaluated(string stCode, int termId, int evaluationTypeId)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                var sqlQueryQuestion = "";
                if (evaluationTypeId == 1)
                    sqlQueryQuestion = $"select * from Evaluation.QuestionAnswer where termId={termId} and  StudentCode='" + stCode + "'";
                else
                    sqlQueryQuestion = $"select * from Evaluation.QuestionAnswer where ProfessorId is not null and ClassId is not null and termId={termId} and  StudentCode='" + stCode + "'";
                var modelQuestions = conn.Query<QuestionAnswerDTO>(sqlQueryQuestion).Distinct().ToList();
                return modelQuestions.Count() > 0;
            }
        }

        public IEnumerable<EvaluationQuestionViewModel> GetAllQuestions(int termId, int typeId, string startDate, string endDate)
        {
            using (var conn = new SqlConnection(Connectionstr))
            {
                var sqlQueryQuestion = $"SELECT q.Id,q.Text,t.TermCode AS TermText,ty.Title,t.Id AS TermId FROM Evaluation.Question q";
                sqlQueryQuestion += " JOIN dbo.Terms t ON t.Id = q.TermId";
                sqlQueryQuestion+= $" JOIN Evaluation.Type ty ON ty.Id = q.EvaluationTypeId where q.EvaluationTypeId={typeId}";
                if (termId != 0)
                    sqlQueryQuestion += $" and [TermId]={termId}";
                if (!string.IsNullOrEmpty(startDate))
                {
                    var date = ToGregorianDate(startDate);
                    sqlQueryQuestion += $" and [StartDate]<='{date}'";
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    var date = ToGregorianDate(endDate);
                    sqlQueryQuestion += $" and [EndDate]>='{date}'";
                }
                var modeldd = conn.Query<EvaluationQuestionViewModel>(sqlQueryQuestion).Distinct().ToList();
                return modeldd;
            }
        }
    }
}
