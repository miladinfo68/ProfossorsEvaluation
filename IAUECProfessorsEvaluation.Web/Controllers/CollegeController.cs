using AutoMapper;
using IAUECProfessorsEvaluation.Service.IService;
using IAUECProfessorsEvaluation.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAUECProfessorsEvaluation.Web.Controllers
{
    public class CollegeController : Controller
    {
        private ICollegeService _collegeService;
        private ICollegeScoreService _collegeScoreService;
        private IEducationalGroupService _educationalGroupService;
        private ITermService _termService;
        private IRoleAccessService _roleAccessService;
        private IUserRoleService _userRoleService;
        private DapperService _reportService;
        private ILogService _logService;

        public CollegeController(
            ICollegeService collegeService
            , ICollegeScoreService collegeScoreService
            , IEducationalGroupService educationalGroupService
            , ITermService termService
            , IRoleAccessService roleAccessService
            , IUserRoleService userRoleService
            , ILogService logService
            )
        {
            _collegeService = collegeService;
            _collegeScoreService = collegeScoreService;
            _educationalGroupService = educationalGroupService;
            _termService = termService;
            _reportService = new DapperService(educationalGroupService);
            _roleAccessService = roleAccessService;
            _userRoleService = userRoleService;
            _logService = logService;
        }

        public ActionResult RankOfCollegeInUniversity(string term = null)
        {
            SetViewBag(title: "رتبه بندی دانشکده ها", menuItem: "RankOfCollegeInUniversity");
            //// Old Methode Start
            //var counter = 0;
            //var professors = Mapper.Map<IEnumerable<Model.Models.College>, IEnumerable<Models.College>>(_collegeService.GetAll()).OrderByDescending(o => o.TotalScores).Select(
            //    x => new Models.College
            //    {
            //        CollegeCode = x.CollegeCode,
            //        CollegeScores = x.CollegeScores,
            //        EducationalGroups = x.EducationalGroups,
            //        //Professors = x.Professors,
            //        CreationDate = x.CreationDate,
            //        Name = x.Name,
            //        Id = x.Id,
            //        IsActive = x.IsActive,
            //        LastModifiedDate = x.LastModifiedDate,
            //        RankInUniversity = ++counter
            //    }).ToList();
            //return View(professors);
            //// Old Methode End
            var termId = 0;
            ViewBag.TermList = new SelectList(_termService.GetAll().OrderByDescending(o => o.TermCode), "Id", "Name");
            if (!string.IsNullOrEmpty(term) && int.TryParse(term, out termId) && termId > 0)
            {
                var model = _reportService.GetCollegeReport(termId).General.ToList();
                return View(model);
            }
            return View();
        }

        [Authorize]
        public ActionResult ViewCollegeRankInUniversityDetails(int id = 0, int termId = 0)
        {
            //// Old Methode Start
            //var model = new Models.CollegeRankDetails();
            //if (id > 0)
            //{
            //    model.CollegeScores = null;//Mapper.Map<List<Model.Models.CollegeScore>, List<Models.CollegeScore>>(_collegeScoreService.GetAll().Where(w => w.College.CollegeCode == id).ToList());
            //    model.CollegeInfo = Mapper.Map<Model.Models.College, Models.College>(_collegeService.Get(g => g.CollegeCode == id));
            //}
            //return PartialView(model);
            //// Old Methode End
            if (termId == 0)
                termId = GetCurrentTerm().Id;
            var model = _reportService.GetCollegeReport(termId).Detial.Where(p => p.CollegeId == id).ToList();
            return PartialView(model);
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
        #endregion
    }
}