using AutoMapper;
using IAUECProfessorsEvaluation.Core.Helper;
using IAUECProfessorsEvaluation.Core.Model.Evaluation;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Service.IService;
using IAUECProfessorsEvaluation.Web.Models;
using IAUECProfessorsEvaluation.Web.Models.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IAUECProfessorsEvaluation.Web.Controllers
{
    public class StudentEvaluationController : Controller
    {
        #region Fileds
        private readonly ITermService _termService;
        private readonly IEvaluationQuestionService _evaluationQuestionService;
        private readonly IEvaluationAnswerService _evaluationAnswerService;
        private readonly IEvaluationTypeService _evaluationTypeService;
        private IRoleAccessService _roleAccessService;
        private IUserRoleService _userRoleService;
        private ILogService _logService;
        private readonly IStudentEducationalClassService _studentEducationalClassService;
        #endregion

        #region Const
        public StudentEvaluationController(ITermService termService, IEvaluationQuestionService evaluationQuestionService,
            IEvaluationAnswerService evaluationAnswerService
              , IRoleAccessService roleAccessService
            , IUserRoleService userRoleService
            , ILogService logService,
            IEvaluationTypeService evaluationTypeService
            , IStudentEducationalClassService studentEducationalClassService)
        {
            _termService = termService;
            _evaluationQuestionService = evaluationQuestionService;
            _evaluationAnswerService = evaluationAnswerService;
            _roleAccessService = roleAccessService;
            _userRoleService = userRoleService;
            _logService = logService;
            _evaluationTypeService = evaluationTypeService;
            _studentEducationalClassService = studentEducationalClassService;
        }
        #endregion

        #region Loog
        public void LogError(string error)
        {
            string filePath = Server.MapPath("~/App_Data/Error.txt");

            using (StreamWriter writer = System.IO.File.AppendText(filePath))
            {
                writer.Write(error);
                writer.Flush();
            }
        }
        #endregion

        #region HelperMethod
        public void SetViewBag(string title = null, string menuItem = null)
        {
            ViewBag.Title = title;
            ViewBag.LastUpdate = _logService.LastUpdate();
            var roles = new List<int>();
            if (!string.IsNullOrEmpty(menuItem))
                ViewBag.MenuItem = menuItem;
            if (Session["UserInfo"] != null)
            {
                var user = (Models.User)Session["UserInfo"];
                ViewBag.FullName = user.FirstName + " " + user.LastName;
                var x = _userRoleService.GetMany(g => g.User.ID == user.ID).ToList();
                roles.AddRange(_userRoleService.GetMany(g => g.User.ID == user.ID).Select(s => s.Role.Id));
            }
            var accessList = _roleAccessService.GetMany(g => roles.Contains(g.Role.Id)).Select(s => s.Access).ToList();
            var allSections = accessList.Select(s => s.MenuList.MenuSection).Distinct();
            var sections = new List<Models.MenuSection>();
            foreach (var section in allSections)
            {
                var item = new Models.MenuSection
                {
                    Id = section.Id,
                    Name = section.Name
                };
                var itemAccess = section.MenuLists.SelectMany(s => s.Accesses).Where(w => accessList.Select(s => s.Id).Contains(w.Id)).Select(s => s.MenuList).Distinct();
                item.MenuLists = Mapper.Map<List<Models.MenuList>>(itemAccess);
                sections.Add(item);
            }
            foreach (var list in sections.SelectMany(s => s.MenuLists))
            {
                var item = list.Accesses.Where(w => accessList.Select(s => s.Id).Contains(w.Id)).ToList();
                list.Accesses = item;
            }
            ViewBag.Menu = sections;
        }

        public List<StudentEducationClassViewModel> GetStudentsClassInfo(string stcode)
        {
            var currentTerm = _termService.Get(g => g.IsCurrentTerm);
            var stCodeInt = Convert.ToInt32(stcode);
            var units = ClientHelper.GetValue<UnitProfessorsSyncModel>(StaticValue.GetStudentCurrentUnits + $"/?stcode={stCodeInt}");
            //var classList =  _studentEducationalClassService.GetMany(g => g.StudentId == stCodeInt && g.Term.Id == currentTerm.Id).ToList();
            return units.Select(s => new StudentEducationClassViewModel
            {
                stcode = stcode,
                EducationClassId = s.Did,
                EducationClassName = s.ClassName,
                //ProfessorFirstName = s.ProfessorName,
                ProfessorLastName = s.ProfessorName,
                ProfessorId = s.ProfessorCode
            }).ToList();
        }
        private void InsertAnswers(QuestionViewModel model, Model.Models.EvaluationQuestion questionModel)
        {
            if (model.QuestionType == 2)
            {
                var evaluationAnswers = model.answers?.Select(x => x.ToModel(questionModel)).ToList();
                if (evaluationAnswers != null)
                {
                    foreach (var evaluationAnswer in evaluationAnswers)
                    {
                        _evaluationAnswerService.Add(evaluationAnswer);
                    }
                }
            }
        }
        #endregion

        #region CRUD
        public ActionResult IndexQuestion()
        {
            SetViewBag(title: "لیست سوالات ارزیابی", menuItem: "Create");
            ViewBag.Terms = _termService.GetAll().ToDictionary(x => x.Id, x => x.Name);
            ViewBag.Types = _evaluationTypeService.GetAll().ToDictionary(x => x.Id, x => x.Title);
            return View();
        }
        [HttpGet]
        public ActionResult ShowQuestions(int typeId, int termId, string startDate, string endDate)
        {
            ViewBag.TypeId = typeId;
            ViewBag.termId = termId;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            return View();
        }
        public ActionResult GetAllQuestions([DataSourceRequest]DataSourceRequest request, int termId, int typeId, string startDate, string endDate)
        {
            var models = _evaluationQuestionService.GetAllQuestions(termId, typeId, startDate, endDate).ToList().Select(x => x.ToModel());
            //var models = _evaluationQuestionService.GetAll().Where(x => x.ParentId == 0 & x.TermId == termId && x.EvaluationTypeId == typeId).ToList().Select(x => x.ToModel());
            return Json(models.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            SetViewBag(title: "ایجاد سوال ارزیابی", menuItem: "Create");
            ViewBag.Terms = _termService.GetAll().ToDictionary(x => x.Id, x => x.Name);
            ViewBag.Types = _evaluationTypeService.GetAll().ToDictionary(x => x.Id, x => x.Title);
            return View();
        }
        [HttpPost]
        public ActionResult Insert(QuestionViewModel model)
        {
            try
            {

                var term = _termService.Get(x => x.Id == model.termId);
                var type = _evaluationTypeService.Get(x => x.Id == model.typeId);
                var questionModel = model.question.ToModel(term, type, 0, model.isLastQuestion, model.ChartId, model.isPossibilityToInsertComment, model.QuestionType, model.StartDate, model.EndDate);
                if (model.isLastQuestion)
                    _evaluationQuestionService.ChangeLastQuestion(term, type);
                _evaluationQuestionService.Add(questionModel);
                InsertAnswers(model, questionModel);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError(ex.InnerException.Message);
                LogError(ex.InnerException.InnerException.Message);
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpGet]
        public ActionResult Edit(int questionId, int termId)
        {
            SetViewBag(title: "ویرایش سوال ارزیابی", menuItem: "Edit");
            ViewBag.Terms = _termService.GetAll().ToDictionary(x => x.Id, x => x.Name);
            ViewBag.Types = _evaluationTypeService.GetAll().ToDictionary(x => x.Id, x => x.Title);
            var question = _evaluationQuestionService.Get(x => x.Id == questionId)?.ToModel();
            var answers = _evaluationAnswerService.GetMany(x => x.Question.Id == questionId)?.Select(x => x.ToModel()).ToList();
            var model = question.ToModel(answers);
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(QuestionViewModel model)
        {
            var questionModel = _evaluationQuestionService.Get(x => x.Id == model.questionId);
            var term = _termService.Get(x => x.Id == model.termId);
            var type = _evaluationTypeService.Get(x => x.Id == model.typeId);
            try
            {
                var newModel = questionModel.ToModel(model, term, type);
                _evaluationQuestionService.Update(newModel);
            }
            catch (Exception ex)
            {

            }
            _evaluationAnswerService.DeleteMany(x => x.Question.Id == model.questionId);
            InsertAnswers(model, questionModel);
            return RedirectToAction("IndexQuestion");
        }
        [HttpGet]
        public JsonResult Delete(int questionId)
        {
            var relateAnswers = _evaluationAnswerService.GetMany(x => x.Question.Id == questionId).ToList();
            foreach (var item in relateAnswers)
            {
                _evaluationAnswerService.Delete(item);
            }
            var subQuestions = _evaluationQuestionService.GetMany(x => x.ParentId == questionId).ToList();
            foreach (var item in subQuestions)
            {
                _evaluationQuestionService.Delete(item);
            }
            var question = _evaluationQuestionService.Get(x => x.Id == questionId);
            _evaluationQuestionService.Delete(question);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateSubquestion(int questionId)
        {
            SetViewBag(title: "ایجاد زیر سوال", menuItem: "CreateSubquestion");
            var question = _evaluationQuestionService.Get(x => x.Id == questionId).ToModel();
            ViewBag.QuestionId = question.Id;
            ViewBag.TermId = question.TermId;
            ViewBag.QuestionTitle = question.Title;
            ViewBag.TypeId = question.TypeId;
            ViewBag.ChartId = question.ChartId;
            var model = _evaluationQuestionService.GetMany(x => x.ParentId == questionId).ToList().Select(x => x.ToModel());
            return View(model);
        }
        [HttpPost]
        public ActionResult InsertSubQuestion(SubQuestionViewModel model)
        {
            var oldSubQuestion = _evaluationQuestionService.GetMany(x => x.ParentId == model.questionId);
            foreach (var item in oldSubQuestion)
            {
                _evaluationQuestionService.Delete(item);
            }
            var term = _termService.Get(x => x.Id == model.termId);
            var type = _evaluationTypeService.Get(x => x.Id == model.typeId);
            foreach (var item in model.subQuestions)
            {
                var questionModel = item.ToModel(term, type, model.questionId, false, model.chartId, false, 0);
                _evaluationQuestionService.Add(questionModel);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult SaveQuestionFor(int fromTypeId, int fromTermId, int toTypeId, int totermId)
        {
            var res = _evaluationQuestionService.SaveQuestionFor(fromTypeId, fromTermId, toTypeId, totermId);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Evaluation
        public ActionResult ProfessorEvaluation()
        {
            SetViewBag(title: "ارزیابی اساتید", menuItem: "Index");
            if (Session["UserInfo"] == null)
                return RedirectToAction("Login", "Pages");
            else
            {
                var user = (Models.User)Session["UserInfo"];
                //user.PersonalCode = "952264635";
                var modelLoist = GetStudentsClassInfo(user.PersonalCode);
                return View(modelLoist);
            }
        }
        [HttpGet]
        public ActionResult EvaluateProfessor(decimal professorId, decimal classId, string professorFullName, string classTitle, string stcode)
        {
            var pageTite = $"ارزیابی استاد {professorFullName} در کلاس {classTitle}";
            SetViewBag(title: pageTite, menuItem: "EvaluateProfessor");
            ViewBag.ProfessorId = Convert.ToInt32(professorId);
            ViewBag.ClassId = Convert.ToInt32(classId);
            ViewBag.Stcode = stcode;
            return View();
        }

        public JsonResult IsUserEvaluatedProfessor(string professorIdStr, string classIdStr, string stcode = null)
        {
            if (stcode == null)
            {
                var user = (Models.User)Session["UserInfo"];
                stcode = user.PersonalCode.ToString();
            }
            var professorId = Convert.ToInt32(professorIdStr);
            var classId = Convert.ToInt32(classIdStr);
            var res = true;
            var result = _evaluationQuestionService.IsProfessorQuestionExist(professorId, classId);
            if (result)
                res = _evaluationQuestionService.IsUserEvaluateProfessor(professorId, classId, stcode);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult IsProfessorQuestionExist(int professorId, int classId)
        {
            var res = _evaluationQuestionService.IsProfessorQuestionExist(professorId, classId);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            if (Session["UserInfo"] == null)
                return RedirectToAction("Login", "Pages");
            SetViewBag(title: "ارزیابی واحد", menuItem: "Index");
            //ViewBag.TermId = _evaluationQuestionService.GetCurrentTermId();
            return View();
        }

        public JsonResult IsQuestionExist(int typeId)
        {
            var res = _evaluationQuestionService.IsQuestionExist(typeId);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsUserevaluated(string stcode = null)
        {
            if (stcode == null)
            {
                var user = (Models.User)Session["UserInfo"];
                stcode = user.PersonalCode;
            }
            var res = true;
            var result = _evaluationQuestionService.IsQuestionExist(1);
            if (result)
                res = _evaluationQuestionService.IsUserevaluated(stcode);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTestQuestions(int typeId = 1)
        {
            var models = _evaluationQuestionService.GetQuestionsTest(typeId).ToList();
            return Json(models, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDescriptiveQuestions(int typeId = 1)
        {
            var res = _evaluationQuestionService.GetDescriptiveQuestions(typeId);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public bool InsertStudentAnswer(string[] Answers, string Comment, string TermId, int? professorId, int? classId, string[] descriptiveAnswerArray, string stcode = null)
        {
            if (stcode == null)
            {
                var user = (Models.User)Session["UserInfo"];
                stcode = user.PersonalCode.ToString();
            }
            var studentAnswerList = new List<EvaluationQuestionAnswer>();
            var termId = _termService.Get(x => x.IsCurrentTerm).Id;
            if (Answers != null)
                studentAnswerList = Answers.ToModel(stcode, TermId, professorId, classId, termId);
            var studentComment = Comment.ToModel(stcode, TermId);
            var answerQuestionComment = descriptiveAnswerArray.ToModel(termId, stcode);
            try
            {
                if (Answers != null)
                    _evaluationQuestionService.InsertStudentAnswers(studentAnswerList);
                if (!string.IsNullOrEmpty(Comment))
                    _evaluationQuestionService.InsertStudentComment(Comment, stcode, TermId);
                if (answerQuestionComment.Count() > 0)
                    _evaluationQuestionService.InsertanswerQuestioncomment(answerQuestionComment);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool IsUserEvaluatedInTerm(string stCode, int termId, int evaluationTypeId)
        {
            var isQuestionExist = _evaluationQuestionService.IsQuestionExistByTermId(termId, evaluationTypeId);
            if (isQuestionExist)
                return _evaluationQuestionService.IsUserEvaluated(stCode, termId, evaluationTypeId);
            return true;
        }
        #endregion

        #region Report
        public ActionResult Chart()
        {
            SetViewBag(title: "گزارش تصویری ارزیابی واحد", menuItem: "Chart");
            ViewBag.Terms = _termService.GetAll().ToDictionary(x => x.Id, x => x.Name);
            return View();
        }

        public ActionResult ProfessorEvaluationReport()
        {
            SetViewBag(title: "گزارش تصویری ارزیابی اساتید", menuItem: "ProfessorEvaluationReport");

            return View();
        }
        public ActionResult GetAllProfessor([DataSourceRequest]DataSourceRequest request)
        {
            //var currentTerm = _termService.Get(g => g.IsCurrentTerm);
            //var classList = _studentEducationalClassService.GetMany(x => x.Term.Id == currentTerm.Id).ToList();
            //var models = classList.Select(s => new StudentEducationClassViewModel
            //{
            //    EducationClassId = s.EducationalClass.Id,
            //    EducationClassName = s.EducationalClass.Name,
            //    ProfessorFirstName = s.EducationalClass.Professor.Name,
            //    ProfessorLastName = s.EducationalClass.Professor.Family,
            //    ProfessorId = s.EducationalClass.Professor.Id
            //}).ToList();
            var user = (Models.User)Session["UserInfo"];
            var stcode = user.PersonalCode;
            var models = GetStudentsClassInfo(stcode);
            return Json(models.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ProfessorReportResult(int professorId, int classId)
        {
            var classList = _studentEducationalClassService.GetMany(x => x.EducationalClass.Id == classId).ToList();
            var model = classList.Select(s => new StudentEducationClassViewModel
            {
                EducationClassId = s.EducationalClass.Id,
                EducationClassName = s.EducationalClass.Name,
                ProfessorFirstName = s.EducationalClass.Professor.Name,
                ProfessorLastName = s.EducationalClass.Professor.Family,
                ProfessorId = s.EducationalClass.Professor.Id
            }).FirstOrDefault();
            var professorName = model.ProfessorFullName;
            var pageTite = $"گزارش تصویری ارزیابی استاد {professorName} در کلاس {model.EducationClassName}";
            SetViewBag(title: "گزارش تصویری ارزیابی استاد", menuItem: "ProfessorReportResult");
            ViewBag.ProfessorId = professorId;
            ViewBag.ClassId = classId;
            ViewBag.pageTitle = pageTite;
            ViewBag.Terms = _termService.GetAll().ToDictionary(x => x.Id, x => x.Name);
            return View();
        }
        public JsonResult GetChart(int professorId, int classId, int evaluationTypeId, int termId, string startDate, string endDate)
        {
            var model = _evaluationQuestionService.GetChartListModel(professorId, classId, evaluationTypeId, termId, startDate, endDate);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProfessorScore(int professorId, int classId, int termId, string startDate, string endDate)
        {
            var score = _evaluationQuestionService.GetProfessorScore(professorId, classId, termId, startDate, endDate);
            return Json(score, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAnswersScore(int termId, string startDate, string endDate)
        {
            var scores = _evaluationQuestionService.GetScoreCount(termId, startDate, endDate);
            return Json(scores, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}