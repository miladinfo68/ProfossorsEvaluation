using AutoMapper;
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
    [Authorize]
    public class EducationalGroupController : Controller
    {
        private IEducationalGroupService _educationalGroupService;
        private IEducationalGroupScoreService _educationalGroupScoreService;
        private ICollegeService _collegeService;
        private ITermService _termService;
        private IIndicatorService _indicatorService;
        private IScoreService _scoreService;
        private IRoleAccessService _roleAccessService;
        private IUserRoleService _userRoleService;
        private DapperService _reportService;
        private ILogService _logService;

        public EducationalGroupController(
            IEducationalGroupService educationalGroupService
            , ITermService termService
            , IIndicatorService indicatorService
            , IEducationalGroupScoreService educationalGroupScoreService
            , IScoreService scoreService
            , ICollegeService collegeService
            , IRoleAccessService roleAccessService
            , IUserRoleService userRoleService
            , ILogService logService
            )
        {
            _educationalGroupService = educationalGroupService;
            _termService = termService;
            _indicatorService = indicatorService;
            _educationalGroupScoreService = educationalGroupScoreService;
            _scoreService = scoreService;
            _collegeService = collegeService;
            _reportService = new DapperService(educationalGroupService);
            _roleAccessService = roleAccessService;
            _userRoleService = userRoleService;
            _logService = logService;
        }


        public ActionResult RankOfEducationalGroupInUniversity(string term = null)
        {
            SetViewBag(title: "رتبه بندی گروه ها در سطح دانشگاه", menuItem: "RankOfEducationalGroupInUniversity");
            //// Old Methode Start
            //var counter = 0;
            //var groups = _educationalGroupService.GetAll()
            //    .OrderByDescending(o => o.EducationalGroupScores.Sum(s => s.CurrentScore ) + o.EducationalClasses.Sum(s=> s.Professor.ProfessorScores.Sum(ss=> ss.CurrentScore)))
            //    .Select(s => new Web.Models.EducationalGroup
            //{
            //    EducationalGroupCode = s.EducationalGroupCode,
            //    Name = s.Name,
            //    EducationalGroupScores = Mapper.Map<List<Models.EducationalGroupScore>>(s.EducationalGroupScores),
            //    EducationalClasses = Mapper.Map<List<Models.EducationalClass>>(s.EducationalClasses),
            //    College = Mapper.Map<Models.College>(s.College),
            //    RankInUniversity = ++counter
            //}).ToList();
            //// Old Methode End
            var termId = 0;
            ViewBag.TermList = new SelectList(_termService.GetAll().OrderByDescending(o => o.TermCode), "Id", "Name");
            if (!string.IsNullOrEmpty(term) && int.TryParse(term, out termId) && termId > 0)
            {
                var model = _reportService.GetGroupReport(termId).General.ToList();
                foreach (var item in model)
                    item.RowNumber = model.IndexOf(item) + 1;
                return View(model);
            }
            return View();
        }

        public ActionResult RankOfEducationalGroupInCollege(string id = null, string term = null)
        {
            SetViewBag(title: "رتبه بندی گروه ها در سطح دانشکده ها", menuItem: "RankOfEducationalGroupInCollege");
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
                //var counter = 0;
                //var groups = _educationalGroupService.GetMany(g=> g.College.Id == collegeId).Select(s=> new Models.EducationalGroup {
                //    Id = s.Id,
                //    EducationalGroupCode = s.EducationalGroupCode,
                //    Name = s.Name,
                //    EducationalGroupScores = Mapper.Map<List<Models.EducationalGroupScore>>(s.EducationalGroupScores),
                //    EducationalClasses = Mapper.Map<List<Models.EducationalClass>>(s.EducationalClasses),
                //    GroupManger = Mapper.Map<Models.Professor>(s.GroupManger),
                //    College = Mapper.Map<Models.College>(s.College),
                //    RankInUniversity = ++counter
                //})
                //.ToList();
                //return View(groups);
                //// Old Methode End
                var currentTerm = GetCurrentTerm();
                var collegeList = new List<int>();
                collegeList.Add(collegeId);
                var model = _reportService.GetGroupReport(termId, collegeList: collegeList, allColleges: false).General.ToList();
                foreach (var item in model)
                    item.RowNumber = model.IndexOf(item) + 1;
                return View(model);
            }
            return View();
        }

        public ActionResult ViewGroupRankInUniversityDetails(int id = 0, int termId = 0)
        {
            //var model = new Models.EducationalGroupRankDetails();
            //if (id > 0)
            //{
            //    model.EducationalGroupScores = Mapper.Map<List<Model.Models.EducationalGroupScore>, List<Models.EducationalGroupScore>>(_educationalGroupScoreService.GetMany(g=> g.EducationalGroup.EducationalGroupCode == id).ToList());
            //    model.EducationalGroupInfo = Mapper.Map<Model.Models.EducationalGroup, Models.EducationalGroup>(_educationalGroupService.Get(g => g.EducationalGroupCode == id));
            //}
            //return PartialView(model);
            if (termId == 0)
                termId = GetCurrentTerm().Id;
            var model = _reportService.GetGroupReport(termId).Detial.Where(w => w.GroupId == id).ToList();
            return PartialView(model);
        }


        public ActionResult EducationalGroupManagerPresence()
        {
            SetViewBag(title: "مدیریت وضعیت حضور مدیران گروه ها", menuItem: "EducationalGroupManagerPresence");
            ViewBag.CollegeList = new SelectList(_collegeService.GetAll(), "CollegeCode", "Name");
            ViewBag.TermList = new SelectList(_termService.GetAll().OrderByDescending(o => o.TermCode), "Id", "Name");
            var onlineIndicator = _indicatorService.Get(g => g.CountOfType == "g" + (int)IndicatorGroupName.وضعیت_حضور_آنلاین_مدیر_گروه);
            var physicalIndicator = _indicatorService.Get(g => g.CountOfType == "g" + (int)IndicatorGroupName.وضعیت_حضور_فیزیکی_مدیران_گروه);
            var otherIndicator = _indicatorService.Get(g => g.CountOfType == "g" + (int)IndicatorGroupName.وضعیت_سایر_جلسات_حضور_مدیر_گروه);
            var OnlineHolidaysIndicator = _indicatorService.Get(g => g.CountOfType == "g" + (int)IndicatorGroupName.تعطیلات_رسمی_کلاس_آنلاین_مدیر_گروه);
            var OfflineHolidaysIndicator = _indicatorService.Get(g => g.CountOfType == "g" + (int)IndicatorGroupName.تعطیلات_رسمی_کلاس_فیزیکی_مدیر_گروه);
            var EducationalAndResearchCouncilIndicator = _indicatorService.Get(g => g.CountOfType == "g" + (int)IndicatorGroupName.جلسات_شورای_آموزشی_پژوهشی);
            ViewBag.onlineScores = onlineIndicator.Scores.ToList();
            ViewBag.physicalScores = physicalIndicator.Scores.ToList();
            ViewBag.otherScores = otherIndicator.Scores.ToList();

            ViewBag.OnlineHolidays = OnlineHolidaysIndicator.Scores.ToList();
            ViewBag.OfflineHolidays = OfflineHolidaysIndicator.Scores.ToList();
            ViewBag.EducationalAndResearchCouncil = EducationalAndResearchCouncilIndicator.Scores.ToList();

            var user = (Models.User)Session["UserInfo"];
            var collegeQS = 0;
            Term selectedTerm = null;
            if (string.IsNullOrEmpty(Request.QueryString["termId"]))
                return View();
            //selectedTerm = GetCurrentTerm();
            else
            {
                var termId = Convert.ToInt32(Request.QueryString["termId"]);
                selectedTerm = _termService.Get(g => g.Id == termId);
            }
            ViewBag.TermId = selectedTerm.Id;
            if (Request.QueryString["college"] != null && int.TryParse(Request.QueryString["college"], out collegeQS))
            {
                var groups = _educationalGroupService.GetMany(g => g.College.CollegeCode == collegeQS && g.Term.Id == selectedTerm.Id
                    && !StaticValue.IneligibleEducationalGroupCodes.Contains((int)g.EducationalGroupCode)).Select(s => new EducationalGroup
                    {
                        EducationalGroupCode = s.EducationalGroupCode,
                        Name = s.Name,
                        GroupManger = s.GroupManger ?? new Professor(),
                        OnlinePresenceTime = GetCurrentRating((int)s.EducationalGroupCode, "g" + (int)IndicatorGroupName.وضعیت_حضور_آنلاین_مدیر_گروه, selectedTerm.Id) / onlineIndicator.Ratio.Point,
                        PhysicalPresenceTime = GetCurrentRating((int)s.EducationalGroupCode, "g" + (int)IndicatorGroupName.وضعیت_حضور_فیزیکی_مدیران_گروه, selectedTerm.Id) / physicalIndicator.Ratio.Point,
                        OtherPresenceTime = GetCurrentRating((int)s.EducationalGroupCode, "g" + (int)IndicatorGroupName.وضعیت_سایر_جلسات_حضور_مدیر_گروه, selectedTerm.Id) / otherIndicator.Ratio.Point,
                        OnlineHolidays = GetCurrentRating((int)s.EducationalGroupCode, "g" + (int)IndicatorGroupName.تعطیلات_رسمی_کلاس_آنلاین_مدیر_گروه, selectedTerm.Id) / OnlineHolidaysIndicator.Ratio.Point,
                        OfflineHolidays = GetCurrentRating((int)s.EducationalGroupCode, "g" + (int)IndicatorGroupName.تعطیلات_رسمی_کلاس_فیزیکی_مدیر_گروه, selectedTerm.Id) / OfflineHolidaysIndicator.Ratio.Point,
                        EducationalAndResearchCouncil = GetCurrentRating((int)s.EducationalGroupCode, "g" + (int)IndicatorGroupName.جلسات_شورای_آموزشی_پژوهشی, selectedTerm.Id) / EducationalAndResearchCouncilIndicator.Ratio.Point,
                        Id = s.Id
                    }).ToList();
                TempData["model"] = groups;
                TempData["collegeId"] = collegeQS;
            }
            if ((user == null || user.College == null) && TempData["model"] == null)
                return View();
            if (TempData["model"] != null)
            {
                var groups = (List<Model.Models.EducationalGroup>)TempData["model"];
                ViewBag.CollegeId = TempData["collegeId"];
                TempData["model"] = null;
                TempData["collegeId"] = null;
                return View(groups);
            }
            else
            {
                var collegeCode = user.College.Id;
                var groups = _educationalGroupService.GetMany(g => g.College.CollegeCode == collegeCode && g.Term.Id == selectedTerm.Id
                && !StaticValue.IneligibleEducationalGroupCodes.Contains((int)g.EducationalGroupCode)
                ).ToList();
                return View(groups);
            }
        }
        //[HttpPost]
        //public ActionResult EducationalGroupManagerPresenceForAdmin(string college)
        //{
        //    var collegeId = Convert.ToInt32(college);
        //    var groups = _educationalGroupService.GetMany(g => g.College.CollegeCode == collegeId).ToList();
        //    TempData["model"] = groups;
        //    TempData["collegeId"] = collegeId;
        //    return this.Json(string.Empty);
        //}
        public ActionResult EditEducationalGroupManagerPresence()
        {
            var groupId = 0;
            var termId = 0;
            if (int.TryParse(Request.QueryString["group"], out groupId) && int.TryParse(Request.QueryString["termId"], out termId))
            {
                var group = _educationalGroupService.Get(g => g.EducationalGroupCode == groupId && g.Term.Id == termId);
                var onlineIndicator = _indicatorService.GetMany(g => g.CountOfType == "g" + (int)IndicatorGroupName.وضعیت_حضور_آنلاین_مدیر_گروه).FirstOrDefault();
                var physicalIndicator = _indicatorService.GetMany(g => g.CountOfType == "g" + (int)IndicatorGroupName.وضعیت_حضور_فیزیکی_مدیران_گروه).FirstOrDefault();
                var otherIndicator = _indicatorService.GetMany(g => g.CountOfType == "g" + (int)IndicatorGroupName.وضعیت_سایر_جلسات_حضور_مدیر_گروه).FirstOrDefault();
                var onlineHolidaysIndicator = _indicatorService.GetMany(g => g.CountOfType == "g" + (int)IndicatorGroupName.تعطیلات_رسمی_کلاس_آنلاین_مدیر_گروه).FirstOrDefault();
                var offlineHolidaysIndicator = _indicatorService.GetMany(g => g.CountOfType == "g" + (int)IndicatorGroupName.تعطیلات_رسمی_کلاس_فیزیکی_مدیر_گروه).FirstOrDefault();
                var educationAndResearchCouncilIndicator = _indicatorService.GetMany(g => g.CountOfType == "g" + (int)IndicatorGroupName.جلسات_شورای_آموزشی_پژوهشی).FirstOrDefault();
                var ol = onlineIndicator.Scores.ToList();
                var pl = physicalIndicator.Scores.ToList();
                var othl = otherIndicator.Scores.ToList();
                var onhl = onlineHolidaysIndicator.Scores.ToList();
                var ofhl = offlineHolidaysIndicator.Scores.ToList();
                var erl = educationAndResearchCouncilIndicator.Scores.ToList();

                if (ol.Count() > 0)
                {
                    ViewBag.OnlineScoreList = new SelectList(ol, "Id", "Name");
                    var currentOnlineScore = GetCurrentRating(groupId, "g" + (int)IndicatorGroupName.وضعیت_حضور_آنلاین_مدیر_گروه, termId) / onlineIndicator.Ratio.Point;
                    if (currentOnlineScore != 0)
                        ViewBag.OnlineScoreId = ol.FirstOrDefault(f => f.Point == currentOnlineScore).Id;
                }
                if (pl.Count() > 0)
                {
                    ViewBag.PhysicalScoreList = new SelectList(pl, "Id", "Name");
                    var currentPhysicalScore = GetCurrentRating(groupId, "g" + (int)IndicatorGroupName.وضعیت_حضور_فیزیکی_مدیران_گروه, termId) / physicalIndicator.Ratio.Point;
                    if (currentPhysicalScore != 0)
                        ViewBag.PhysicalScoreId = pl.FirstOrDefault(f => f.Point == currentPhysicalScore).Id;
                }
                if (othl.Count() > 0)
                {
                    ViewBag.OtherScoreList = new SelectList(othl, "Id", "Name");
                    var currentOtherScore = GetCurrentRating(groupId, "g" + (int)IndicatorGroupName.وضعیت_سایر_جلسات_حضور_مدیر_گروه, termId) / otherIndicator.Ratio.Point;
                    if (currentOtherScore != 0)
                        ViewBag.OtherScoreId = othl.FirstOrDefault(f => f.Point == currentOtherScore).Id;
                }

                if(onhl.Count() > 0)
                {
                    ViewBag.OnlineHolidayScoreList = new SelectList(ol, "Id", "Name");
                    var currentOnlineHolidayScore = GetCurrentRating(groupId, "g" + (int)IndicatorGroupName.تعطیلات_رسمی_کلاس_آنلاین_مدیر_گروه, termId) / onlineHolidaysIndicator.Ratio.Point;
                    if (currentOnlineHolidayScore != 0)
                        ViewBag.OnlineHolidayScoreId = onhl.FirstOrDefault(f => f.Point == currentOnlineHolidayScore).Id;
                }
                if (ofhl.Count() > 0)
                {
                    ViewBag.offlineHolidayScoreList = new SelectList(ol, "Id", "Name");
                    var currentOfflineHolidayScore = GetCurrentRating(groupId, "g" + (int)IndicatorGroupName.تعطیلات_رسمی_کلاس_فیزیکی_مدیر_گروه, termId) / offlineHolidaysIndicator.Ratio.Point;
                    if (currentOfflineHolidayScore != 0)
                        ViewBag.OfflineHolidayScoreId = ofhl.FirstOrDefault(f => f.Point == currentOfflineHolidayScore).Id;
                }
                if (erl.Count() > 0)
                {
                    ViewBag.EducationalAndResearchCouncilScoreList = new SelectList(ol, "Id", "Name");
                    var currentEducationAndResearchCouncilScore = GetCurrentRating(groupId, "g" + (int)IndicatorGroupName.جلسات_شورای_آموزشی_پژوهشی, termId) / educationAndResearchCouncilIndicator.Ratio.Point;
                    if (currentEducationAndResearchCouncilScore != 0)
                        ViewBag.EducationAndResearchCouncilId = erl.FirstOrDefault(f => f.Point == currentEducationAndResearchCouncilScore).Id;
                }

                ViewBag.TermId = Request.QueryString["termId"];
                return PartialView(group);
            }
            else
                return null;
        }
        [HttpPost]
        public ActionResult UpdateEducationalGroupManagerPresence(string groupData, string onlineScore, string physicalScore, string otherScore, string onlineHolidayScore, string offlineHolidayScore, string educationalAndResearchCouncilScore, string termId)
        {
            var groupCode = Convert.ToInt32(groupData);
            var obj = _educationalGroupService.Get(g => g.EducationalGroupCode == groupCode);
            if (!string.IsNullOrEmpty(groupData)
                && (!string.IsNullOrEmpty(onlineScore) || !string.IsNullOrEmpty(physicalScore))
                )
            {
                var onlineScoreId = 0;
                var physicalScoreId = 0;
                var otherScoreId = 0;
                var onlineHolidaysScoreId = 0;
                var offlineHolidaysScoreId = 0;
                var educationAndResearchScoreId = 0;
                if (!string.IsNullOrEmpty(onlineScore))
                    onlineScoreId = Convert.ToInt32(onlineScore);
                if (!string.IsNullOrEmpty(physicalScore))
                    physicalScoreId = Convert.ToInt32(physicalScore);
                if (!string.IsNullOrEmpty(otherScore))
                    otherScoreId = Convert.ToInt32(otherScore);
                if(!string.IsNullOrEmpty(onlineHolidayScore))
                    onlineHolidaysScoreId = Convert.ToInt32(onlineHolidayScore);
                if (!string.IsNullOrEmpty(offlineHolidayScore))
                    offlineHolidaysScoreId = Convert.ToInt32(offlineHolidayScore);
                if (!string.IsNullOrEmpty(educationalAndResearchCouncilScore))
                    educationAndResearchScoreId = Convert.ToInt32(educationalAndResearchCouncilScore);
                //var term = GetCurrentTerm();
                var tid = Convert.ToInt32(termId);
                var term = _termService.Get(g => g.Id == tid);
                var onlineGroupScore = _educationalGroupScoreService.Get(g =>
                g.EducationalGroup.Id == obj.Id
                && g.Term.Id == term.Id
                && g.Score.Indicator.CountOfType == "g" + (int)IndicatorGroupName.وضعیت_حضور_آنلاین_مدیر_گروه
                );
                var physicalGroupScore = _educationalGroupScoreService.Get(g =>
                g.EducationalGroup.Id == obj.Id
                && g.Term.Id == term.Id
                && g.Score.Indicator.CountOfType == "g" + (int)IndicatorGroupName.وضعیت_حضور_فیزیکی_مدیران_گروه
                );
                var otherGroupScore = _educationalGroupScoreService.Get(g =>
                g.EducationalGroup.Id == obj.Id
                && g.Term.Id == term.Id
                && g.Score.Indicator.CountOfType == "g" + (int)IndicatorGroupName.وضعیت_سایر_جلسات_حضور_مدیر_گروه
                );

                var onlineHolidayGroupScore = _educationalGroupScoreService.Get(g =>
                g.EducationalGroup.Id == obj.Id
                && g.Term.Id == term.Id
                && g.Score.Indicator.CountOfType == "g" + (int)IndicatorGroupName.تعطیلات_رسمی_کلاس_آنلاین_مدیر_گروه
                );
                var offlineHolidayGroupScore = _educationalGroupScoreService.Get(g =>
                g.EducationalGroup.Id == obj.Id
                && g.Term.Id == term.Id
                && g.Score.Indicator.CountOfType == "g" + (int)IndicatorGroupName.تعطیلات_رسمی_کلاس_فیزیکی_مدیر_گروه
                );
                var educationAndResearchCouncilGroupScore = _educationalGroupScoreService.Get(g =>
                g.EducationalGroup.Id == obj.Id
                && g.Term.Id == term.Id
                && g.Score.Indicator.CountOfType == "g" + (int)IndicatorGroupName.جلسات_شورای_آموزشی_پژوهشی
                );

                var onlineScoreObj = _scoreService.Get(g => g.Id == onlineScoreId);
                var physicalScoreObj = _scoreService.Get(g => g.Id == physicalScoreId);
                var otherScoreObj = _scoreService.Get(g => g.Id == otherScoreId);

                var onlineHolidaysScoreObj = _scoreService.Get(g => g.Id == onlineHolidaysScoreId);
                var offlineHolidaysScoreObj = _scoreService.Get(g => g.Id == offlineHolidaysScoreId);
                var educationAndResearchCouncilScoreObj = _scoreService.Get(g => g.Id == educationAndResearchScoreId);

                if (onlineScoreId > 0)
                {
                    if (onlineGroupScore == null)
                    {
                        _educationalGroupScoreService.Add(new Model.Models.EducationalGroupScore
                        {
                            EducationalGroup = obj,
                            Score = onlineScoreObj,
                            Term = term,
                            CurrentScore = (int)(onlineScoreObj.Point * onlineScoreObj.Indicator.Ratio.Point)
                        });
                    }
                    else
                    {
                        onlineGroupScore.Score = onlineScoreObj;
                        onlineGroupScore.CurrentScore = (int)(onlineScoreObj.Point * onlineScoreObj.Indicator.Ratio.Point);
                        _educationalGroupScoreService.Update(onlineGroupScore);
                    }
                    obj.OnlinePresenceTime = Convert.ToInt32(onlineScoreObj.Point);
                }
                else if (onlineGroupScore != null)
                    _educationalGroupScoreService.Delete(onlineGroupScore);


                if (physicalScoreId > 0)
                {
                    if (physicalGroupScore == null)
                    {
                        _educationalGroupScoreService.Add(new Model.Models.EducationalGroupScore
                        {
                            EducationalGroup = obj,
                            Score = physicalScoreObj,
                            Term = term,
                            CurrentScore = (int)(physicalScoreObj.Point * physicalScoreObj.Indicator.Ratio.Point)
                        });
                    }
                    else
                    {
                        physicalGroupScore.Score = physicalScoreObj;
                        physicalGroupScore.CurrentScore = (int)(physicalScoreObj.Point * physicalScoreObj.Indicator.Ratio.Point);
                        _educationalGroupScoreService.Update(physicalGroupScore);
                    }
                    obj.PhysicalPresenceTime = Convert.ToInt32(physicalScoreObj.Point);
                }
                else if (physicalGroupScore != null)
                    _educationalGroupScoreService.Delete(physicalGroupScore);


                if (otherScoreId > 0)
                {
                    if (otherGroupScore == null)
                    {
                        _educationalGroupScoreService.Add(new Model.Models.EducationalGroupScore
                        {
                            EducationalGroup = obj,
                            Score = otherScoreObj,
                            Term = term,
                            CurrentScore = (int)(otherScoreObj.Point * otherScoreObj.Indicator.Ratio.Point)
                        });
                    }
                    else
                    {
                        otherGroupScore.Score = otherScoreObj;
                        otherGroupScore.CurrentScore = (int)(otherScoreObj.Point * otherScoreObj.Indicator.Ratio.Point);
                        _educationalGroupScoreService.Update(otherGroupScore);
                    }
                }
                else if (otherGroupScore != null)
                    _educationalGroupScoreService.Delete(otherGroupScore);


                if (onlineHolidaysScoreId > 0)
                {
                    if (onlineHolidayGroupScore == null)
                    {
                        _educationalGroupScoreService.Add(new Model.Models.EducationalGroupScore
                        {
                            EducationalGroup = obj,
                            Score = onlineHolidaysScoreObj,
                            Term = term,
                            CurrentScore = (int)(onlineHolidaysScoreObj.Point * onlineHolidaysScoreObj.Indicator.Ratio.Point)
                        });
                    }
                    else
                    {
                        onlineHolidayGroupScore.Score = onlineHolidaysScoreObj;
                        onlineHolidayGroupScore.CurrentScore = (int)(onlineHolidaysScoreObj.Point * onlineHolidaysScoreObj.Indicator.Ratio.Point);
                        _educationalGroupScoreService.Update(onlineHolidayGroupScore);
                    }
                }
                else if (onlineHolidayGroupScore != null)
                    _educationalGroupScoreService.Delete(onlineHolidayGroupScore);

                if (offlineHolidaysScoreId > 0)
                {
                    if (offlineHolidayGroupScore == null)
                    {
                        _educationalGroupScoreService.Add(new Model.Models.EducationalGroupScore
                        {
                            EducationalGroup = obj,
                            Score = offlineHolidaysScoreObj,
                            Term = term,
                            CurrentScore = (int)(offlineHolidaysScoreObj.Point * offlineHolidaysScoreObj.Indicator.Ratio.Point)
                        });
                    }
                    else
                    {
                        offlineHolidayGroupScore.Score = offlineHolidaysScoreObj;
                        offlineHolidayGroupScore.CurrentScore = (int)(offlineHolidaysScoreObj.Point * offlineHolidaysScoreObj.Indicator.Ratio.Point);
                        _educationalGroupScoreService.Update(offlineHolidayGroupScore);
                    }
                }
                else if (offlineHolidayGroupScore != null)
                    _educationalGroupScoreService.Delete(offlineHolidayGroupScore);

                if (educationAndResearchScoreId > 0)
                {
                    if (educationAndResearchCouncilGroupScore == null)
                    {
                        _educationalGroupScoreService.Add(new Model.Models.EducationalGroupScore
                        {
                            EducationalGroup = obj,
                            Score = educationAndResearchCouncilScoreObj,
                            Term = term,
                            CurrentScore = (int)(educationAndResearchCouncilScoreObj.Point * educationAndResearchCouncilScoreObj.Indicator.Ratio.Point)
                        });
                    }
                    else
                    {
                        educationAndResearchCouncilGroupScore.Score = educationAndResearchCouncilScoreObj;
                        educationAndResearchCouncilGroupScore.CurrentScore = (int)(educationAndResearchCouncilScoreObj.Point * educationAndResearchCouncilScoreObj.Indicator.Ratio.Point);
                        _educationalGroupScoreService.Update(educationAndResearchCouncilGroupScore);
                    }
                }
                else if (educationAndResearchCouncilGroupScore != null)
                    _educationalGroupScoreService.Delete(educationAndResearchCouncilGroupScore);






                _educationalGroupService.Update(obj);
            }
            var groups = _educationalGroupService.GetMany(g => g.College.CollegeCode == obj.College.Id).ToList();
            TempData["model"] = groups;
            TempData["collegeId"] = obj.College.Id;
            return null;
        }


        public ActionResult InTimePresentCurriculum()
        {
            SetViewBag("مدیریت وضعیت ارائه برنامه درسی", "InTimePresentCurriculum");
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
                TempData["model"] = _educationalGroupService.GetMany(g => g.College.Id == collegeQS && g.Term.Id == selectedTerm.Id
                && !StaticValue.IneligibleEducationalGroupCodes.Contains((int)g.EducationalGroupCode)
                ).ToList().Select(s => new EducationalGroup
                {
                    EducationalGroupCode = s.EducationalGroupCode,
                    Name = s.Name,
                    InTimePresentCurriculum = GetCurrentRating((int)s.EducationalGroupCode, "g" + (int)IndicatorGroupName.ارائه_به_موقع_برنامه_درسی_توسط_مدیرگروه_در_موعد_مقرر, selectedTerm.Id).ToString(),
                    EducationalGroupScores = s.EducationalGroupScores,
                    GroupManger = s.GroupManger,
                }).ToList();
                TempData["collegeId"] = collegeQS;
            }
            if ((user == null || user.College == null) && TempData["model"] == null)
                return View();
            var indicator = _indicatorService.Get(g => g.CountOfType == "g" + (int)IndicatorGroupName.ارائه_به_موقع_برنامه_درسی_توسط_مدیرگروه_در_موعد_مقرر);
            if (indicator != null)
            {
                ViewBag.Scores = indicator.Scores.OrderByDescending(o => o.Point).ToList();
                if (TempData["model"] != null)
                {
                    var groups = (List<Model.Models.EducationalGroup>)TempData["model"];
                    ViewBag.CollegeId = TempData["collegeId"];
                    TempData["model"] = null;
                    TempData["collegeId"] = null;
                    return View(groups);
                }
                else
                {
                    var groups = _educationalGroupService.GetMany(g => g.College.Id == user.College.Id && g.Term.Id == selectedTerm.Id
                    && !StaticValue.IneligibleEducationalGroupCodes.Contains((int)g.EducationalGroupCode)
                    ).ToList().Select(s => new EducationalGroup
                    {
                        EducationalGroupCode = s.EducationalGroupCode,
                        Name = s.Name,
                        InTimePresentCurriculum = GetCurrentRating((int)s.EducationalGroupCode, "g" + (int)IndicatorGroupName.ارائه_به_موقع_برنامه_درسی_توسط_مدیرگروه_در_موعد_مقرر, selectedTerm.Id).ToString(),
                        EducationalGroupScores = s.EducationalGroupScores,
                        GroupManger = s.GroupManger,
                    }).ToList();
                    ViewBag.CollegeId = user.College.Id;
                    return View(groups);
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult InTimePresentCurriculumForAdmin(string college)
        {
            var collegeId = Convert.ToInt32(college);
            var groups = _educationalGroupService.GetMany(g => g.College.CollegeCode == collegeId).ToList();
            TempData["model"] = groups;
            TempData["collegeId"] = collegeId;
            return this.Json(string.Empty);
        }
        [HttpPost]
        public ActionResult AddOrUpdateInTimePresent(string groupData, string rateIndex, string countOfType, string termId = null)
        {
            //var scoreBase = _indicatorService.Get(g => g.CountOfType == countOfType).Scores.OrderBy(o => o.Point)
            //    .ToList().ElementAt(Convert.ToInt32(rateIndex) - 1);
            var scoreBase = _indicatorService.Get(g => g.CountOfType == countOfType).Scores.FirstOrDefault(f => f.Point == Convert.ToInt32(rateIndex));
            var score = new Model.Models.Score();
            Mapper.Map(scoreBase, score);
            var groupCode = Convert.ToInt32(groupData);
            var tid = Convert.ToInt32(termId);
            var groupBase = _educationalGroupService.Get(g => g.EducationalGroupCode == groupCode && g.Term.Id == tid);
            //var currentterm = GetCurrentTerm();
            var term = _termService.Get(g => g.Id == tid);
            var currentScore = Convert.ToInt32(scoreBase.Point * scoreBase.Indicator.Ratio.Point);
            var group = new Model.Models.EducationalGroup();
            Mapper.Map(groupBase, group);
            var existing = _educationalGroupScoreService.Get(g => g.EducationalGroup.Id == group.Id
            && g.Term.Id == term.Id
            && g.Score.Indicator.CountOfType == countOfType);

            var newGS = new Model.Models.EducationalGroupScore
            {
                EducationalGroup = group,
                Score = score,
                Term = term,
                CurrentScore = currentScore
            };
            if (existing == null)
                _educationalGroupScoreService.Add(newGS);
            else
            {
                newGS.Id = existing.Id;
                _educationalGroupScoreService.Update(newGS);
            }
            group.InTimePresentCurriculum = score.Point.ToString();
            _educationalGroupService.Update(group);
            return new JsonResult { Data = currentScore };
        }

        [HttpPost]
        public ActionResult DeleteInTimePresent(string code, string termData = null)
        {
            var groupCode = Convert.ToInt32(code);
            var group = _educationalGroupService.Get(g => g.EducationalGroupCode == groupCode);
            Term selectedTerm = null;
            if (string.IsNullOrEmpty(termData))
                selectedTerm = GetCurrentTerm();
            else
            {
                var termId = Convert.ToInt32(termData);
                selectedTerm = _termService.Get(g => g.Id == termId);
            }
            group.InTimePresentCurriculum = null;
            _educationalGroupService.Update(group);
            var score = _educationalGroupScoreService.Get(g => g.EducationalGroup.EducationalGroupCode == groupCode && g.Term.Id == selectedTerm.Id && g.Score.Indicator.CountOfType == "g" + (int)IndicatorGroupName.ارائه_به_موقع_برنامه_درسی_توسط_مدیرگروه_در_موعد_مقرر);
            _educationalGroupScoreService.Delete(score);
            return null;
        }

        private int GetCurrentRating(int groupCode, string countOfType, int termId)
        {
            //var currentTermId = GetCurrentTerm().Id;
            var groupScore = _educationalGroupScoreService.GetMany(g =>
            g.EducationalGroup.EducationalGroupCode == groupCode
            && g.Term.Id == termId
            && g.Score.Indicator.CountOfType == countOfType
                    ).FirstOrDefault();
            if (groupScore != null)
                return (int)groupScore.CurrentScore;
            return 0;
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
        private Model.Models.Term GetCurrentTerm()
        {
            var lt = _termService.Get(g => g.IsCurrentTerm);
            var ct = new Model.Models.Term();
            Mapper.Map(lt, ct);
            return ct;
        }
        [HttpPost]
        public int GetCurrentStar(string currentRateData, string countOfType)
        {
            var scores = _indicatorService.Get(g => g.CountOfType == countOfType).Scores.OrderBy(o => o.Point).ToList();
            var currentRate = Convert.ToInt32(currentRateData);
            var scorePoint = currentRate / scores.FirstOrDefault().Indicator.Ratio.Point;
            return (int)scorePoint;
            //return scores.IndexOf(scores.FirstOrDefault(w => w.Point == currentRate)) + 1;
        }
        #endregion
    }
}