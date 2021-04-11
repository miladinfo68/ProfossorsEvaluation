using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using IAUECProfessorsEvaluation.Service.IService;
using IAUECProfessorsEvaluation.Web.Models.ViewModel;
using System.IO;
using IAUECProfessorsEvaluation.Data.ReportModel;
using IAUECProfessorsEvaluation.Service.Service;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;
using System.Web.Script.Serialization;
using IAUECProfessorsEvaluation.Web.Models.ReportModel;
using IAUECProfessorsEvaluation.Web.Models.Utility;
using NPOI.HSSF.UserModel;
using CollegeReportModel = IAUECProfessorsEvaluation.Data.ReportModel.CollegeReportModel;
using AutoMapper;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Service.Sync;
using IAUECProfessorsEvaluation.Core.Helper;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace IAUECProfessorsEvaluation.Web.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        #region Fields
        private ICollegeService _collegeService;
        private IEducationalGroupService _educationalGroupService;
        private IEducationalClassService _educationalClassService;
        private IIndicatorService _indicatorService;
        private ITermService _termService;
        private IProfessorService _professorService;
        private IProfessorScoreService _professorScoreService;
        private DapperService _reportService;
        private IRoleAccessService _roleAccessService;
        private IUserRoleService _userRoleService;
        private ILogService _logService;
        private int? _userCollegeId;
        private int? _userGroupCode;
        #endregion

        #region Ctor
        public ReportsController(
            ICollegeService collegeService,
            IEducationalGroupService educationalGroupService,
            IEducationalClassService educationalClassService,
            IIndicatorService indicatorService,
            ITermService termService,
            IEducationalGroupScoreService educationalGroupScoreService,
            IProfessorScoreService professorScoreService,
            IProfessorService professorService,
            IRoleAccessService roleAccessService,
            IUserRoleService userRoleService,
            ILogService logService
            )
        {

            _collegeService = collegeService;
            _educationalGroupService = educationalGroupService;
            _educationalClassService = educationalClassService;
            _indicatorService = indicatorService;
            _termService = termService;
            _professorService = professorService;
            _professorScoreService = professorScoreService;
            _reportService = new DapperService(educationalGroupService);
            _roleAccessService = roleAccessService;
            _userRoleService = userRoleService;


            _logService = logService;
        }
        #endregion


        public ActionResult GetColleges()
        {
            SetUser();

            if (_userCollegeId != null && _userCollegeId != 0)
            {
                var colleges = _collegeService.GetMany(x => x.IsActive == true && x.Id == _userCollegeId)
                    .Select(x => new SelectCustomeList { Id = x.Id, Name = x.Name }).ToList();
                return Json(colleges.OrderByDescending(x => x.Id), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var colleges = _collegeService.GetMany(x => x.IsActive == true)
                    .Select(x => new SelectCustomeList { Id = x.Id, Name = x.Name }).ToList();
                return Json(colleges.OrderByDescending(x => x.Id), JsonRequestBehavior.AllowGet);
            }
        }

        private void SetUser()
        {
            var user = (Models.User)Session["UserInfo"];
            if (user != null && user.College != null)
                _userCollegeId = user.College.Id;
            if (user != null && user.EducationalGroupCode != null)
                _userGroupCode = user.EducationalGroupCode;
        }

        public ActionResult GetGroups(string collegesList, string term, bool all)
        {
            SetUser();

            if (_userGroupCode != null && _userGroupCode != 0)
            {

                var termId = Convert.ToUInt32(term);
                List<SelectCustomeList> model;
                if (string.IsNullOrEmpty(collegesList) || all)
                {
                    model = _educationalGroupService.GetMany(x => x.IsActive == true && x.Term.Id == termId
                    && x.EducationalGroupCode == _userGroupCode)
                        .Select(x => new SelectCustomeList { Id = x.Id, Name = x.Name }).OrderBy(x => x.Name).ToList();
                }
                else
                {
                    var colleges = new JavaScriptSerializer().Deserialize<List<int>>(collegesList);
                    model = _educationalGroupService.GetMany(x =>
                        x.IsActive == true
                        && x.Term.Id == termId
                        && colleges.Contains(x.College.Id)
                        && x.EducationalGroupCode == _userGroupCode
                        )
                        .Select(x => new SelectCustomeList { Id = x.Id, Name = x.Name }).OrderBy(x => x.Name).ToList();
                }


                return Json(model, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var termId = Convert.ToUInt32(term);
                List<SelectCustomeList> model;
                if (string.IsNullOrEmpty(collegesList) || all)
                {
                    model = _educationalGroupService.GetMany(x => x.IsActive == true && x.Term.Id == termId)
                        .Select(x => new SelectCustomeList { Id = x.Id, Name = x.Name }).OrderBy(x => x.Name).ToList();
                }
                else
                {
                    var colleges = new JavaScriptSerializer().Deserialize<List<int>>(collegesList);
                    model = _educationalGroupService.GetMany(x => x.IsActive == true && x.Term.Id == termId && colleges.Contains(x.College.Id))
                        .Select(x => new SelectCustomeList { Id = x.Id, Name = x.Name }).OrderBy(x => x.Name).ToList();
                }


                return Json(model, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult GetProfessores(string groupList, string term, bool all)
        {


            var termId = Convert.ToUInt32(term);

            List<SelectCustomeList> model;
            if (string.IsNullOrEmpty(groupList) || all)
            {
                model = _professorService.GetMany(x => x.IsActive == true && x.Term.Id == termId)
                    .Select(x => new SelectCustomeList { Id = x.Id, Name = (x.Name + ' ' + x.Family) }).OrderBy(x => x.Name).ToList();
            }
            else
            {
                var groups = new JavaScriptSerializer().Deserialize<List<int>>(groupList);
                var professors = _educationalClassService
                    .GetMany(x => x.IsActive == true && groups.Contains(x.EducationalGroup.Id)).Select(x => x.Professor.Id).Distinct().ToList();

                model = _professorService.GetMany(x =>
                x.IsActive == true
                && x.Term.Id == termId
                && professors.Contains(x.Id)
                )
                   .Select(x => new SelectCustomeList { Id = x.Id, Name = (x.Name + ' ' + x.Family) }).OrderBy(x=>x.Name).ToList();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Duration = 600000)]
        public ActionResult Index()
        {
            SetUser();
            SetViewBag(title: "گزارش ساز", menuItem: "ReportsIndex");
            ViewBag.lastUpdate = _logService.LastUpdate();

            var model = new ReportingViewMoel();
            var indicators = _indicatorService.GetAllWithObjectTypeAndScore().Where(x => x.IsActive == true)
                .Select(
                    m => new IndicatorViewModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        ObjectTypeId = m.ObjectType.Id,
                        Scores = m.Scores.Where(x => x.IsActive == true).Select(
                            n => new ScoreViewModel { Id = n.Id, Name = n.Name }).ToList(),
                        CountOfType = string.IsNullOrEmpty(m.CountOfType) ? "" : m.CountOfType
                    }).ToList();



            model.IndicatorsForGroup = indicators.Where(x => x.ObjectTypeId == 2 && x.CountOfType.Contains('g')).ToList();
            model.IndicatorsForProfessor = indicators.Where(x => x.ObjectTypeId == 1 && x.CountOfType.Contains('p')).ToList();
            model.Terms = _termService.GetAll().Where(t => string.CompareOrdinal(t.TermCode, "96-97-2") >= 0).OrderByDescending(o => o.TermCode).Select(m => new TermViewModel { Id = m.Id, Name = m.Name, TermCode = m.TermCode }).ToList();
            model.ReportLevleType = ReportLevle.GetReportLeveles(_userCollegeId, _userGroupCode);
            model.SortingList = SortingViewModel.GetSortingList();
            return View(model);
        }

        public JsonResult ServerFiltering_GetColleges(string text)
        {

            List<CollegeViewModel> colleges = null;
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Contains(","))
                {
                    var textSplited = text.Split(',');
                    text = textSplited[(textSplited.Length - 1)];
                }

                colleges = _collegeService.GetAll()
                    .Where(x => x.IsActive == true)
                    .Select(x => new CollegeViewModel { Id = x.Id, Name = x.Name })
                    .Where(p => p.Name.Contains(text.Trim())).OrderBy(x => x.Name)
                    .ToList();
            }

            return Json(colleges, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Resualt(ReportingViewMoel model)
        {
            SetViewBag(title: "نتیجه گزارش", menuItem: "ReportsIndex");

            var flawIndicatiors = new List<string>();
            IEnumerable<Condition> groupCondition = new List<Condition>();
            IEnumerable<Condition> professoreCondition = new List<Condition>();
            if (model.TermId == null)
            {
                model.TermId = _termService.GetAll().OrderByDescending(x => x.Id).FirstOrDefault()?.Id;
            }




            var collegeList = new List<int>();
            var groupList = new List<int>();
            var professorList = new List<int>();
            var groupScoresList = new List<int>();
            var professorScoresList = new List<int>();
            var groupIndicatorList = new List<int>();
            var professorIndicatorList = new List<int>();

            foreach (var ig in model.IndicatorsForGroup)
            {
                if (ig.IsParticipation == true)
                {
                    if (ig.SelectedScore != null)
                        foreach (var gs in ig.SelectedScore)
                        {
                            if (gs != null)
                            {
                                groupScoresList.Add(gs.Value);
                            }
                        }
                    if (ig.IsParticipation != null)
                        groupIndicatorList.Add(ig.Id);

                }
            }

            foreach (var ip in model.IndicatorsForProfessor)
            {
                if (ip.IsParticipation == true)
                {
                    if (ip.SelectedScore != null)
                        foreach (var ps in ip.SelectedScore)
                        {
                            if (ps != null)
                            {
                                professorScoresList.Add(ps.Value);
                            }
                        }
                    if (ip.IsParticipation != null)
                        professorIndicatorList.Add(ip.Id);
                }

            }

            if (model.ReportLevel == 1)
            {
                if (model.ColList == null) model.ColList = new List<int>();

                var countOfCollege = _collegeService.GetMany(x => x.IsActive == true).Select(x => x.Id).Count();
                model.AllCollege = model.ColList.Count == countOfCollege;
            }
            else
            {
                if (model.ReportLevel == 2)
                {
                    if (model.ColList == null) model.ColList = new List<int>();
                    if (model.GrpList == null) model.GrpList = new List<int>();

                    var countOfCollege = _collegeService.GetMany(x => x.IsActive == true).Select(x => x.Id).Count();
                    var countOfGroup = _educationalGroupService.GetMany(x => x.IsActive == true).Select(x => x.Id).Count();
                    model.AllCollege = model.ColList.Count == countOfCollege;
                    model.AllGroup = model.GrpList.Count == countOfGroup;
                }
                else
                {
                    if (model.ColList == null) model.ColList = new List<int>();
                    if (model.GrpList == null) model.GrpList = new List<int>();
                    if (model.ProList == null) model.ProList = new List<int>();

                    var countOfCollege = _collegeService.GetMany(x => x.IsActive == true).Select(x => x.Id).Count();
                    var countOfGroup = _educationalGroupService.GetMany(x => x.IsActive == true).Select(x => x.Id).Count();
                    var countOfProfessor = _professorService.GetMany(x => x.IsActive == true && x.Term.Id == model.TermId).Select(x => x.Id).Count();
                    model.AllCollege = model.ColList.Count == countOfCollege;
                    model.AllGroup = model.GrpList.Count == countOfGroup;
                    model.AllProfessore = model.ProList.Count == countOfProfessor;
                }
            }



            if (!model.AllCollege)
                collegeList = model.ColList;
            if (!model.AllGroup)
                groupList = model.GrpList;
            if (!model.AllProfessore)
                professorList = model.ProList;


            switch (model.ReportLevel)
            {

                case 1:
                    CompleteCollegeResult collegeResualt;

                    var iAllColleges = true;
                    var allCollegeIndicators = true;
                    var allCollegeScores = true;

                    if (!model.AllCollege)
                        iAllColleges = false;


                    if (groupIndicatorList.Any())
                        allCollegeIndicators = false;

                    if (groupScoresList.Any())
                        allCollegeScores = false;

                    collegeResualt = _reportService.GetCollegeReport(model.TermId.Value,
                        groupIndicatorList, groupScoresList, collegeList, iAllColleges,
                        allCollegeIndicators, allCollegeScores, model.CountShow, model.OrderingType);

                    var groupIdInCollege = collegeResualt.Detial.Select(x => x.GroupId).ToList();


                    //TODO College Indicator


                    TempData["CollegeModel"] = collegeResualt.Detial.ToList();
                    return View("CollegeReport", collegeResualt.General.ToList());



                case 2:
                    CompleteGroupResult groupResualt;


                    var gAllColleges = true;
                    var gAllGroups = true;
                    var allGroupIndicators = true;
                    var allGroupScores = true;

                    if (!model.AllCollege)
                        gAllColleges = false;

                    if (!model.AllGroup)
                        gAllGroups = false;


                    if (groupIndicatorList.Any())
                        allGroupIndicators = false;
                    if (groupScoresList.Any())
                        allGroupScores = false;

                    groupResualt = _reportService.GetGroupReport(model.TermId.Value, groupIndicatorList
                        , groupScoresList, collegeList, groupList
                        , gAllColleges, gAllGroups, allGroupIndicators, allGroupScores, model.CountShow, model.OrderingType);

                    var qg = (from g in groupResualt.Detial
                              group g by g.GroupId
                        into groups
                              select groups);

                    var groupdetResualt = new List<GroupReportModel>();
                    var gInd = _indicatorService
                        .GetMany(x => x.IsActive == true)
                        .Where(y => !string.IsNullOrEmpty(y.CountOfType) && y.CountOfType.Contains('g'));
                    var gIndicators = new List<Model.Models.Indicator>();
                    gIndicators = allGroupIndicators ? gInd.ToList() : gInd.Where(x => groupIndicatorList.Contains(x.Id)).ToList();


                    qg.ForEach(g =>
                    {
                        var t = g.Select(x => x.IndicatorId);
                        var o = model
                        .IndicatorsForGroup
                        .FindAll(x => !t.Contains(x.Id)).Select(y => y.Id);

                        groupdetResualt.AddRange(g);

                        o.ForEach(x =>
                        {
                            var newItem = new GroupReportModel();
                            var item = g.FirstOrDefault();
                            if (item != null)
                            {
                                newItem.IndicatorId = x;
                                var indicator = gIndicators.FirstOrDefault(y => y.Id == x);
                                if (indicator != null)
                                {
                                    if (!flawIndicatiors.Exists(z => z == indicator.Name))
                                        flawIndicatiors.Add(indicator.Name);


                                    newItem.IndicatorName = indicator.Name;

                                    if (indicator.Ratio != null)
                                    {
                                        newItem.RatioName = indicator.Ratio.Name;
                                        newItem.RatioValue = indicator.Ratio.Point.Value;
                                    }
                                    newItem.AvgProfessorScoreGroup = item.AvgProfessorScoreGroup;
                                    newItem.GroupName = item.GroupName;
                                    newItem.GroupScore = item.GroupScore;
                                    newItem.CurrentScore = null;
                                    newItem.GroupId = item.GroupId;
                                    newItem.GroupCode = item.GroupCode;
                                    newItem.CollegeName = item.CollegeName;
                                    newItem.CollegeId = item.CollegeId;
                                    newItem.CollegeCode = item.CollegeCode;
                                    groupdetResualt.Add(newItem);
                                }
                            }

                        });
                    });
                    if (flawIndicatiors.Count > 0)
                        groupResualt.General.FirstOrDefault().FlawIndicatiors = flawIndicatiors;

                    TempData["GroupModel"] = groupdetResualt.ToList();
                    return View("GroupReport", groupResualt.General);

                case 3:


                    var allColleges = true;
                    var allGroups = true;
                    var allProfessors = true;
                    var allProfessorIndicators = true;
                    var allProfessorScores = true;


                    if (!model.AllCollege)
                        allColleges = false;

                    if (!model.AllGroup)
                        allGroups = false;

                    if (!model.AllProfessore)
                        allProfessors = false;

                    if (professorIndicatorList.Any())
                        allProfessorIndicators = false;
                    if (professorScoresList.Any())
                        allProfessorScores = false;

                    if (!model.AllCollege && model.AllGroup)
                    {
                        var groups = _educationalGroupService.GetMany(x => collegeList.Contains(x.College.Id)).Select(p => p.Id);
                        groupList.AddRange(groups);
                        var grp = groupList.Distinct();
                        groupList = grp.ToList();
                    }


                    var professorResualt = _reportService.GetProfessorReport(model.TermId.Value,
                        professorIndicatorList,
                        professorScoresList,
                        collegeList, groupList, professorList,
                        allColleges, allGroups, allProfessors,
                          allProfessorIndicators, allProfessorScores, model.CountShow, model.OrderingType);

                    var q = (from p in professorResualt.Detial
                             group p by p.ProfessorId
                        into professors
                             select professors);

                    var detResualt = new List<ProfessorDetialReportModel>();
                    var pindc = _indicatorService.GetMany(x => x.IsActive == true).Where(y => !string.IsNullOrEmpty(y.CountOfType) && y.CountOfType.Contains('p'));

                    var indicators = allProfessorIndicators ? pindc.ToList() : pindc.Where(x => professorIndicatorList.Contains(x.Id)).ToList();

                    q.ForEach(p =>
                    {
                        var t = p.Select(x => x.IndicatorId);
                        var o = model.IndicatorsForProfessor.FindAll(x => !t.Contains(x.Id)).Select(y => y.Id);

                        detResualt.AddRange(p);
                        o.ForEach(x =>
                        {
                            var item = p.FirstOrDefault();
                            var newItem = new ProfessorDetialReportModel();
                            if (item != null)
                            {
                                newItem.IndicatorId = x;
                                var indicator = indicators.FirstOrDefault(y => y.Id == x);
                                if (indicator != null)
                                {
                                    if (!flawIndicatiors.Exists(z => z == indicator.Name))
                                        flawIndicatiors.Add(indicator.Name);


                                    newItem.IndicatorName = indicator.Name;
                                    newItem.AvgScore = null;
                                    newItem.RationName = indicator.Ratio.Name;
                                    newItem.RationValue = indicator.Ratio.Point.Value;
                                    newItem.Name = item.Name;
                                    newItem.Family = item.Family;
                                    newItem.FullName = item.FullName;
                                    newItem.Gender = item.Gender;
                                    newItem.NationalCode = item.NationalCode;
                                    newItem.ProfessorId = item.ProfessorId;
                                    newItem.ProfessorCode = item.ProfessorCode;
                                    detResualt.Add(newItem);
                                }
                            }
                        });
                    });

                    TempData["ProfessorDetialResualt"] = detResualt;
                    TempData["ProfessorGeneralResualt"] = professorResualt.General;
                    TempData["ReportTermId"] = model.TermId.Value;
                    if (flawIndicatiors.Count > 0)
                        professorResualt.General.FirstOrDefault().FlawIndicatiors = flawIndicatiors;
                    ViewBag.TermId = model.TermId.Value;
                    return View("ProfessorReport", professorResualt.General);
                default:
                    break;
            }


            return Content("not Complete!!!");
        }
        
        public ActionResult ProfessorDetails(int professorId, int termId = 0)
        {

            //var id = Int32.Parse(professorId);

            var model = ((List<ProfessorDetialReportModel>)TempData["ProfessorDetialResualt"]).Where(p => p.ProfessorId == professorId).ToList();
            TempData.Keep();
            if (termId == 0)
                termId = _termService.Get(g => g.IsCurrentTerm).Id;
            ViewBag.TermId = termId;
            return PartialView(model);
        }

        public ActionResult ServerFiltering_GetEducationalGroups(string text, string colleges, bool allCollege)
        {
            List<GroupViewModel> groups = null;

            var selectedColleges = colleges.Split(',');
            var collegesSplited = new string[selectedColleges.Length];
            if (selectedColleges.Length > 0)
                for (var i = 0; i < (selectedColleges.Length - 1); i++)
                {
                    collegesSplited[i] = selectedColleges[i].Trim();
                }

            if (!string.IsNullOrEmpty(text))
            {
                if (text.Contains(","))
                {
                    var textSplited = text.Split(',');
                    text = textSplited[(textSplited.Length - 1)];
                }

                if (allCollege)
                {
                    groups = _educationalGroupService.GetAll()
                        .Where(x => x.IsActive == true)

                        .Select(x => new GroupViewModel { Id = x.Id, Name = x.Name })
                        .Where(p => p.Name.StartsWith(text.Trim()))
                        .ToList();
                }
                else
                {
                    groups = _educationalGroupService.GetAllWithCollege()
                        .Where(x => x.IsActive == true && collegesSplited.Contains(x.College.Name))
                        .Select(x => new GroupViewModel { Id = x.Id, Name = x.Name })
                        .Where(p => p.Name.StartsWith(text.Trim()))
                        .ToList();
                }
            }

            return Json(groups, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ServerFiltering_ProfessoreGroups(string text, string groups, bool allGroup, string colleges, bool allCollege)
        {
            List<ProfessoreViewModel> professores = null;



            if (!string.IsNullOrEmpty(text))
            {
                if (text.Contains(","))
                {
                    var textSplited = text.Split(',');
                    text = textSplited[(textSplited.Length - 1)];
                }

                if (allGroup)
                {
                    if (allCollege)
                    {
                        professores = _educationalClassService.GetAllWithProfessoreAndCollege()
                            .Where(x => x.IsActive == true
                                        && (x.Professor.Name.StartsWith(text.Trim())
                                            || x.Professor.Family.StartsWith(text.Trim())))

                            .Select(x =>
                                new ProfessoreViewModel { Id = x.Professor.Id, Name = string.Concat(x.Professor.Name + " " + x.Professor.Family) })

                            .DistinctBy(x => x.Id)
                            .ToList();
                    }
                    else
                    {
                        var selectedColleges = colleges.Split(',');
                        var collegesSplited = new string[selectedColleges.Length];
                        if (selectedColleges.Length > 0)
                            for (var i = 0; i < (selectedColleges.Length - 1); i++)
                            {
                                collegesSplited[i] = selectedColleges[i].Trim();
                            }
                        professores = _educationalClassService.GetAllWithProfessoreAndCollege()
                        .Where(x => x.IsActive == true
                                    && collegesSplited.Contains(x.EducationalGroup.College.Name)
                                    && (x.Professor.Name.StartsWith(text.Trim()) || x.Professor.Family.StartsWith(text.Trim())))
                        .Select(x =>
                            new ProfessoreViewModel { Id = x.Professor.Id, Name = string.Concat(x.Professor.Name + " " + x.Professor.Family) })

                        .DistinctBy(x => x.Id)
                        .ToList();
                    }


                }
                else
                {
                    var groupSelected = groups.Split(',');
                    var groupSplited = new string[groupSelected.Length];
                    if (groupSelected.Length > 0)
                        for (var i = 0; i < (groupSelected.Length - 1); i++)
                        {
                            groupSplited[i] = groupSelected[i].Trim();
                        }


                    professores = _educationalClassService.GetAllWithProfessoreAndCollege()
                        .Where(x => x.IsActive == true
                                    && (x.Professor.Name.StartsWith(text.Trim()) || x.Professor.Family.StartsWith(text.Trim()))
                                    && groupSplited.Contains(x.EducationalGroup.Name))
                        .Select(x =>
                            new ProfessoreViewModel { Id = x.Professor.Id, Name = string.Concat(x.Professor.Name + " " + x.Professor.Family) })
                        .DistinctBy(x => x.Id).ToList();

                }
            }
            return Json(professores, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Cascading_Get_Ordering(int level)
        {
            var indicator = _indicatorService.GetAllWithObjectTypeAndScore().Select(m => new IndicatorViewModel { Id = m.Id, Name = m.Name, ObjectTypeId = m.ObjectType.Id });
            switch (level)
            {
                case 1:
                    return Json(indicator.Where(m => m.ObjectTypeId == 2), JsonRequestBehavior.AllowGet);
                case 2:
                    return Json(indicator.Where(m => m.ObjectTypeId == 2), JsonRequestBehavior.AllowGet);
                case 3:
                    return Json(indicator.Where(m => m.ObjectTypeId == 1), JsonRequestBehavior.AllowGet);
                default:
                    return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        
        public void SetViewBag(string title = null, string menuItem = null)
        {
            ViewBag.Title = title;
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
        
        public ActionResult ProfessorDetailsPrint()
        {
            var detailsModel = ((List<ProfessorDetialReportModel>)TempData["ProfessorDetialResualt"]).OrderBy(x => x.IndicatorName).ToList();

            var generalModel = ((List<ProfessorDetialReportModel>)TempData["ProfessorGeneralResualt"]);
            var termId = (int)TempData["ReportTermId"];

            var query = generalModel.Join(detailsModel, x => x.ProfessorId, y => y.ProfessorId, (x, y) => new
            {
                AvgScore = y.AvgScore,
                IndicatorName = y.IndicatorName,
                FullName = y.FullName,
                ProfessorScore = x.ProfessorScore,
                NationalCode = y.NationalCode,
                ProfessorCode = y.ProfessorCode,
                WithoutRatio = y.AvgScore / y.RationValue,
                RatioName = y.RationName,
                ScoreName = (y.AvgScore / y.RationValue)?.GetScoreName(y.IndicatorId, y.ProfessorCode, termId),
                FirstName = y.Name,
                LastName = y.Family
            }).ToList();

            TempData.Keep();

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();
            sheet.IsRightToLeft = true;
            //Set Style
            ICellStyle style1 = workbook.CreateCellStyle();
            style1.FillForegroundColor = IndexedColors.DarkGreen.Index;
            style1.FillPattern = FillPattern.SolidForeground;
            style1.FillBackgroundColor = IndexedColors.DarkGreen.Index;
            style1.BorderLeft = BorderStyle.Thin;

            ICellStyle style2 = workbook.CreateCellStyle();
            style2.FillForegroundColor = IndexedColors.LightYellow.Index;
            style2.FillPattern = FillPattern.SolidForeground;
            style2.FillBackgroundColor = IndexedColors.LightYellow.Index;
            style2.BorderBottom = BorderStyle.Thin;
            style2.BorderTop = BorderStyle.Thin;
            style2.BottomBorderColor = IndexedColors.Grey25Percent.Index;
            style2.TopBorderColor = IndexedColors.Grey25Percent.Index;


            IFont font1 = workbook.CreateFont();
            font1.Color = IndexedColors.White.Index;
            //font1.IsItalic = true;
            font1.Boldweight = 400;
            //font1.Underline = FontUnderlineType.Double;
            font1.FontHeightInPoints = 15;

            //bind font with style 
            style1.SetFont(font1);

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 10 * 256);
            sheet.SetColumnWidth(1, 15 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 15 * 256);
            sheet.SetColumnWidth(4, 30 * 256);
            sheet.SetColumnWidth(5, 30 * 256);
            sheet.SetColumnWidth(6, 12 * 256);
            sheet.SetColumnWidth(7, 15 * 256);
            sheet.SetColumnWidth(8, 12 * 256);
            sheet.SetColumnWidth(9, 12 * 256);
            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //Set the column names in the header row
            var cell0 = headerRow.CreateCell(0);
            cell0.SetCellValue("کد استاد");
            var cell1 = headerRow.CreateCell(1);
            cell1.SetCellValue("نام");
            var cell2 = headerRow.CreateCell(2);
            cell2.SetCellValue("نام خانوادگی");
            var cell3 = headerRow.CreateCell(3);
            cell3.SetCellValue("کد ملی");
            var cell4 = headerRow.CreateCell(4);
            cell4.SetCellValue("شاخص");
            var cell5 = headerRow.CreateCell(5);
            cell5.SetCellValue("مقدار شاخص");
            var cell6 = headerRow.CreateCell(6);
            cell6.SetCellValue("امتیاز کسب شده در شاخص");
            var cell7 = headerRow.CreateCell(7);
            cell7.SetCellValue("ضریب شاخص");
            var cell8 = headerRow.CreateCell(8);
            cell8.SetCellValue("امتیاز کسب شده");
            var cell9 = headerRow.CreateCell(9);
            cell9.SetCellValue("امتیاز نهایی");

            cell0.CellStyle = style1;
            cell1.CellStyle = style1;
            cell2.CellStyle = style1;
            cell3.CellStyle = style1;
            cell4.CellStyle = style1;
            cell5.CellStyle = style1;
            cell6.CellStyle = style1;
            cell7.CellStyle = style1;
            cell8.CellStyle = style1;
            cell9.CellStyle = style1;



            //headerRow.RowStyle = style1;

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (var i in query)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber++);

                //Set values for the cells
                var rowCell0 = row.CreateCell(0);
                rowCell0.SetCellValue(i.ProfessorCode);

                var rowCell1 = row.CreateCell(1);
                rowCell1.SetCellValue(i.FirstName);

                var rowCell2 = row.CreateCell(2);
                rowCell2.SetCellValue(i.LastName);

                var rowCell3 = row.CreateCell(3);
                rowCell3.SetCellValue(i.NationalCode);

                var rowCell4 = row.CreateCell(4);
                rowCell4.SetCellValue(i.IndicatorName);
                if (i.AvgScore == null)
                {
                    var rowCell5 = row.CreateCell(5);
                    rowCell5.SetCellValue("امتیازی ثبت نشده");

                    var rowCell6 = row.CreateCell(6);
                    rowCell6.SetCellValue(string.Empty);

                    var rowCell7 = row.CreateCell(7);
                    rowCell7.SetCellValue(string.Empty);

                    var rowCell8 = row.CreateCell(8);
                    rowCell8.SetCellValue(string.Empty);

                    var rowCell9 = row.CreateCell(9);
                    rowCell9.SetCellValue(string.Empty);

                    rowCell0.CellStyle = style2;
                    rowCell1.CellStyle = style2;
                    rowCell2.CellStyle = style2;
                    rowCell3.CellStyle = style2;
                    rowCell4.CellStyle = style2;
                    rowCell5.CellStyle = style2;
                    rowCell6.CellStyle = style2;
                    rowCell7.CellStyle = style2;
                    rowCell8.CellStyle = style2;
                    rowCell9.CellStyle = style2;

                }
                else
                {
                    row.CreateCell(5).SetCellValue(i.ScoreName);
                    row.CreateCell(6).SetCellValue(i.WithoutRatio.Value.ToString());
                    row.CreateCell(7).SetCellValue($"{i.RatioName}(X{i.RatioName.GetValuOfRatio()})");
                    row.CreateCell(8).SetCellValue(i.AvgScore.ToString());
                    row.CreateCell(9).SetCellValue(i.ProfessorScore.Value.ToString());


                }


            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();

            workbook.Write(output);

            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                "ProfessorDetial.xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user

        }

        public ActionResult ProfessorGeneralPrint()
        {
            var generalModel = ((List<ProfessorDetialReportModel>)TempData["ProfessorGeneralResualt"]).ToList();
            TempData.Keep();

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();
            sheet.IsRightToLeft = true;

            //Set Style
            ICellStyle style1 = workbook.CreateCellStyle();
            style1.FillForegroundColor = IndexedColors.DarkGreen.Index;
            style1.FillPattern = FillPattern.SolidForeground;
            style1.FillBackgroundColor = IndexedColors.DarkGreen.Index;
            style1.BorderLeft = BorderStyle.Thin;

            //Set Font
            IFont font1 = workbook.CreateFont();
            font1.Color = IndexedColors.White.Index;
            //font1.IsItalic = true;
            font1.Boldweight = 400;
            //font1.Underline = FontUnderlineType.Double;
            font1.FontHeightInPoints = 15;

            //bind font with style 
            style1.SetFont(font1);


            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 10 * 256);
            sheet.SetColumnWidth(1, 15 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 15 * 256);
            sheet.SetColumnWidth(4, 12 * 256);

            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //Set the column names in the header row
            var cell0 = headerRow.CreateCell(0);
            cell0.SetCellValue("کد استاد");
            var cell1 = headerRow.CreateCell(1);
            cell1.SetCellValue("نام");
            var cell2 = headerRow.CreateCell(2);
            cell2.SetCellValue("نام خانوادگی");
            var cell3 = headerRow.CreateCell(3);
            cell3.SetCellValue("کد ملی");
            var cell4 = headerRow.CreateCell(4);
            cell4.SetCellValue("امتیاز نهایی");

            cell0.CellStyle = style1;
            cell1.CellStyle = style1;
            cell2.CellStyle = style1;
            cell3.CellStyle = style1;
            cell4.CellStyle = style1;

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (var i in generalModel)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber++);

                //Set values for the cells
                var row0 = row.CreateCell(0);
                row0.SetCellValue(i.ProfessorCode);
                var row1 = row.CreateCell(1);
                row1.SetCellValue(i.Name);
                var row2 = row.CreateCell(2);
                row2.SetCellValue(i.Family);
                var row3 = row.CreateCell(3);
                row3.SetCellValue(i.NationalCode);
                var row4 = row.CreateCell(4);
                row4.SetCellValue(i.ProfessorScore.Value.ToString());


            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                "ProfessorGeneral.xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user

        }
        
        public ActionResult GroupDetails(int groupId)
        {
            var model = ((List<GroupReportModel>)TempData["GroupModel"]).Where(x => x.GroupId == groupId).ToList();
            TempData.Keep();
            return PartialView(model);
        }

        public ActionResult GroupDetailsPrint()
        {
            var model = ((List<GroupReportModel>)TempData["GroupModel"]).OrderBy(x => x.GroupName).ThenBy(x => x.IndicatorName).ToList();
            TempData.Keep();

            var query = model.ToList();

            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();
            sheet.IsRightToLeft = true;
            //Set Style
            ICellStyle style1 = workbook.CreateCellStyle();
            style1.FillForegroundColor = IndexedColors.DarkGreen.Index;
            style1.FillPattern = FillPattern.SolidForeground;
            style1.FillBackgroundColor = IndexedColors.DarkGreen.Index;
            style1.BorderLeft = BorderStyle.Thin;

            ICellStyle style2 = workbook.CreateCellStyle();
            style2.FillForegroundColor = IndexedColors.LightYellow.Index;
            style2.FillPattern = FillPattern.SolidForeground;
            style2.FillBackgroundColor = IndexedColors.LightYellow.Index;
            style2.BorderBottom = BorderStyle.Thin;
            style2.BorderTop = BorderStyle.Thin;
            style2.BottomBorderColor = IndexedColors.Grey25Percent.Index;
            style2.TopBorderColor = IndexedColors.Grey25Percent.Index;


            IFont font1 = workbook.CreateFont();
            font1.Color = IndexedColors.White.Index;
            //font1.IsItalic = true;
            font1.Boldweight = 400;
            //font1.Underline = FontUnderlineType.Double;
            font1.FontHeightInPoints = 15;

            //bind font with style 
            style1.SetFont(font1);

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 10 * 256);
            sheet.SetColumnWidth(1, 15 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 40 * 256);
            sheet.SetColumnWidth(4, 30 * 256);
            sheet.SetColumnWidth(5, 30 * 256);
            sheet.SetColumnWidth(6, 15 * 256);
            sheet.SetColumnWidth(7, 17 * 256);
            sheet.SetColumnWidth(8, 20 * 256);
            sheet.SetColumnWidth(9, 15 * 256);

            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //Set the column names in the header row
            var row0 = headerRow.CreateCell(0);
            row0.SetCellValue("کد گروه");
            var row1 = headerRow.CreateCell(1);
            row1.SetCellValue("نام گروه");
            var row2 = headerRow.CreateCell(2);
            row2.SetCellValue("نام دانشکده");
            var row3 = headerRow.CreateCell(3);
            row3.SetCellValue("شاخص");
            var row4 = headerRow.CreateCell(4);
            row4.SetCellValue("مقدار شاخص");
            var row5 = headerRow.CreateCell(5);
            row5.SetCellValue("امتیاز کسب شده در شاخص");
            var row6 = headerRow.CreateCell(6);
            row6.SetCellValue("ضریب شاخص");
            var row7 = headerRow.CreateCell(7);
            row7.SetCellValue("امتیاز کسب شده");
            var row8 = headerRow.CreateCell(8);
            row8.SetCellValue("میانگین امتیاز گروه");
            var row9 = headerRow.CreateCell(9);
            row9.SetCellValue("امتیاز نهایی");

            row0.CellStyle = style1;
            row1.CellStyle = style1;
            row2.CellStyle = style1;
            row3.CellStyle = style1;
            row4.CellStyle = style1;
            row5.CellStyle = style1;
            row6.CellStyle = style1;
            row7.CellStyle = style1;
            row8.CellStyle = style1;
            row9.CellStyle = style1;

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (var i in query)
            {
                if (i.CurrentScore == null)
                {
                    //Create a new row
                    var row = sheet.CreateRow(rowNumber++);

                    //Set values for the cells

                    var rowCell0 = row.CreateCell(0);
                    rowCell0.SetCellValue(i.GroupCode);

                    var rowCell1 = row.CreateCell(1);
                    rowCell1.SetCellValue(i.GroupName);

                    var rowCell2 = row.CreateCell(2);
                    rowCell2.SetCellValue(i.CollegeName);

                    var rowCell3 = row.CreateCell(3);
                    rowCell3.SetCellValue(i.IndicatorName);


                    var rowCell4 = row.CreateCell(4);
                    rowCell4.SetCellValue("امتیازی ثبت نشده");

                    var rowCell5 = row.CreateCell(5);
                    rowCell5.SetCellValue(string.Empty);

                    var rowCell6 = row.CreateCell(6);
                    rowCell6.SetCellValue(string.Empty);

                    var rowCell7 = row.CreateCell(7);
                    rowCell7.SetCellValue(string.Empty);

                    var rowCell8 = row.CreateCell(8);
                    rowCell8.SetCellValue(string.Empty);

                    var rowCell9 = row.CreateCell(9);
                    rowCell9.SetCellValue(string.Empty);


                    rowCell0.CellStyle = style2;
                    rowCell1.CellStyle = style2;
                    rowCell2.CellStyle = style2;
                    rowCell3.CellStyle = style2;
                    rowCell4.CellStyle = style2;
                    rowCell5.CellStyle = style2;
                    rowCell6.CellStyle = style2;
                    rowCell7.CellStyle = style2;
                    rowCell8.CellStyle = style2;
                    rowCell9.CellStyle = style2;

                }
                else
                {
                    //Create a new row
                    var row = sheet.CreateRow(rowNumber++);

                    //Set values for the cells

                    var rowCell0 = row.CreateCell(0);
                    rowCell0.SetCellValue(i.GroupCode);

                    var rowCell1 = row.CreateCell(1);
                    rowCell1.SetCellValue(i.GroupName);

                    var rowCell2 = row.CreateCell(2);
                    rowCell2.SetCellValue(i.CollegeName);

                    var rowCell3 = row.CreateCell(3);
                    rowCell3.SetCellValue(i.IndicatorName);

                    if (!string.IsNullOrEmpty(i.RatioName))
                    {
                        var rowCell4 = row.CreateCell(4);
                        rowCell4.SetCellValue((i.CurrentScore / i.RatioValue)?.GetScoreName(i.IndicatorId));

                        var rowCell5 = row.CreateCell(5);
                        rowCell5.SetCellValue((i.CurrentScore / i.RatioValue).ToString());
                        var rowCell6 = row.CreateCell(6);
                        rowCell6.SetCellValue($"{i.RatioName}(X{i.RatioName.GetValuOfRatio()})");

                    }
                    else
                    {
                        var rowCell4 = row.CreateCell(4);
                        rowCell4.SetCellValue("امتیازی ثبت نشده");

                        var rowCell5 = row.CreateCell(5);
                        rowCell5.SetCellValue(string.Empty);
                        var rowCell6 = row.CreateCell(6);
                        rowCell6.SetCellValue(string.Empty);
                    }

                    var rowCell7 = row.CreateCell(7);
                    rowCell7.SetCellValue(i.CurrentScore?.ToString());

                    var rowCell8 = row.CreateCell(8);
                    rowCell8.SetCellValue(i.AvgProfessorScoreGroup?.ToString());

                    var rowCell9 = row.CreateCell(9);
                    rowCell9.SetCellValue(i.GroupScore?.ToString());



                }
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                "GroupDetial.xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }

        public ActionResult GroupGeneralPrint()
        {
            var model = (List<GroupReportModel>)TempData["GroupModel"];
            TempData.Keep();

            var query = model.Select(x => new
            {
                GroupId = x.GroupCode,
                GroupName = x.GroupName,
                CollegeName = x.CollegeName,
                ProfessorScore = x.AvgProfessorScoreGroup,
                GroupScore = x.GroupScore
            }).Distinct().ToList();

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();
            sheet.IsRightToLeft = true;

            //Set Style
            ICellStyle style1 = workbook.CreateCellStyle();
            style1.FillForegroundColor = IndexedColors.DarkGreen.Index;
            style1.FillPattern = FillPattern.SolidForeground;
            style1.FillBackgroundColor = IndexedColors.DarkGreen.Index;
            style1.BorderLeft = BorderStyle.Thin;

            //Create Font
            IFont font1 = workbook.CreateFont();
            font1.Color = IndexedColors.White.Index;
            //font1.IsItalic = true;
            font1.Boldweight = 400;
            //font1.Underline = FontUnderlineType.Double;
            font1.FontHeightInPoints = 15;

            //bind font with style 
            style1.SetFont(font1);



            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 10 * 256);
            sheet.SetColumnWidth(1, 15 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 20 * 256);
            sheet.SetColumnWidth(4, 15 * 256);
            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //Set the column names in the header row
            var row0 = headerRow.CreateCell(0);
            row0.SetCellValue("کد گروه");
            var row1 = headerRow.CreateCell(1);
            row1.SetCellValue("نام گروه");
            var row2 = headerRow.CreateCell(2);
            row2.SetCellValue("کد دانشکده");
            var row3 = headerRow.CreateCell(3);
            row3.SetCellValue("میانگین نمرات اساتید گروه");
            var row4 = headerRow.CreateCell(4);
            row4.SetCellValue("امتیاز نهایی");


            row0.CellStyle = style1;
            row1.CellStyle = style1;
            row2.CellStyle = style1;
            row3.CellStyle = style1;
            row4.CellStyle = style1;

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (var i in query)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber++);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(i.GroupId);
                row.CreateCell(1).SetCellValue(i.GroupName);
                row.CreateCell(2).SetCellValue(i.CollegeName);
                row.CreateCell(3).SetCellValue(i.ProfessorScore.Value.ToString());
                row.CreateCell(4).SetCellValue(i.GroupScore.Value.ToString());
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                "GroupGeneral.xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }

        public ActionResult CollegeDetails(int collegeId)
        {
            var model = ((List<GroupReportModel>)TempData["CollegeModel"]).Where(x => x.CollegeId == collegeId).ToList();
            TempData.Keep();
            return PartialView(model);
        }

        public ActionResult CollegeDetailsPrint()
        {
            var model = (List<GroupReportModel>)TempData["CollegeModel"];
            TempData.Keep();

            var query = model.ToList();

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();

            sheet.IsRightToLeft = true;

            //Set Style
            ICellStyle style1 = workbook.CreateCellStyle();
            style1.FillForegroundColor = IndexedColors.DarkGreen.Index;
            style1.FillPattern = FillPattern.SolidForeground;
            style1.FillBackgroundColor = IndexedColors.DarkGreen.Index;
            style1.BorderLeft = BorderStyle.Thin;

            //Create Font
            IFont font1 = workbook.CreateFont();
            font1.Color = IndexedColors.White.Index;
            //font1.IsItalic = true;
            font1.Boldweight = 400;
            //font1.Underline = FontUnderlineType.Double;
            font1.FontHeightInPoints = 15;

            //bind font with style 
            style1.SetFont(font1);

            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 10 * 256);
            sheet.SetColumnWidth(1, 20 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 20 * 256);
            sheet.SetColumnWidth(4, 15 * 256);
            sheet.SetColumnWidth(5, 15 * 256);

            //Set the column names in the header row
            var row0 = headerRow.CreateCell(0);
            row0.SetCellValue("کد دانشکده");
            var row1 = headerRow.CreateCell(1);
            row1.SetCellValue("نام دانشکده");
            var row2 = headerRow.CreateCell(2);
            row2.SetCellValue("کد گروه");
            var row3 = headerRow.CreateCell(3);
            row3.SetCellValue("نام گروه");
            var row4 = headerRow.CreateCell(4);
            row4.SetCellValue("امتیاز کسب شده گروه");
            var row5 = headerRow.CreateCell(5);
            row5.SetCellValue("امتیاز نهایی دانشکده");

            row0.CellStyle = style1;
            row1.CellStyle = style1;
            row2.CellStyle = style1;
            row3.CellStyle = style1;
            row4.CellStyle = style1;
            row5.CellStyle = style1;

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (var i in query)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber++);

                //Set values for the cells

                row.CreateCell(0).SetCellValue(i.CollegeCode);
                row.CreateCell(1).SetCellValue(i.CollegeName);
                row.CreateCell(2).SetCellValue(i.GroupCode);
                row.CreateCell(3).SetCellValue(i.GroupName);
                row.CreateCell(4).SetCellValue(i.GroupScore.Value);
                row.CreateCell(5).SetCellValue(i.CollegeScore);
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                "CollegeDetial.xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }

        public ActionResult CollegeGeneralPrint()
        {
            var model = (List<GroupReportModel>)TempData["CollegeModel"];
            TempData.Keep();

            if (!model.Any())
                model = new List<GroupReportModel>();
            else
            {
                var tempList = new List<GroupReportModel>();
                foreach (var item in model)
                {
                    if (tempList.FirstOrDefault(x => x.CollegeId == item.CollegeId) == null)
                        tempList.Add(item);
                }

                model = tempList;
            }
            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();

            sheet.IsRightToLeft = true;

            //Set Style
            ICellStyle style1 = workbook.CreateCellStyle();
            style1.FillForegroundColor = IndexedColors.DarkGreen.Index;
            style1.FillPattern = FillPattern.SolidForeground;
            style1.FillBackgroundColor = IndexedColors.DarkGreen.Index;
            style1.BorderLeft = BorderStyle.Thin;

            //Create Font
            IFont font1 = workbook.CreateFont();
            font1.Color = IndexedColors.White.Index;
            //font1.IsItalic = true;
            font1.Boldweight = 400;
            //font1.Underline = FontUnderlineType.Double;
            font1.FontHeightInPoints = 15;

            //bind font with style 
            style1.SetFont(font1);

            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 10 * 256);
            sheet.SetColumnWidth(1, 20 * 256);
            sheet.SetColumnWidth(2, 20 * 256);


            //Set the column names in the header row
            var row0 = headerRow.CreateCell(0);
            row0.SetCellValue("کد دانشکده");
            var row1 = headerRow.CreateCell(1);
            row1.SetCellValue("نام دانشکده");
            var row2 = headerRow.CreateCell(2);
            row2.SetCellValue("امتیاز نهایی دانشکده");

            row0.CellStyle = style1;
            row1.CellStyle = style1;
            row2.CellStyle = style1;



            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (var i in model)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber++);

                //Set values for the cells

                row.CreateCell(0).SetCellValue(i.CollegeCode);
                row.CreateCell(1).SetCellValue(i.CollegeName);
                row.CreateCell(2).SetCellValue(i.CollegeScore);
            }
            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                "CollegeGeneral.xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }

        public ActionResult GroupManagersActivity()
        {
            SetViewBag("گزارش مدیران گروه ها", "GroupManagersActivity");
            ViewBag.CollegeList = new SelectList(_collegeService.GetAll().ToList(), "Id", "Name");
            ViewBag.TermList = new SelectList(_termService.GetAll().OrderByDescending(o => o.TermCode).ToList(), "Id", "Name");
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
                var groups = _educationalGroupService.GetMany(g => g.College.Id == collegeQS && g.Term.Id == selectedTerm.Id
                 && !StaticValue.IneligibleEducationalGroupCodes.Contains((int)g.EducationalGroupCode)
                ).ToList().Select(s => new EducationalGroup
                {
                    Id = s.Id,
                    EducationalGroupCode = s.EducationalGroupCode,
                    Name = s.Name,
                    EducationalGroupScores = s.EducationalGroupScores,
                    GroupManger = s.GroupManger,
                    InTimePresentCurriculum = GetCurrentStatus(s.Id), // InTimePresentCurriculum Used Just As a PlaceHolder
                }).ToList();
                //TempData["collegeId"] = collegeQS;
                ViewBag.CollegeId = collegeQS;
                return View(groups);
            }
            //if ((user == null || user.College == null) && TempData["model"] == null)
            //    return View();
            return View();
        }

        public string GetCurrentStatus(int id)
        {
            var proScores = _professorScoreService.GetMany(g => g.EducationalGroup.Id == id && g.Score.Indicator.CountOfType == "p" + (int)IndicatorProfessorName.نظر_مدیر_گروه);
            if (proScores.Count() == 0)
                return "ثبت نشده";
            if (proScores.Count() == _educationalGroupService.Get(g => g.Id == id).EducationalClasses.Select(s => s.Professor).Distinct().Count())
                return "ثبت شده";
            else
                return "ثبت ناقص";
        }

        private Model.Models.Term GetCurrentTerm()
        {
            var lt = _termService.Get(g => g.IsCurrentTerm);
            var ct = new Model.Models.Term();
            Mapper.Map(lt, ct);
            return ct;
        }
    }
}