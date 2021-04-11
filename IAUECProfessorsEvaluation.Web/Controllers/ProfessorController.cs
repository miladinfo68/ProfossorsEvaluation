using AutoMapper;
using IAUECProfessorsEvaluation.Data.ReportModel;
using IAUECProfessorsEvaluation.Service.IService;
using IAUECProfessorsEvaluation.Service.Service;
using IAUECProfessorsEvaluation.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Web.Controllers
{
    public class ProfessorController : Controller
    {
        private IProfessorService _professorService;
        private IProfessorScoreService _professorScoreService;
        private IEducationalGroupService _educationalGroupService;
        private ICollegeService _collegeService;
        private ITermService _termService;
        private IIndicatorService _indicatorService;
        private IRoleAccessService _roleAccessService;
        private IUserRoleService _userRoleService;
        private DapperService _reportService;
        private ILogService _logService;
        private IEducationalClassService _educationalClassService;

        public ProfessorController(
            IProfessorService professorService
            , IProfessorScoreService professorScoreService
            , IEducationalGroupService educationalGroupService
            , ICollegeService collegeService
            , ITermService termService
            , IIndicatorService indicatorService
            , IRoleAccessService roleAccessService
            , IUserRoleService userRoleService
            , ILogService logService
            , IEducationalClassService educationalClassService
            )
        {
            _professorService = professorService;
            _professorScoreService = professorScoreService;
            _educationalGroupService = educationalGroupService;
            _collegeService = collegeService;
            _termService = termService;
            _indicatorService = indicatorService;
            _roleAccessService = roleAccessService;
            _userRoleService = userRoleService;
            _reportService = new DapperService(educationalGroupService);
            _logService = logService;
            _educationalClassService = educationalClassService;
        }

        public ActionResult ProfessorManagement()
        {
            var professors = _professorService.GetAll().OrderBy(o => o.Id).ToList();
            return View(Mapper.Map<List<Model.Models.Professor>, List<Models.Professor>>(professors));
        }
        [Authorize]
        public ActionResult RankOfProfessorInUniversity(string term = null)
        {
            SetViewBag(title: "رتبه بندی اساتید در سطح دانشگاه", menuItem: "RankOfProfessorInUniversity");
            //// Old Methode Start
            //var counter = 0;
            //var professors = _professorService.GetAll().OrderByDescending(o => o.ProfessorScores.Sum(s => s.CurrentScore)).Select(x => new Models.Professor
            //{
            //    EducationalClasses = Mapper.Map<ICollection<Model.Models.EducationalClass>, ICollection<Models.EducationalClass>>(x.EducationalClasses),
            //    ProfessorScores = Mapper.Map<ICollection<Model.Models.ProfessorScore>, ICollection<Models.ProfessorScore>>(x.ProfessorScores),
            //    Name = x.Name,
            //    Family = x.Family,
            //    ProfessorCode = x.ProfessorCode,
            //    NationalCode = x.NationalCode,
            //    Gender = Convert.ToBoolean(x.Gender),
            //    RankInUniversity = ++counter
            //}).ToList();
            //// Old Methode End
            var termId = 0;
            ViewBag.TermList = new SelectList(_termService.GetAll().OrderByDescending(o => o.TermCode), "Id", "Name");
            if (!string.IsNullOrEmpty(term) && int.TryParse(term, out termId) && termId > 0)
            {
                var model = _reportService.GetProfessorReport(termId).General.ToList();
                foreach (var item in model)
                    item.RowNumber = model.IndexOf(item) + 1;
                return View(model);
            }
            return View();
        }
        [Authorize]
        public ActionResult ViewProfessorRankInUniversityDetails(int id = 0, int termId = 0)
        {
            //// Old Methode Start
            //var professorScores = new List<Models.ProfessorScore>();
            //if (id > 0)
            //    professorScores = Mapper.Map<List<Model.Models.ProfessorScore>, List<Models.ProfessorScore>>(_professorScoreService.GetAll().Where(w => w.Professor.ProfessorCode == id).ToList());
            //return PartialView(professorScores);
            //// Old Methode End
            if (termId == 0)
                termId = GetCurrentTerm().Id;
            ViewBag.TermId = termId;
            var model = _reportService.GetProfessorReport(termId).Detial.Where(p => p.ProfessorId == id).ToList();
            return PartialView(model);
        }
        [Authorize]
        public ActionResult RankOfProfessorInCollege(string id = null, string term = null)
        {
            SetViewBag(title: "رتبه بندی اساتید در سطح دانشکده", menuItem: "RankOfProfessorInCollege");
            var collegeId = 0;
            var termId = 0;
            ViewBag.CollegeList = new SelectList(_collegeService.GetAll(), "Id", "Name");
            ViewBag.TermList = new SelectList(_termService.GetAll().OrderByDescending(o => o.TermCode), "Id", "Name");

            if (!string.IsNullOrEmpty(id)) int.TryParse(id, out collegeId);
            var user = (Models.User)Session["UserInfo"];
            if (user != null && user.College != null)
                collegeId = user.College.Id;
            if (!string.IsNullOrEmpty(term) && int.TryParse(term, out termId) && termId > 0 && collegeId > 0)
            {
                ViewBag.CollegeName = _collegeService.Get(w => w.Id == collegeId).Name;
                //// Old Methode Start
                //    var counter = 0;
                //    var professors = _collegeService.Get(w => w.Id == collegeId).EducationalGroups.SelectMany(s => s.EducationalClasses).Where(w => w.Term.Id == termId)
                //        .Select(s => s.Professor).OrderByDescending(o => o.ProfessorScores.Where(w => w.Term != null && w.Term.Id == termId).Sum(s => s.CurrentScore)).Distinct()
                //        .Select(s => new Models.Professor
                //        {
                //            //Colleges = Mapper.Map<ICollection<Model.Models.College>, ICollection<Models.College>>(s.Colleges),
                //            //EducationalGroups = Mapper.Map<ICollection<Model.Models.EducationalGroup>, ICollection<Models.EducationalGroup>>(s.EducationalGroups),
                //            EducationalClasses = Mapper.Map<ICollection<Model.Models.EducationalClass>, ICollection<Models.EducationalClass>>(s.EducationalClasses),
                //            ProfessorScores = Mapper.Map<ICollection<Model.Models.ProfessorScore>, ICollection<Models.ProfessorScore>>(s.ProfessorScores),
                //            Name = s.Name,
                //            Family = s.Family,
                //            NationalCode = s.NationalCode,
                //            ProfessorCode = s.ProfessorCode,
                //            Gender = Convert.ToBoolean(s.Gender),
                //            RankInUniversity = ++counter
                //        }).ToList();
                //    return View(professors);
                //// Old Methode End

                var collegeList = new List<int>();
                collegeList.Add(collegeId);
                var model = _reportService.GetProfessorReport(termId, collegeList: collegeList, allColleges: false).General.ToList();
                foreach (var item in model)
                    item.RowNumber = model.IndexOf(item) + 1;
                return View(model);
            }
            //else show error
            return View();


        }

        public ActionResult RankOfProfessorInEducationalGroup(string id = null, string group = null, string term = null)
        {
            SetViewBag(title: "رتبه بندی اساتید در سطح گروه", menuItem: "RankOfProfessorInEducationalGroup");
            var collegeId = 0;
            var termId = 0;
            var groupCode = 0;

            ViewBag.CollegeList = new SelectList(_collegeService.GetAll().Where(w => 1 == 2), "Id", "Name", id);
            ViewBag.GroupList = new SelectList(_educationalGroupService.GetAll().Where(w => 1 == 2), "EducationalGroupCode", "Name", group);
            ViewBag.TermList = new SelectList(_termService.GetAll().OrderByDescending(o => o.TermCode), "Id", "Name", term);

            if (!string.IsNullOrEmpty(id)) int.TryParse(id, out collegeId);
            if (!string.IsNullOrEmpty(group)) int.TryParse(group, out groupCode);
            var user = (Models.User)Session["UserInfo"];
            if (user != null && user.College != null)
            {
                collegeId = user.College.Id;
                ViewBag.CollegeId = collegeId;
            }
            if (user != null && user.EducationalGroupCode != null)
                groupCode = (int)user.EducationalGroupCode;

            if (!string.IsNullOrEmpty(term) && int.TryParse(term, out termId) && termId > 0 && collegeId > 0 && groupCode > 0)
            {
                var educationalGroup = _educationalGroupService.Get(g => g.EducationalGroupCode == groupCode && g.Term.Id == termId);
                var college = _collegeService.Get(g => g.Id == collegeId);
                ViewBag.EducationalGroup = educationalGroup.Name;
                ViewBag.College = college.Name;
                ViewBag.CollegeId = college.Id;
                ViewBag.EducationalGroupId = educationalGroup.EducationalGroupCode;
                ViewBag.TermId = termId;

                //// Old Methode Start
                //var counter = 0;
                //var professors = _educationalGroupService.Get(g => g.Id == groupId).EducationalClasses.Where(w => w.Term.Id == termId).Select(s => s.Professor)
                //    .OrderByDescending(o => o.ProfessorScores.Where(w => w.Term != null && w.Term.Id == termId).Sum(s => s.CurrentScore))
                //    .Select(s => new Models.Professor
                //    {
                //        //Colleges = Mapper.Map<ICollection<Model.Models.College>, ICollection<Models.College>>(s.Colleges),
                //        //EducationalGroups = Mapper.Map<ICollection<Model.Models.EducationalGroup>, ICollection<Models.EducationalGroup>>(s.EducationalGroups),
                //        EducationalClasses = Mapper.Map<ICollection<Model.Models.EducationalClass>, ICollection<Models.EducationalClass>>(s.EducationalClasses),
                //        ProfessorScores = Mapper.Map<ICollection<Model.Models.ProfessorScore>, ICollection<Models.ProfessorScore>>(s.ProfessorScores),
                //        Name = s.Name,
                //        Family = s.Family,
                //        NationalCode = s.NationalCode,
                //        ProfessorCode = s.ProfessorCode,
                //        Gender = Convert.ToBoolean(s.Gender),
                //        RankInUniversity = ++counter
                //    })
                //    .ToList();
                //return View(professors);
                //// Old Methode End
                var collegeList = new List<int>();
                var groupList = new List<int>();
                collegeList.Add(collegeId);
                groupList.Add(educationalGroup.Id);
                var model = _reportService.GetProfessorReport(termId, collegeList: collegeList, groupList: groupList, allColleges: false, allGroups: false)
                    .General.ToList();
                foreach (var item in model)
                    item.RowNumber = model.IndexOf(item) + 1;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetListOfColleges(string termId)
        {
            if (!string.IsNullOrEmpty(termId))
            {
                TempData["SelectedTermId"] = Convert.ToInt32(termId);
                return Json(_collegeService.GetAll().Select(s => new { Name = s.Name, Code = s.Id }));
            }
            return null;
        }

        [Authorize]
        public ActionResult GroupManagerRating()
        {
            SetViewBag("ثبت نظر مدیر گروه در خصوص اساتید", "GroupManagerRating");
            Term selectedTerm = null;
            if (string.IsNullOrEmpty(Request.QueryString["termId"]))
                selectedTerm = GetCurrentTerm();
            else
            {
                var termId = Convert.ToInt32(Request.QueryString["termId"]);
                selectedTerm = _termService.Get(g => g.Id == termId);
            }
            ViewBag.GroupList = new SelectList(_educationalGroupService.GetMany(w => w.Term.Id == selectedTerm.Id).Where(w => !StaticValue.IneligibleEducationalGroupCodes.Contains((int)w.EducationalGroupCode)), "EducationalGroupCode", "Name");
            ViewBag.TermList = new SelectList(_termService.GetAll().OrderByDescending(o => o.TermCode), "Id", "Name");
            var user = (Models.User)Session["UserInfo"];
            var groupQS = 0;

            ViewBag.TermId = selectedTerm.Id;
            if (Request.QueryString["group"] != null && int.TryParse(Request.QueryString["group"], out groupQS))
            {
                TempData["model"] = _educationalGroupService.GetMany(g => g.EducationalGroupCode == groupQS && g.Term.Id == selectedTerm.Id
                && !StaticValue.IneligibleEducationalGroupCodes.Contains((int)g.EducationalGroupCode))
                    .SelectMany(s => s.EducationalClasses).Where(w => w.Term.Id == selectedTerm.Id)
                    .Select(s => s.Professor).Distinct()
                    .Select(s => new Models.Professor
                    {
                        //EducationalClasses = Mapper.Map<ICollection<Model.Models.EducationalClass>, ICollection<Models.EducationalClass>>(s.EducationalClasses),
                        //ProfessorScores = Mapper.Map<ICollection<Model.Models.ProfessorScore>, ICollection<Models.ProfessorScore>>(s.ProfessorScores),
                        Name = s.Name,
                        Family = s.Family,
                        NationalCode = s.NationalCode,
                        GroupMangerComments = GetCurrentGroupManagerRating((int)s.ProfessorCode, "p" + (int)IndicatorProfessorName.نظر_مدیر_گروه, selectedTerm.Id, groupQS),
                        ProfessorCode = s.ProfessorCode
                    })
                    .ToList();
                TempData["groupId"] = groupQS;
            }
            if ((user == null || user.EducationalGroupCode == null) && TempData["model"] == null)
                return View();
            /// TODO:
            /// چک کردن وجود شاخص قبل از گرفتن لیست امتیازها
            ViewBag.Scores = _indicatorService.Get(g => g.CountOfType == "p" + (int)IndicatorProfessorName.نظر_مدیر_گروه).Scores.OrderByDescending(o => o.Point).ToList();
            if (TempData["model"] != null)
            {
                var professors = (List<Models.Professor>)TempData["model"];
                ViewBag.GroupId = TempData["groupId"];
                TempData["model"] = null;
                TempData["groupId"] = null;
                return View(professors);
            }
            else
            {
                var groupId = user.EducationalGroupCode;
                ViewBag.GroupId = groupId;
                var professors = _educationalGroupService.GetMany(g => g.EducationalGroupCode == groupId && g.Term.Id == selectedTerm.Id)
                    .SelectMany(s => s.EducationalClasses)
                    .Where(w => w.Term.Id == selectedTerm.Id)
                    .Select(s => s.Professor).Distinct()
                    .Select(s => new Models.Professor
                    {
                        //EducationalClasses = Mapper.Map<ICollection<Model.Models.EducationalClass>, ICollection<Models.EducationalClass>>(s.EducationalClasses),
                        //ProfessorScores = Mapper.Map<ICollection<Model.Models.ProfessorScore>, ICollection<Models.ProfessorScore>>(s.ProfessorScores),
                        Name = s.Name,
                        Family = s.Family,
                        NationalCode = s.NationalCode,
                        GroupMangerComments = GetCurrentGroupManagerRating((int)s.ProfessorCode, "p" + (int)IndicatorProfessorName.نظر_مدیر_گروه, selectedTerm.Id, (int)groupId),
                        ProfessorCode = s.ProfessorCode
                    })
                    .ToList();
                return View(professors);
            }
        }

        //[HttpPost]
        //public ActionResult GroupManagerRatingForAdmin(string group)
        //{
        //    var term = GetCurrentTerm();
        //    var groupId = Convert.ToInt32(group);
        //    var professors = _educationalGroupService.GetMany(g => g.Id == groupId).SelectMany(s => s.EducationalClasses)
        //            .Select(s => s.Professor).Distinct()
        //            .Select(s => new Models.Professor
        //            {
        //                EducationalClasses = Mapper.Map<ICollection<Model.Models.EducationalClass>, ICollection<Models.EducationalClass>>(s.EducationalClasses),
        //                ProfessorScores = Mapper.Map<ICollection<Model.Models.ProfessorScore>, ICollection<Models.ProfessorScore>>(s.ProfessorScores),
        //                Name = s.Name,
        //                Family = s.Family,
        //                NationalCode = s.NationalCode,
        //                GroupMangerComments = GetCurrentRating((int)s.ProfessorCode, "g" + (int)IndicatorProfessorName.نظر_مدیر_گروه),
        //                ProfessorCode = s.ProfessorCode
        //            })
        //            .ToList();
        //    TempData["model"] = professors;
        //    TempData["groupId"] = groupId;
        //    return this.Json(string.Empty);
        //}

        [Authorize]
        [HttpPost]
        public ActionResult GroupManagerRating(List<Models.Professor> professors)
        {
            SetViewBag("ثبت نظر مدیر گروه در خصوص اساتید", "GroupManagerRating");
            foreach (var professor in professors)
                _professorService.Update(Mapper.Map<Models.Professor, Model.Models.Professor>(professor));
            // show success message
            return View();
        }

        public ActionResult ProfessorCollegeRating()
        {
            SetViewBag("ثبت نظر دانشکده در خصوص نحوه دسترسی و تعامل با استاد", "ProfessorCollegeRating");
            /// TODO:
            /// چک کردن وجود شاخص قبل از گرفتن لیست امتیازها
            ViewBag.Scores = _indicatorService.Get(g => g.CountOfType == "p" + (int)IndicatorProfessorName.وضعیت_دسترسی_به_استاد_و_تعامل_با_دانشکده).Scores
                .OrderByDescending(o => o.Point).ToList();
            ViewBag.CollegeList = new SelectList(_collegeService.GetAll(), "Id", "Name");
            ViewBag.TermList = new SelectList(_termService.GetAll().OrderByDescending(o => o.TermCode), "Id", "Name");
            var user = (Models.User)Session["UserInfo"];
            var collegeQS = 0;
            Term selectedTerm = null;
            if (string.IsNullOrEmpty(Request.QueryString["termId"]))
                selectedTerm = GetCurrentTerm();
            else
            {
                var termId = Convert.ToInt32(Request.QueryString["termId"]);
                selectedTerm = _termService.Get(g => g.Id == termId);
            }
            ViewBag.TermId = selectedTerm.Id;
            if (Request.QueryString["college"] != null && int.TryParse(Request.QueryString["college"], out collegeQS))
            {
                TempData["model"] = _collegeService.Get(c => c.Id == collegeQS).EducationalGroups.Where(w => w.IsActive == true && w.Term.Id == selectedTerm.Id)
                    .SelectMany(s =>
                        s.EducationalClasses.Where(w => w.Term.Id == selectedTerm.Id && w.IsActive == true)
                        .Select(ss => ss.Professor).Where(w => w.IsActive == true && w.Term.Id == selectedTerm.Id).Distinct()
                     )
                    .Select(s => new Models.Professor
                    {
                        EducationalClasses = s.EducationalClasses.Select(ss => new Models.EducationalClass
                        {
                            CodeClass = ss.CodeClass,
                            CreationDate = ss.CreationDate,
                            DeclaringScoreDate = ss.DeclaringScoreDate,
                            EvaluationScore = ss.EvaluationScore,
                            HoldingExamDate = ss.HoldingExamDate,
                            Id = ss.Id,
                            IsActive = ss.IsActive,
                            LastModifiedDate = ss.LastModifiedDate,
                            LoadingQuestionDate = ss.LoadingQuestionDate,
                            Name = ss.Name,
                            OnlineHeldingCount = ss.OnlineHeldingCount,
                            OthersHeldingCount = ss.OthersHeldingCount,
                            PersentHeldingCount = ss.PersentHeldingCount,
                            ProfessorDelayAndEarlier = (int)ss.ProfessorDelayAndEarlier

                        }).ToList(), //Mapper.Map<ICollection<Model.Models.EducationalClass>, ICollection<Models.EducationalClass>>(s.EducationalClasses),
                        ProfessorScores = s.ProfessorScores.Select(ss => new Models.ProfessorScore
                        {
                            CurrentScore = ss.CurrentScore
                        }).ToList(),//Mapper.Map<ICollection<Model.Models.ProfessorScore>, ICollection<Models.ProfessorScore>>(s.ProfessorScores),
                        Name = s.Name,
                        Family = s.Family,
                        NationalCode = s.NationalCode,
                        ProfessorAccessStatus = GetCurrentCollegeRating((int)s.ProfessorCode, "p" + (int)IndicatorProfessorName.وضعیت_دسترسی_به_استاد_و_تعامل_با_دانشکده, selectedTerm.Id, collegeQS),
                        CollegeRate = 0,
                        ProfessorCode = s.ProfessorCode
                    }).ToList();
                TempData["collegeId"] = collegeQS;
            }
            if ((user == null || user.College == null) && TempData["model"] == null)
                return View();
            if (TempData["model"] != null)
            {
                var professors = (List<Models.Professor>)TempData["model"];
                ViewBag.CollegeId = TempData["collegeId"];
                TempData["model"] = null;
                TempData["collegeId"] = null;
                return View(professors);
            }
            else
            {
                var collegeId = user.College.Id;
                //var professorScoreList = _professorScoreService
                //    .GetMany(w => w.Score.Indicator.CountOfType == "p" + (int)IndicatorProfessorName.وضعیت_دسترسی_به_استاد_و_تعامل_با_دانشکده)
                //    .Where(w => w.EducationalGroup.College.Id == collegeId);
                ViewBag.CollegeId = collegeId;
                var x = DateTime.Now;
                var professors = _collegeService.Get(c => c.Id == collegeId).EducationalGroups.Where(w => w.IsActive == true && w.Term.Id == selectedTerm.Id)
                    .SelectMany(s => s.EducationalClasses.Where(w => w.Term.Id == selectedTerm.Id && w.IsActive == true)
                    .Select(ss => ss.Professor).Where(w => w.IsActive == true && w.Term.Id == selectedTerm.Id).Distinct())
                    .Select(s => new Models.Professor
                    {
                        //EducationalClasses = Mapper.Map<ICollection<Model.Models.EducationalClass>, ICollection<Models.EducationalClass>>(s.EducationalClasses),
                        //ProfessorScores = Mapper.Map<ICollection<Model.Models.ProfessorScore>, ICollection<Models.ProfessorScore>>(s.ProfessorScores),
                        EducationalClasses = s.EducationalClasses.Select(ss => new Models.EducationalClass
                        {
                            CodeClass = ss.CodeClass,
                            CreationDate = ss.CreationDate,
                            DeclaringScoreDate = ss.DeclaringScoreDate,
                            EvaluationScore = ss.EvaluationScore,
                            HoldingExamDate = ss.HoldingExamDate,
                            Id = ss.Id,
                            IsActive = ss.IsActive,
                            LastModifiedDate = ss.LastModifiedDate,
                            LoadingQuestionDate = ss.LoadingQuestionDate,
                            Name = ss.Name,
                            OnlineHeldingCount = ss.OnlineHeldingCount,
                            OthersHeldingCount = ss.OthersHeldingCount,
                            PersentHeldingCount = ss.PersentHeldingCount,
                            ProfessorDelayAndEarlier = (int)ss.ProfessorDelayAndEarlier

                        }).ToList(),
                        ProfessorScores = s.ProfessorScores.Select(sss => new Models.ProfessorScore { CurrentScore = sss.CurrentScore }).ToList(), //GetProfessorScoreByCollegeId(s.ProfessorCode, professorScoreList),
                        Name = s.Name,
                        Family = s.Family,
                        NationalCode = s.NationalCode,
                        ProfessorAccessStatus = GetCurrentCollegeRating((int)s.ProfessorCode, "p" + (int)IndicatorProfessorName.وضعیت_دسترسی_به_استاد_و_تعامل_با_دانشکده, selectedTerm.Id, collegeId),
                        CollegeRate = 0,
                        ProfessorCode = s.ProfessorCode
                    })
                    .ToList();
                var y = DateTime.Now - x;
                return View(professors);
            }
        }
        //[HttpPost]
        //public ActionResult ProfessorCollegeRatingForAdmin(string college)
        //{
        //    var collegeId = Convert.ToInt32(college);
        //    var x = _collegeService.Get(c => c.Id == collegeId);
        //    var professors = _collegeService.Get(c => c.Id == collegeId).EducationalGroups.SelectMany(s => s.EducationalClasses).Where(w=> w.Term == )
        //        .Select(s => s.Professor).Distinct()
        //            .Select(s => new Models.Professor
        //            {
        //                EducationalClasses = Mapper.Map<ICollection<Model.Models.EducationalClass>, ICollection<Models.EducationalClass>>(s.EducationalClasses),
        //                ProfessorScores = Mapper.Map<ICollection<Model.Models.ProfessorScore>, ICollection<Models.ProfessorScore>>(s.ProfessorScores),
        //                Name = s.Name,
        //                Family = s.Family,
        //                NationalCode = s.NationalCode,
        //                ProfessorAccessStatus = GetCurrentRating((int)s.ProfessorCode, "p" + (int)IndicatorProfessorName.وضعیت_دسترسی_به_استاد_و_تعامل_با_دانشکده, term.Id),
        //                CollegeRate = 0,
        //                ProfessorCode = s.ProfessorCode
        //            })
        //            .ToList();
        //    TempData["model"] = professors;
        //    TempData["collegeId"] = collegeId;
        //    return this.Json(string.Empty);
        //}

        [Authorize]
        [HttpPost]
        public ActionResult AddOrUpdateRate(string professorCode, string rateIndex, string countOfType, string groupId = null, string collegeId = null
            , string termId = null)
        {
            //var scoreBase = _indicatorService.Get(g => g.CountOfType == countOfType).Scores.OrderBy(o => o.Point)
            //    .ToList().ElementAt(Convert.ToInt32(rateIndex) - 1);
            var scoreBase = _indicatorService.Get(g => g.CountOfType == countOfType).Scores.FirstOrDefault(f => f.Point == Convert.ToInt32(rateIndex));
            var score = new Model.Models.Score();
            Mapper.Map(scoreBase, score);
            var pCode = Convert.ToInt32(professorCode);
            var tid = Convert.ToInt32(termId);
            var term = _termService.Get(g => g.Id == tid);
            var professorBase = _professorService.Get(g => g.ProfessorCode == pCode && g.Term.Id == tid);
            //var currentterm = GetCurrentTerm();
            var currentScore = Convert.ToInt32(scoreBase.Point * scoreBase.Indicator.Ratio.Point);

            var professor = new Model.Models.Professor();
            Mapper.Map(professorBase, professor);

            if (string.IsNullOrEmpty(groupId))
            {
                var groups = professor.EducationalClasses.Select(s => s.EducationalGroup).Distinct().ToList();
                if (string.IsNullOrEmpty(collegeId))
                {
                    foreach (var group in groups)
                    {
                        var existing = _professorScoreService.Get(g => g.Professor.Id == professor.Id
                        && g.Term.Id == term.Id
                        && g.Score.Indicator.CountOfType == countOfType
                        && g.EducationalGroup.Id == group.Id);
                        var newPs = new Model.Models.ProfessorScore
                        {
                            Professor = professor,
                            Score = score,
                            Term = term,
                            CurrentScore = currentScore,
                            EducationalGroup = group
                        };
                        if (existing == null)
                            _professorScoreService.Add(newPs);
                        else
                        {
                            newPs.Id = existing.Id;
                            _professorScoreService.Update(newPs);
                        }
                    }
                }
                else
                {
                    foreach (var group in groups)
                    {
                        if (group.College.Id == Convert.ToInt32(collegeId))
                        {
                            var existing = _professorScoreService.Get(g => g.Professor.Id == professor.Id
                            && g.Term.Id == term.Id
                            && g.Score.Indicator.CountOfType == countOfType
                            && g.EducationalGroup.Id == group.Id);
                            var newPs = new Model.Models.ProfessorScore
                            {
                                Professor = professor,
                                Score = score,
                                Term = term,
                                CurrentScore = currentScore,
                                EducationalGroup = group
                            };
                            if (existing == null)
                                _professorScoreService.Add(newPs);
                            else
                            {
                                newPs.Id = existing.Id;
                                _professorScoreService.Update(newPs);
                            }
                        }
                    }
                }
            }
            else
            {
                var gCode = Convert.ToInt32(groupId);
                var group = _educationalGroupService.Get(g => g.EducationalGroupCode == gCode && g.Term.Id == term.Id);
                var existing = _professorScoreService.Get(g => g.Professor.Id == professor.Id
                    && g.Term.Id == term.Id
                    && g.Score.Indicator.CountOfType == countOfType
                    && g.EducationalGroup.Id == group.Id);
                var newPs = new Model.Models.ProfessorScore
                {
                    Professor = professor,
                    Score = score,
                    Term = term,
                    CurrentScore = currentScore,
                    EducationalGroup = group
                };
                if (existing == null)
                    _professorScoreService.Add(newPs);
                else
                {
                    newPs.Id = existing.Id;
                    _professorScoreService.Update(newPs);
                }
            }
            return new JsonResult { Data = currentScore };
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteCollegeRate(string code, string collegeId, string termId)
        {
            var professorCode = Convert.ToInt32(code);
            var tid = Convert.ToInt32(termId);
            var term = _termService.Get(g => g.Id == tid);
            var professor = _professorService.Get(g => g.ProfessorCode == professorCode && g.Term.Id == tid);
            professor.ProfessorAccessStatus = null;
            _professorService.Update(professor);
            var scores = _professorScoreService.GetMany(g =>
            g.Professor.ProfessorCode == professorCode
            && g.Term.Id == term.Id && g.Score.Indicator.CountOfType == "p" + (int)IndicatorProfessorName.وضعیت_دسترسی_به_استاد_و_تعامل_با_دانشکده).ToList();
            if (scores != null && scores.Count() > 0)
                foreach (var score in scores)
                    if (score.EducationalGroup.College.Id == Convert.ToInt32(collegeId))
                        _professorScoreService.Delete(score);
            return null;
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteGroupManagerRate(string code, string groupId, string termId)
        {
            var professorCode = Convert.ToInt32(code);
            var tid = Convert.ToInt32(termId);
            var term = _termService.Get(g => g.Id == tid);
            var professor = _professorService.Get(g => g.ProfessorCode == professorCode && g.Term.Id == tid);
            var gid = Convert.ToInt32(groupId);
            professor.GroupMangerComments = null;
            _professorService.Update(professor);
            var score = _professorScoreService.Get(g =>
            g.Professor.ProfessorCode == professorCode
            && g.Term.Id == term.Id && g.Score.Indicator.CountOfType == "p" + (int)IndicatorProfessorName.نظر_مدیر_گروه
            && g.EducationalGroup.EducationalGroupCode == gid
            );
            _professorScoreService.Delete(score);
            return null;
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteDeputyRate(string code, string termId)
        {
            var professorCode = Convert.ToInt32(code);
            var tid = Convert.ToInt32(termId);
            var term = _termService.Get(g => g.Id == tid);
            var professor = _professorService.Get(g => g.ProfessorCode == professorCode && g.Term.Id == tid);
            professor.DeputyComments = null;
            _professorService.Update(professor);
            var scores = _professorScoreService.GetMany(g => g.Professor.ProfessorCode == professorCode
                && g.Term.Id == term.Id && g.Score.Indicator.CountOfType == "p" + (int)IndicatorProfessorName.نظر_معاون_آموزشی).ToList();
            foreach (var score in scores)
                _professorScoreService.Delete(score);
            return null;
        }

        public List<Models.ProfessorScore> GetProfessorScoreByCollegeId(int? professorCode, IEnumerable<ProfessorScore> professorScoreList)
        {
            if (professorCode != null)
                return professorScoreList
                        .Where(w => w.Professor.ProfessorCode == professorCode)
                    .Select(s => new Models.ProfessorScore
                    {
                        CurrentScore = s.CurrentScore,
                        Id = s.Id,
                    //Score = Mapper.Map<Models.Score>(s.Score),
                    //EducationalGroup = new Models.EducationalGroup {  },
                    //Professor = new Models.Professor { },
                    //Term = Mapper.Map<Models.Term>(s.Term)
                }).ToList();
            return null;
        }

        public ActionResult DeputyRating()
        {
            SetViewBag("ثبت نظر معاون آموزشی در خصوص اساتید", "DeputyRating");
            ViewBag.TermList = new SelectList(_termService.GetAll().OrderByDescending(o => o.TermCode), "Id", "Name");

            if (string.IsNullOrEmpty(Request.QueryString["termId"]))
                return View();

            var user = (Models.User)Session["UserInfo"];
            Term selectedTerm = null;
            if (string.IsNullOrEmpty(Request.QueryString["termId"]))
                selectedTerm = GetCurrentTerm();
            else
            {
                var termId = Convert.ToInt32(Request.QueryString["termId"]);
                selectedTerm = _termService.Get(g => g.Id == termId);
            }
            ViewBag.TermId = selectedTerm.Id;

            var professors = _educationalClassService.GetMany(g => g.Term.Id == selectedTerm.Id).Select(s => s.Professor).Distinct().Select(s => new Models.Professor
            {
                Name = s.Name,
                Family = s.Family,
                NationalCode = s.NationalCode,
                DeputyComments = GetCurrentGroupManagerRating((int)s.ProfessorCode, "p" + (int)IndicatorProfessorName.نظر_معاون_آموزشی, selectedTerm.Id),
                ProfessorCode = s.ProfessorCode
            }).ToList();

            ViewBag.Scores = _indicatorService.Get(g => g.CountOfType == "p" + (int)IndicatorProfessorName.نظر_معاون_آموزشی).Scores.OrderByDescending(o => o.Point).ToList();
            return View(professors);
        }

        #region Helper Methodes
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
        [HttpPost]
        public JsonResult GetGroupsOfCollege(int id = 0, int term = 0)
        {

            if (term > 0)
                TempData["SelectedTermId"] = term;
            if (TempData["SelectedTermId"] != null)
            {
                var termId = (int)TempData["SelectedTermId"];
                if (id > 0)
                    return Json(_educationalGroupService.GetMany(w => w.College.Id == id && w.Term.Id == termId
                    && !StaticValue.IneligibleEducationalGroupCodes.Contains((int)w.EducationalGroupCode))
                    .Select(s => new { Name = s.Name, Code = s.EducationalGroupCode }));
                //return Json(_educationalGroupService.GetAll().Where(w => w.College.Id == id && w.Term.Id == termId
                //    && !StaticValue.IneligibleEducationalGroupCodes.Contains((int)w.EducationalGroupCode))
                //        .Select(s => new { Name = s.Name, Code = s.EducationalGroupCode }));
            }
            return null;
        }

        private int GetCurrentAccessStatus(int professorCode)
        {
            var currentTermId = GetCurrentTerm().Id;
            var professorScore = _professorScoreService.GetMany(g =>
            g.Professor.ProfessorCode == professorCode
            && g.Term.Id == currentTermId
            && g.Score.Indicator.Name == "وضعیت دسترسی به استاد و تعامل با دانشکده"
                    ).FirstOrDefault();
            if (professorScore != null)
                return (int)professorScore.Score.Point;
            return 0;
        }
        private int? GetCurrentGroupManagerRating(int professorCode, string countOfType, int termId, int groupCode = 0)
        {
            //var currentTermId = GetCurrentTerm().Id;
            var professorScore = _professorScoreService.GetMany(g =>
            g.Professor.ProfessorCode == professorCode
            && g.Term.Id == termId
            && g.Score.Indicator.CountOfType == countOfType
            && (groupCode == 0 || g.EducationalGroup.EducationalGroupCode == groupCode)
                    ).FirstOrDefault();
            if (professorScore != null)
                return (int)professorScore.CurrentScore;
            return null;
        }
        private int GetCurrentCollegeRating(int professorCode, string countOfType, int termId, int collegeId)
        {
            var professorScore = _professorScoreService.GetMany(g =>
            g.Professor.ProfessorCode == professorCode
            && g.Professor.Term.Id == termId
            && g.Term.Id == termId
            && g.Score.Indicator.CountOfType == countOfType
            && g.EducationalGroup.College.Id == collegeId
                    ).FirstOrDefault();
            if (professorScore != null)
                return (int)professorScore.CurrentScore;
            return 0;
        }

        [HttpPost]
        public int GetCurrentStar(string currentRateData, string countOfType)
        {
            var scores = _indicatorService.Get(g => g.CountOfType == countOfType).Scores.OrderBy(o => o.Point).ToList();
            var currentRate = Convert.ToInt32(currentRateData);
            var scorePoint = currentRate / scores.FirstOrDefault().Indicator.Ratio.Point;
            //return scores.IndexOf(scores.FirstOrDefault(w => w.Point == scorePoint)) + 1;
            return (int)scorePoint;
        }
        [HttpPost]
        public int GetIndicatorRatio(string countOfType)
        {
            return (int)_indicatorService.Get(g => g.CountOfType == countOfType).Ratio.Point;
        }

        private Model.Models.Term GetCurrentTerm()
        {
            var lt = _termService.Get(g => g.IsCurrentTerm);
            var ct = new Model.Models.Term();
            Mapper.Map(lt, ct);
            return ct;
        }

        [HttpGet]
        public JsonResult GetGroupsByTermId(string termId)
        {
            if (!string.IsNullOrEmpty(termId))
            {
                var tid = Convert.ToInt32(termId);
                var res = _educationalGroupService.GetAll().Where(w => w.Term.Id == tid).Select(s => new { s.Name, s.EducationalGroupCode }).ToList();
                return new JsonResult { Data = res, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return null;
        }
        #endregion
    }
}