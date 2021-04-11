using AutoMapper;
using IAUECProfessorsEvaluation.Core.Helper;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.IService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace IAUECProfessorsEvaluation.Web.Controllers
{
    public class EducationalClassesController : Controller
    {
        private IEducationalClassService _educationalClassService;
        private IEducationalGroupService _educationalGroupService;
        private IProfessorScoreService _professorScoreService;
        private ITermService _termService;
        private IScoreService _scoreService;
        private IRoleAccessService _roleAccessService;
        private IUserRoleService _userRoleService;
        private IProfessorService _professorService;
        private IIndicatorService _indicatorService;

        public EducationalClassesController(
            IEducationalClassService educationalClassService
            , IEducationalGroupService educationalGroupService
            , ITermService termService
            , IProfessorScoreService professorScoreService
            , IScoreService scoreService
            , IRoleAccessService roleAccessService
            , IUserRoleService userRoleService
            , IProfessorService professorService
            , IIndicatorService indicatorService
            )
        {
            _educationalClassService = educationalClassService;
            _educationalGroupService = educationalGroupService;
            _termService = termService;
            _professorScoreService = professorScoreService;
            _scoreService = scoreService;
            _roleAccessService = roleAccessService;
            _userRoleService = userRoleService;
            _professorService = professorService;
            _indicatorService = indicatorService;
        }

        public ActionResult EducationalClassesManagement()
        {
            SetViewBag("مدیریت جلسات برگزاری کلاس ها", "EducationalClassesManagement");
            Term selectedTerm = null;
            if (string.IsNullOrEmpty(Request.QueryString["termId"]))
                selectedTerm = GetCurrentTerm();
            else
            {
                var termId = Convert.ToInt32(Request.QueryString["termId"]);
                selectedTerm = _termService.Get(g => g.Id == termId);
            }
            var user = (Models.User)Session["UserInfo"];
            var groupQS = 0;
            ViewBag.GroupList = new SelectList(_educationalGroupService.GetMany(w => w.Term.Id == selectedTerm.Id).Where(w => !StaticValue.IneligibleEducationalGroupCodes.Contains((int)w.EducationalGroupCode)), "EducationalGroupCode", "Name");
            ViewBag.TermList = new SelectList(_termService.GetAll().OrderByDescending(o => o.TermCode), "Id", "Name");
            ViewBag.TermId = selectedTerm.Id;
            if (Request.QueryString["group"] != null && int.TryParse(Request.QueryString["group"], out groupQS))
            {
                TempData["model"] = _educationalClassService.GetMany(w => w.Term.Id == selectedTerm.Id && w.EducationalGroup.EducationalGroupCode == groupQS).ToList();
                TempData["groupId"] = groupQS;
            }
            if ((user == null || user.EducationalGroupCode == null) && TempData["model"] == null)
                return View();
            if (TempData["model"] != null)
            {
                var classes = (List<Model.Models.EducationalClass>)TempData["model"];
                ViewBag.GroupId = TempData["groupId"];
                TempData["model"] = null;
                TempData["groupId"] = null;
                return View(classes);
            }
            else
            {
                var groupId = _educationalGroupService.Get(x => x.EducationalGroupCode == user.EducationalGroupCode && x.Term.Id == selectedTerm.Id).Id;
                var classes = _educationalGroupService.Get(g => g.Id == groupId).EducationalClasses.Where(w => w.Term.Id == selectedTerm.Id).ToList();
                return View(classes);
            }

        }

        //public ActionResult EducationalClassesManagementForAdmin(string group)
        //{
        //    var term = GetCurrentTerm();
        //    var groupId = Convert.ToInt32(group);
        //    var classes = _educationalGroupService.Get(g => g.Id == groupId).EducationalClasses.Where(w => w.Term.Id == term.Id).ToList();
        //    TempData["model"] = classes;
        //    TempData["groupId"] = groupId;
        //    return this.Json(string.Empty);
        //}

        public ActionResult EditClassDetails()
        {
            ViewBag.TermId = Request.QueryString["termId"];
            var cid = Convert.ToInt32(Request.QueryString["class"]);
            return PartialView(_educationalClassService.Get(g => g.CodeClass == cid));
        }
        public ActionResult UpdateClassInfoFromExcel()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult UploadAndUpdateClassInfo(string groupId, string termId)
        {
            var rowCount = 0;
            var tid = Convert.ToInt32(termId);
            var badRows = new List<int>();
            foreach (string file in Request.Files)
            {
                var fileContent = Request.Files[file];
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    var stream = fileContent.InputStream;
                    var ext = fileContent.FileName.Substring(fileContent.FileName.LastIndexOf('.'));
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ext;
                    var folderPath = Server.MapPath("~/Content/userUploads");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);
                    var path = Path.Combine(folderPath, fileName);
                    using (var fileStream = System.IO.File.Create(path))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.CopyTo(fileStream);
                    }

                    string con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";
                    using (OleDbConnection connection = new OleDbConnection(con))
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);
                        using (OleDbDataReader dr = command.ExecuteReader())
                        {

                            do
                            {
                                rowCount++;
                                var classCode = 0;
                                var onlineHeld = -1;
                                var offlineHeld = -1;
                                var otherHeld = -1;

                                if (rowCount != 1 && int.TryParse(dr[0].ToString(), out classCode) && classCode > 0
                                        && (string.IsNullOrEmpty(dr[1].ToString()) || (!string.IsNullOrEmpty(dr[1].ToString()) && int.TryParse(dr[1].ToString(), out onlineHeld) && onlineHeld > 0))
                                        && (string.IsNullOrEmpty(dr[2].ToString()) || (!string.IsNullOrEmpty(dr[2].ToString()) && int.TryParse(dr[2].ToString(), out offlineHeld) && offlineHeld > 0))
                                        && (string.IsNullOrEmpty(dr[3].ToString()) || (!string.IsNullOrEmpty(dr[3].ToString()) && int.TryParse(dr[3].ToString(), out otherHeld) && otherHeld > 0))
                                    )
                                {
                                    var classObj = _educationalClassService.Get(g => g.CodeClass == classCode && g.Term.Id == tid);
                                    var gid = Convert.ToInt32(groupId);
                                    if (classObj != null && classObj.EducationalGroup.EducationalGroupCode == gid)
                                        UpdateClassSessions(classCode, onlineHeld, offlineHeld, otherHeld, termId, false);
                                    else
                                        badRows.Add(rowCount);
                                }
                                else if (rowCount != 1)
                                    badRows.Add(rowCount);
                            }
                            while (dr.Read());

                        }
                    }
                    System.IO.File.Delete(path);
                }
            }
            dynamic res = new ExpandoObject();
            res.TotalCount = rowCount - 1;
            res.ErrorCount = badRows.Count();
            res.BadRows = String.Join(", ", badRows.ToArray()); ;
            res.Added = rowCount - badRows.Count() - 1;
            return Content(JsonConvert.SerializeObject(res), "application/json");
        }

        [HttpPost]
        public ActionResult UpdateClassDetails(string classCodeData, string onlineHeld, string presentHeld, string other, string termId)
        {
            var classCode = 0;
            var onlineSessions = 0;
            var offlineSessions = 0;
            var otherSessions = 0;
            int.TryParse(onlineHeld, out onlineSessions);
            int.TryParse(presentHeld, out offlineSessions);
            int.TryParse(other, out otherSessions);
            if (int.TryParse(classCodeData, out classCode) && classCode > 0)
                UpdateClassSessions(classCode, onlineSessions, offlineSessions, otherSessions, termId, true);
            return null;
        }

        public void UpdateClassSessions(int classCode, int onlineHeld, int presentHeld, int other, string termId = null, bool manualUpdate = false)
        {
            Term currentTerm = null;
            if (string.IsNullOrEmpty(termId))
                currentTerm = GetCurrentTerm();
            else
            {
                var tid = Convert.ToInt32(termId);
                currentTerm = _termService.Get(g => g.Id == tid);
            }
            var obj = _educationalClassService.Get(g => g.CodeClass == classCode && g.Term.Id == currentTerm.Id);
            if ((manualUpdate && onlineHeld >= 0) || (!manualUpdate && onlineHeld > 0))
                obj.OnlineHeldingCount = onlineHeld;
            if ((manualUpdate && presentHeld >= 0) || (!manualUpdate && presentHeld > 0))
                obj.PersentHeldingCount = presentHeld;
            if ((manualUpdate && other >= 0) || (!manualUpdate && other > 0))
                obj.OthersHeldingCount = other;
            _educationalClassService.Update(obj);


            //var classes = _educationalClassService.GetMany(g => g.Professor.Id == obj.Professor.Id && g.Term.Id == currentTerm.Id && g.EducationalGroup.Id == obj.EducationalGroup.Id);
            //obj.Professor.OnlineSession = classes.Sum(s => s.OnlineHeldingCount);
            //obj.Professor.InPersonSession = classes.Sum(s => s.PersentHeldingCount);
            //obj.Professor.OthersSession = classes.Sum(s => s.OthersHeldingCount);
            //_professorService.Update(obj.Professor);

            var indicatorCountOfTypet = "p" + (int)IndicatorProfessorName.تعداد_جلسات_برگزار_شده_کلاس;
            var profAllScores = _professorScoreService.GetMany(g =>
            g.Professor.Id == obj.Professor.Id
            && g.Term.Id == currentTerm.Id
            && g.Score.Indicator.CountOfType == indicatorCountOfTypet);
            Model.Models.ProfessorScore profScore = new Model.Models.ProfessorScore();
            if (profAllScores.Count() > 0)
                profScore = profAllScores.Where(w => w.EducationalGroup.Id == obj.EducationalGroup.Id).FirstOrDefault();

            //var totalCount = (obj.Professor.OnlineSession + obj.Professor.InPersonSession + obj.Professor.OthersSession) / classes.Count();
            //var score = _scoreService.Get(g => g.MinValue <= totalCount && g.MaxValue >= totalCount && g.Indicator.CountOfType == indicatorCountOfTypet);


            var listOfCountClass = new List<decimal>();


            var listOfClass = _educationalClassService.GetMany(x => x.Professor.Id == obj.Professor.Id &&
                                                                    x.EducationalGroup.Id == obj.EducationalGroup.Id &&
                                                                    x.Term.Id == currentTerm.Id &&
                                                                    x.IsActive==true).ToList();

            var indicator7 = _indicatorService
                .Get(y => y.CountOfType == ("p" + (int)IndicatorProfessorName.تعداد_جلسات_برگزار_شده_کلاس));
            var scores7 = indicator7.Scores;

            foreach (var c in listOfClass)
            {
                if (obj.HoldingExamDate == null)
                {
                    listOfCountClass.Add(((scores7.FirstOrDefault(p => p.Point == 80).MinValue.Value) + 1));
                }
                else
                {

                    listOfCountClass.Add(((obj.OnlineHeldingCount ?? 0) +(obj.PersentHeldingCount ?? 0) +(obj.OthersHeldingCount ?? 0)));
                }
            }
            var avg = Math.Round(listOfCountClass.Average(), 0);
            var s = scores7
                .FirstOrDefault(p => p.MinValue <= avg
                                     && p.MaxValue >= avg
                );

            if (s != null)
                if (profScore != null && profScore.Id > 0)
                {
                    profScore.Score = s;
                    var point = s.Point * indicator7.Ratio.Point;
                    if (point != null)
                    {
                        profScore.CurrentScore = (int)point;
                    }
                    _professorScoreService.Update(profScore);
                }
                else
                {
                    var point = s.Point * indicator7.Ratio.Point;
                    if (point != null)
                    {
                        var sPoint = (int)point;
                        _professorScoreService.Add(new Model.Models.ProfessorScore
                        {
                            EducationalGroup = obj.EducationalGroup,
                            Professor = obj.Professor,
                            Term = currentTerm,
                            Score = s,
                            CurrentScore = (int)point
                        });
                    }
                }

            //var scores = _professorScoreService.GetMany(g =>
            //g.Professor.Id == obj.Professor.Id
            //&& g.Term.Id == currentTerm.Id
            //&& g.Score.Indicator.CountOfType == indicatorCountOfTypet).ToList();
            //foreach (var item in scores)
            //{
            //    item.CurrentScore = currentScore;
            //    _professorScoreService.Update(item);
            //}
        }

        #region Helper Methodes
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

        private Model.Models.Term GetCurrentTerm()
        {
            var lt = _termService.Get(g => g.IsCurrentTerm);
            var ct = new Model.Models.Term();
            Mapper.Map(lt, ct);
            return ct;
        }
        #endregion
    }
}