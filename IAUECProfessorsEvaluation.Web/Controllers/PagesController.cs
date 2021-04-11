using IAUECProfessorsEvaluation.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Web.Models.Utility;
using IAUECProfessorsEvaluation.Web.Helpers;
using IAUECProfessorsEvaluation.Core.Helper;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Web.Models.ViewModel;

namespace IAUECProfessorsEvaluation.Web.Controllers
{
    [Authorize]

    public class PagesController : Controller
    {
        #region Fields
        private ITermService _termService;
        private ICollegeService _collegeService;
        private IEducationalGroupService _educationalGroupService;
        private IProfessorService _professorService;
        private IUserService _userService;
        private IRoleService _roleService;
        private IUserRoleService _userRoleService;
        private IRoleAccessService _roleAccessService;
        private IAccessService _accessService;
        private IMappingService _mappingService;
        private IIndicatorService _indicatorService;
        private IUniversityLevelMappingService _universityLevelMappingService;
        private IScheduleService _scheduleService;
        private ILogService _logService;
        private ILogTypeService _logTypeService;
        private IEducationalClassService _educationalClassService;
        private IStudentEducationalClassService _studentEducationalClassService;
        private IProfessorScoreService _professorScoreService;
        private IMappingTypeService _mappingTypeService;
        private IEducationalGroupScoreService _educationalGroupScoreService;
        private IServiceUsersMappingService _serviceUsersMappingService;
        #endregion

        #region Ctor
        public PagesController(
            ITermService termService
            , ICollegeService collegeService
            , IEducationalGroupService educationalGroupService
            , IProfessorService professorService
            , IRoleService roleService
            , IRoleAccessService roleAccessService
            , IAccessService accessService
            , IUserRoleService userRoleService
            , IMappingService mappingService
            , IIndicatorService indicatorService
            , IUniversityLevelMappingService universityLevelMappingService
            , IUserService userService
            , IScheduleService scheduleService


            , IEducationalClassService educationalClassService
            , IStudentEducationalClassService studentEducationalClassService
            , IProfessorScoreService professorScoreService
            , IMappingTypeService mappingTypeService
            , IEducationalGroupScoreService educationalGroupScoreService
            , ILogService logService
            , ILogTypeService logTypeService
            , IServiceUsersMappingService serviceUsersMappingService
            )
        {
            _termService = termService;
            _collegeService = collegeService;
            _educationalGroupService = educationalGroupService;
            _professorService = professorService;
            _roleService = roleService;
            _roleAccessService = roleAccessService;
            _accessService = accessService;
            _userRoleService = userRoleService;
            _mappingService = mappingService;
            _indicatorService = indicatorService;
            _universityLevelMappingService = universityLevelMappingService;
            _userService = userService;
            _scheduleService = scheduleService;
            _logService = logService;
            _logTypeService = logTypeService;
            _educationalClassService = educationalClassService;
            _studentEducationalClassService = studentEducationalClassService;
            _professorScoreService = professorScoreService;
            _mappingTypeService = mappingTypeService;
            _educationalGroupScoreService = educationalGroupScoreService;
            _serviceUsersMappingService = serviceUsersMappingService;
            //SyncService.RunAll(termService, mappingService, collegeService, educationalGroupService
            //    , professorService, educationalClassService, studentEducationalClassService, professorScoreService
            //    , indicatorService, mappingTypeService, universityLevelMappingService, educationalGroupScoreService
            //    , logService, logTypeService, userService);
            //var lastUpdate = _logService.LastUpdate();
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            SetViewBag(title: "میز کار");

            if(Session["UserInfo"] == null)
            return RedirectToAction("Login");

            var user = (Models.User)Session["UserInfo"];
            var role = _userRoleService.GetMany(w => w.User.ID == user.ID).Select(s => s.Role.Id).ToList();
            var model = new List<SystemMessageViewModel>();
            if (role.Contains(5)) //Student Role
            {
                var units = ClientHelper.GetValue<UnitProfessorsSyncModel>(StaticValue.GetStudentCurrentUnits + $"/?stcode={user.PersonalCode}");
                var sc = DependencyResolver.Current.GetService<StudentEvaluationController>();
                if (!Convert.ToBoolean(sc.IsUserevaluated(user.PersonalCode).Data))
                {
                    model.Add(new SystemMessageViewModel { MessageType = "warning", MessageBody = "لطفا ارزیابی واحد را تکمیل نمایید." });
                }
                foreach (var unit in units)
                {
                    if(!Convert.ToBoolean(sc.IsUserEvaluatedProfessor(Convert.ToInt32(unit.ProfessorCode).ToString(), Convert.ToInt32(unit.Did).ToString(), unit.stcode).Data))
                    {
                        model.Add(new SystemMessageViewModel { MessageType = "warning", MessageBody = "لطفاً ارزیابی کلاس " + unit.ClassName + " استاد " + unit.ProfessorName + " را تکمیل نمایید." });
                    }
                }
               
            }

            //var colleges = _collegeService.GetAll().ToList();
            //var currentTermId = _termService.Get(g => g.IsCurrentTerm).Id;
            ////var colleges1 = _collegeService.GetAll1();
            //var groups = _educationalGroupService.GetAll().Where(w=> w.Term.Id == currentTermId).ToList();
            //var professors = _professorService.GetAll().Where(w=> w.Term.Id == currentTermId);
            //var professorOrder = professors.OrderByDescending(o => o.ProfessorScores.Sum(s => s.CurrentScore));
            //var groupOrder = groups.OrderByDescending(o => o.EducationalGroupScores.Sum(s => s.CurrentScore) + o.EducationalClasses.SelectMany(s => s.Professor.ProfessorScores).Sum(s => s.CurrentScore));
            //var collegeOrder = colleges.OrderByDescending(o => o.CollegeScores.Sum(s => s.CurrentScore) + o.EducationalGroups.SelectMany(s => s.EducationalGroupScores).Sum(s => s.CurrentScore) + o.EducationalGroups.SelectMany(s => s.EducationalClasses).Select(s => s.Professor).SelectMany(s => s.ProfessorScores).Sum(s => s.CurrentScore));
            //if (colleges.Count() >= 3 && collegeOrder.Count() >= 3 && groups.Count() >= 3 && groupOrder.Count() >= 3 && professors.Count() >= 3 && professorOrder.Count() >= 3)
            //    return View(new Web.Models.IndexModel
            //    {
            //        collegeCountInRank1 = colleges.Where(w => w.CollegeScores.Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalGroupScores).Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalClasses).Select(s => s.Professor).SelectMany(s => s.ProfessorScores).Sum(s => s.CurrentScore) > 900).Count(), //بیش از 900 امتیاز
            //        collegeCountInRank2 = colleges.Where(w => w.CollegeScores.Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalGroupScores).Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalClasses).Select(s => s.Professor).SelectMany(s => s.ProfessorScores).Sum(s => s.CurrentScore) < 900 && w.CollegeScores.Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalGroupScores).Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalClasses).Select(s => s.Professor).SelectMany(s => s.ProfessorScores).Sum(s => s.CurrentScore) > 700).Count(),    //بین 700 تا 900 امتیاز
            //        collegeCountInRank3 = colleges.Where(w => w.CollegeScores.Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalGroupScores).Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalClasses).Select(s => s.Professor).SelectMany(s => s.ProfessorScores).Sum(s => s.CurrentScore) < 700 && w.CollegeScores.Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalGroupScores).Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalClasses).Select(s => s.Professor).SelectMany(s => s.ProfessorScores).Sum(s => s.CurrentScore) > 500).Count(),    //بین 500 تا 700 امتیاز
            //        collegeCountInRank4 = colleges.Where(w => w.CollegeScores.Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalGroupScores).Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalClasses).Select(s => s.Professor).SelectMany(s => s.ProfessorScores).Sum(s => s.CurrentScore) < 500 && w.CollegeScores.Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalGroupScores).Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalClasses).Select(s => s.Professor).SelectMany(s => s.ProfessorScores).Sum(s => s.CurrentScore) > 300).Count(),    //بین 300 تا 500 امتیاز
            //        collegeCountInRank5 = colleges.Where(w => w.CollegeScores.Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalGroupScores).Sum(s => s.CurrentScore) + w.EducationalGroups.SelectMany(s => s.EducationalClasses).Select(s => s.Professor).SelectMany(s => s.ProfessorScores).Sum(s => s.CurrentScore) < 300).Count(),    //کمتر از 300 امتیاز

            //        educationalGroupCountInRank1 = groups.Where(w => w.EducationalGroupScores.Sum(s => s.CurrentScore) + w.EducationalClasses.SelectMany(s => s.Professor.ProfessorScores).Sum(s => s.CurrentScore) > 900).Count(),    //بیش از 900 امتیاز
            //        educationalGroupCountInRank2 = groups.Where(w => w.EducationalGroupScores.Sum(s => s.CurrentScore) + w.EducationalClasses.SelectMany(s => s.Professor.ProfessorScores).Sum(s => s.CurrentScore) > 700 && w.EducationalGroupScores.Sum(s => s.CurrentScore) + w.EducationalClasses.SelectMany(s => s.Professor.ProfessorScores).Sum(s => s.CurrentScore) < 900).Count(),    //بین 700 تا 900 امتیاز
            //        educationalGroupCountInRank3 = groups.Where(w => w.EducationalGroupScores.Sum(s => s.CurrentScore) + w.EducationalClasses.SelectMany(s => s.Professor.ProfessorScores).Sum(s => s.CurrentScore) > 500 && w.EducationalGroupScores.Sum(s => s.CurrentScore) + w.EducationalClasses.SelectMany(s => s.Professor.ProfessorScores).Sum(s => s.CurrentScore) < 700).Count(),    //بین 500 تا 700 امتیاز
            //        educationalGroupCountInRank4 = groups.Where(w => w.EducationalGroupScores.Sum(s => s.CurrentScore) + w.EducationalClasses.SelectMany(s => s.Professor.ProfessorScores).Sum(s => s.CurrentScore) > 300 && w.EducationalGroupScores.Sum(s => s.CurrentScore) + w.EducationalClasses.SelectMany(s => s.Professor.ProfessorScores).Sum(s => s.CurrentScore) < 500).Count(),    //بین 300 تا 500 امتیاز
            //        educationalGroupCountInRank5 = groups.Where(w => w.EducationalGroupScores.Sum(s => s.CurrentScore) + w.EducationalClasses.SelectMany(s => s.Professor.ProfessorScores).Sum(s => s.CurrentScore) < 300).Count(),    //کمتر از 300 امتیاز

            //        professorCountInRank1 = professors.Where(w => w.ProfessorScores.Sum(s => s.CurrentScore) > 900).Count(),    //بیش از 900 امتیاز
            //        professorCountInRank2 = professors.Where(w => w.ProfessorScores.Sum(s => s.CurrentScore) > 700 && w.ProfessorScores.Sum(s => s.CurrentScore) < 900).Count(),     //بین 700 تا 900 امتیاز
            //        professorCountInRank3 = professors.Where(w => w.ProfessorScores.Sum(s => s.CurrentScore) > 500 && w.ProfessorScores.Sum(s => s.CurrentScore) < 700).Count(),    //بین 500 تا 700 امتیاز
            //        professorCountInRank4 = professors.Where(w => w.ProfessorScores.Sum(s => s.CurrentScore) > 300 && w.ProfessorScores.Sum(s => s.CurrentScore) < 500).Count(),     //بین 300 تا 500 امتیاز
            //        professorCountInRank5 = professors.Where(w => w.ProfessorScores.Sum(s => s.CurrentScore) < 300).Count(),     //کمتر از 300 امتیاز

            //        topCollegeName1 = collegeOrder.ElementAt(0).Name,
            //        topCollegeName2 = collegeOrder.ElementAt(1).Name,
            //        topCollegeName3 = collegeOrder.ElementAt(2).Name,
            //        topEducationalGroupName1 = groupOrder.ElementAt(0).Name,
            //        topEducationalGroupName2 = groupOrder.ElementAt(1).Name,
            //        topEducationalGroupName3 = groupOrder.ElementAt(2).Name,
            //        topProfessorName1 = professorOrder.ElementAt(0).Name + " " + professorOrder.ElementAt(0).Family,
            //        topProfessorName2 = professorOrder.ElementAt(1).Name + " " + professorOrder.ElementAt(1).Family,
            //        topProfessorName3 = professorOrder.ElementAt(2).Name + " " + professorOrder.ElementAt(2).Family
            //    });
            return View(model);
        }
        #endregion

        #region Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            SetViewBag(title: "ورود کاربران");
            return View(new Models.User());
        }
        [AllowAnonymous]
        public ActionResult ServiceLogin(string ServiceUserLogin, string FirstName = null, string LastName = null)
        {
            var userMapping = _serviceUsersMappingService.Get(g => g.ServiceUsername == ServiceUserLogin);
            if (userMapping != null)
            {
                var userInfo = _userService.Get(g => g.Username == userMapping.Username);
                if (userInfo != null)
                {
                    var user = new Models.User
                    {
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName,
                        Username = userInfo.Username,
                        ID = userInfo.ID,
                        IsAdministrator = userInfo.IsAdministrator,
                        IsPowerUser = userInfo.IsPowerUser
                    };
                    if (userInfo.College != null)
                        user.College = Mapper.Map<Models.College>(userInfo.College);
                    if (userInfo.EducationalGroupCode != null)
                        user.EducationalGroupCode = userInfo.EducationalGroupCode;
                    System.Web.Security.FormsAuthentication.SetAuthCookie(user.Username, false);
                    Session["UserInfo"] = user;
                    return RedirectToAction("Index");
                }
            }
            else if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
            {
                var userInfo = _userService.Get(g => g.Username == "Student");
                var student = new Models.User
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    PersonalCode = ServiceUserLogin,
                    ID = userInfo.ID
                };
                System.Web.Security.FormsAuthentication.SetAuthCookie(student.Username, false);
                Session["UserInfo"] = student;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Models.User user)
        {
            if (!ModelState.IsValid || user.Username.ToLower() == "sync")
                return View(user);
            var userInfo = _userService.Get(g => g.Username == user.Username);
            if (userInfo != null && user.Password.VerifyMd5Hash(userInfo.Password))
            {
                user = new Models.User
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Username = userInfo.Username,
                    ID = userInfo.ID,
                    IsAdministrator = userInfo.IsAdministrator,
                    IsPowerUser = userInfo.IsPowerUser
                };
                if (userInfo.College != null)
                    user.College = Mapper.Map<Models.College>(userInfo.College);
                if (userInfo.EducationalGroupCode != null)
                    user.EducationalGroupCode = userInfo.EducationalGroupCode;//Mapper.Map<Models.EducationalGroup>(userInfo.EducationalGroup);
                System.Web.Security.FormsAuthentication.SetAuthCookie(user.Username, false);
                Session["UserInfo"] = user;
                return RedirectToAction("Index");
            }
            else
            {
                // Log
                ViewBag.HaveMessage = "Error";
                ViewBag.Message = "نام کاربری یا کلمه عبور صحیح نیست.";
                SetViewBag(title: "ورود کاربران");
                return View(user);
            }
        }
        [HttpPost]
        public ActionResult LogOff()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session["UserInfo"] = null;
            return RedirectToAction("Index");
        }
        #endregion

        #region Roles
        public ActionResult RoleManagement()
        {
            SetViewBag(title: "مدیریت نقش ها", menuItem: "RoleManagement");
            var roles = _roleService.GetAll().Select(s => new Models.Role
            {
                Id = s.Id,
                Name = s.Name,
                Users = Mapper.Map<List<Models.User>>(_userRoleService.GetMany(g => g.Role.Id == s.Id).Select(se => se.User).ToList())
            }).ToList();
            return View(roles);
        }
        public ActionResult ShowRole(int id = 0)
        {
            ViewBag.UserList = new SelectList(_userService.GetAll().Where(w => w.ID != 1 && w.Username.ToLower() != "sync"), "Id", "UserName", id);
            var model = new Models.Role();
            if (id > 0)
            {
                var role = _roleService.Get(g => g.Id == id);
                model.Id = role.Id;
                model.Name = role.Name;
                model.Users = Mapper.Map<List<Models.User>>(_userRoleService.GetMany(g => g.Role.Id == role.Id).Select(se => se.User).ToList());
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddOrUpdateRole(string id, string name)
        {
            var roleId = Convert.ToInt32(id);
            if (roleId > 0)
            {
                var role = _roleService.Get(g => g.Id == roleId);
                role.Name = name;
                _roleService.Update(role);
            }
            else
            {
                _roleService.Add(new Role { Name = name });
            }
            return null;
        }
        [HttpPost]
        public ActionResult DeleteRole(string id)
        {
            var roleId = Convert.ToInt32(id);
            _userRoleService.DeleteMany(g => g.Role.Id == roleId);
            _roleAccessService.DeleteMany(g => g.Role.Id == roleId);
            _roleService.Delete(roleId);
            return null;
        }
        #endregion

        #region Access
        public ActionResult AccessManagement()
        {
            SetViewBag(title: "مدیریت دسترسی ها", menuItem: "AccessManagement");
            var roleAccesses = Mapper.Map<List<Models.RoleAccess>>(_roleAccessService.GetAll().Where(w => w.Role.Id != 1).ToList());
            return View(roleAccesses);
        }
        public ActionResult NewAccess()
        {
            ViewBag.RoleList = new SelectList(_roleService.GetAll().Where(w => w.Id != 1), "Id", "Name");
            ViewBag.PageList = new SelectList(_accessService.GetAll().Where(w => w.MenuList.Id != 7), "Id", "Title");//new SelectList(EnumHelper.GetSelectList(typeof(Models.Pages)).Select(s => new SelectListItem { Text = s.Text.Replace('_', ' '), Value = s.Value }), "Value", "Text");
            return View(new Models.RoleAccess());
        }
        [HttpPost]
        public ActionResult AddAccess(string roleId, string pageId)
        {
            if (!string.IsNullOrEmpty(roleId) && !string.IsNullOrEmpty(pageId))
            {
                var pid = Convert.ToInt32(pageId);
                var rid = Convert.ToInt32(roleId);
                var role = _roleService.Get(g => g.Id == rid);
                var access = _accessService.Get(g => g.Id == pid);
                _roleAccessService.Add(new RoleAccess { Access = access, Role = role });
            }
            return null;
        }
        [HttpPost]
        public ActionResult DeleteAccess(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var aid = Convert.ToInt32(id);
                var roleAccess = _roleAccessService.Get(g => g.Id == aid);
                _roleAccessService.Delete(roleAccess);
            }
            return null;
        }
        #endregion

        #region Users
        public ActionResult UserManagement()
        {
            SetViewBag(title: "مدیریت کاربران", menuItem: "UserManagement");
            var currentTerm = _termService.Get(g => g.IsCurrentTerm);
            var groups = _educationalGroupService.GetMany(g => g.Term.Id == currentTerm.Id).Select(s => new { s.EducationalGroupCode, s.Name }).ToList();
            var users = new List<Models.User>();
            if (((Models.User)Session["UserInfo"]).IsAdministrator)
                users = _userService.GetAll().Where(w => w.Username.ToLower() != "sync" && w.Username.ToLower() != "student").ToList().Select(s => new Models.User
                {
                    Username = s.Username,
                    EducationalGroupCode = s.EducationalGroupCode,
                    EducationalGroupName = groups.Where(w => w.EducationalGroupCode == s.EducationalGroupCode).FirstOrDefault()?.Name,//_educationalGroupService.GetMany(g=> g.EducationalGroupCode == s.EducationalGroupCode).FirstOrDefault()?.Name,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    ID = s.ID,
                    IsAdministrator = s.IsAdministrator,
                    IsPowerUser = s.IsPowerUser,
                    //College = Mapper.Map<Models.College>(s.College)
                    College = s.College != null ? new Models.College { Id = s.College.Id, CollegeCode = s.College.CollegeCode, Name = s.College.Name } : null
                }).ToList();
            //else
            //{
            //    var userRoles = _userRoleService.GetMany(w => w.User.ID == ((Models.User)Session["UserInfo"]).ID).Select(s => s.Role.Id);
            //    var childRoles = new List<int>();
            //    foreach (var id in userRoles)
            //        childRoles.AddRange(_roleService.GetMany(g => g.ParentRole_Id == id).Select(s => s.Id));
            //    users = _userRoleService.GetMany(g=> childRoles.Contains(g.Role.Id) && g.User.Username.ToLower() != "sync").Select(s=> s.User).ToList() //_userService.GetMany(w => w.Username.ToLower() != "sync" && w.Roles.).ToList()
            //        .Select(s => new Models.User
            //        {
            //            Username = s.Username,
            //            EducationalGroupCode = s.EducationalGroupCode,
            //            EducationalGroupName = groups.Where(w => w.EducationalGroupCode == s.EducationalGroupCode).FirstOrDefault()?.Name,//_educationalGroupService.GetMany(g=> g.EducationalGroupCode == s.EducationalGroupCode).FirstOrDefault()?.Name,
            //            FirstName = s.FirstName,
            //            LastName = s.LastName,
            //            ID = s.ID,
            //            IsAdministrator = s.IsAdministrator,
            //            IsPowerUser = s.IsPowerUser,
            //            //College = Mapper.Map<Models.College>(s.College)
            //            College = s.College != null ? new Models.College { Id = s.College.Id, CollegeCode = s.College.CollegeCode, Name = s.College.Name } : null
            //        }).ToList();
            //}
            return View(users);
        }
        public ActionResult ShowUser(int id = 0)
        {
            var allGroups = _educationalGroupService.GetAll();
            ViewBag.CollegeList = new SelectList(_collegeService.GetAll(), "Id", "Name");
            ViewBag.GroupList = new SelectList(allGroups.Where(w => 1 == 2), "EducationalGroupCode", "Name");
            if (id > 0)
            {
                var item = _userService.Get(g => g.ID == id);
                var serviceUser = _serviceUsersMappingService.Get(g => g.Username == item.Username);
                return View(new Models.User
                {
                    //College = Mapper.Map<Models.College>(item.College),
                    College = item.College != null ? new Models.College { Id = item.College.Id, CollegeCode = item.College.CollegeCode, Name = item.College.Name } : null,
                    EducationalGroupCode = item.EducationalGroupCode,
                    EducationalGroupName = allGroups.Where(w => w.EducationalGroupCode == item.EducationalGroupCode).FirstOrDefault()?.Name,//_educationalGroupService.GetMany(g=> g.EducationalGroupCode == item.EducationalGroupCode).FirstOrDefault()?.Name,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    ID = item.ID,
                    IsAdministrator = item.IsAdministrator,
                    IsPowerUser = item.IsPowerUser,
                    ServiceUsername = serviceUser != null ? serviceUser.ServiceUsername : string.Empty,
                    Username = item.Username
                });
            }

            return View(new Models.User());
        }
        [HttpPost]
        public ActionResult AddOrUpdateUser(string id, string username, string college, string group, string firstName, string lastName, string password, string isAdministrator, string isPowerUser, string serviceUsername)
        {
            var userId = Convert.ToInt32(id);
            var actionDone = false;

            if (userId > 0)
            {
                var user = _userService.Get(g => g.ID == userId);
                if (!Convert.ToBoolean(isAdministrator) && !Convert.ToBoolean(isPowerUser))
                {
                    if (!string.IsNullOrEmpty(college))
                    {
                        var collegeId = Convert.ToInt32(college);
                        user.College = _collegeService.Get(g => g.Id == collegeId);
                    }
                    if (!string.IsNullOrEmpty(group))
                    {
                        var groupCode = Convert.ToInt32(group);
                        user.EducationalGroupCode = groupCode;//.EducationalGroup = _educationalGroupService.Get(g => g.Id == groupId);
                    }
                }
                else
                {
                    user.College = null;
                    user.EducationalGroupCode = null;
                }
                user.Username = username;
                user.FirstName = firstName;
                user.LastName = lastName;
                if (!string.IsNullOrEmpty(password))
                    user.Password = password.GetMd5Hash();
                user.IsAdministrator = Convert.ToBoolean(isAdministrator);
                user.IsPowerUser = Convert.ToBoolean(isPowerUser);
                _userService.Update(user);
                actionDone = true;
            }
            else
            {
                var existUser = _userService.Get(g => g.Username == username);
                if (username.ToLower() != "sync" && existUser == null)
                {
                    var user = new User();
                    user.Username = username;
                    if (!string.IsNullOrEmpty(college))
                    {
                        var collegeId = Convert.ToInt32(college);
                        user.College = _collegeService.Get(g => g.Id == collegeId);
                    }
                    if (!string.IsNullOrEmpty(group))
                    {
                        var groupCode = Convert.ToInt32(group);
                        user.EducationalGroupCode = groupCode;//_educationalGroupService.Get(g => g.Id == groupId);
                    }
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    if (!string.IsNullOrEmpty(password))
                        user.Password = password.GetMd5Hash();
                    user.IsAdministrator = Convert.ToBoolean(isAdministrator);
                    user.IsPowerUser = Convert.ToBoolean(isPowerUser);
                    _userService.Add(user);
                    actionDone = true;
                }
            }

            if(actionDone)
            {
                var map = _serviceUsersMappingService.Get(g => g.Username == username);
                if (map == null)
                    _serviceUsersMappingService.Add(new ServiceUsersMapping { Username = username, ServiceUsername = serviceUsername });
                else
                {
                    map.ServiceUsername = serviceUsername;
                    _serviceUsersMappingService.Update(map);
                }
            }
            return null;
        }
        [HttpPost]
        public ActionResult DeleteUser(string id)
        {
            var userId = Convert.ToInt32(id);
            _userRoleService.DeleteMany(g => g.User.ID == userId);
            _userService.Delete(userId);
            return null;
        }
        #endregion

        #region UserRoles
        [HttpPost]
        public int AddUserRole(string roleId, string userId)
        {
            var ri = 0;
            var ui = 0;
            if (int.TryParse(roleId, out ri) && int.TryParse(userId, out ui))
            {
                var ur = _userRoleService.Get(g => g.Role.Id == ri && g.User.ID == ui);
                if (ur == null)
                {
                    _userRoleService.Add(new UserRole
                    {
                        Role = _roleService.Get(g => g.Id == ri),
                        User = _userService.Get(g => g.ID == ui)
                    });
                    return 1;
                }
            }
            return 0;
        }
        [HttpPost]
        public int DeleteUserRole(string roleId, string userId)
        {
            var ri = 0;
            var ui = 0;
            if (int.TryParse(roleId, out ri) && int.TryParse(userId, out ui))
            {
                var ur = _userRoleService.Get(g => g.Role.Id == ri && g.User.ID == ui);
                if (ur != null && userId != "1")
                {
                    _userRoleService.Delete(ur);
                    return 1;
                }
            }
            return 0;
        }
        #endregion

        #region SystemManagement
        public ActionResult SystemManagement()
        {
            SetViewBag(title: "تنظیمات", menuItem: "SystemManagement");
            ViewBag.TermList = new SelectList(_termService.GetAll().OrderByDescending(o => o.TermCode), "TermCode", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult AddTerm(string title, string code)
        {
            if (!string.IsNullOrEmpty(title))
            {
                _termService.Add(new Term
                {
                    CreationDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    IsActive = true,
                    Name = title,
                    TermCode = code
                });
                ViewBag.MessageText = "s";
                ViewBag.MessageType = "success";
            }
            return null;
        }
        [HttpPost]
        public ActionResult ChooseTerm(string id)
        {
            var termId = Convert.ToInt32(id);
            var oldTerm = _termService.Get(g => g.IsCurrentTerm);
            oldTerm.IsCurrentTerm = false;
            _termService.Update(oldTerm);
            var newTerm = _termService.Get(g => g.Id == termId);
            newTerm.IsCurrentTerm = true;
            _termService.Update(newTerm);
            return null;
        }
        [HttpPost]
        public ActionResult UpdateData(string termCode)
        {
            var schedule = new ScheduleController(_scheduleService, _termService, _mappingService, _collegeService, _educationalGroupService
                , _professorService, _educationalClassService, _studentEducationalClassService, _professorScoreService
                , _indicatorService, _mappingTypeService, _universityLevelMappingService, _educationalGroupScoreService
                , _logService, _logTypeService, _userService);
            var sch = schedule.GetSchedule();
            //System.IO.File.AppendAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "log.txt", new string[] { "Test Action 0 at: " + DateTime.Now });
            if (!string.IsNullOrEmpty(sch.ActionMethod) && sch.ActionMethod.LastIndexOf('.') > 0)
                schedule.RunSchedule(sch, true, ((Web.Models.User)Session["UserInfo"]).Username, termCode);
            return null;
        }
        [HttpPost]
        public ActionResult SetScheduleTime(string timeSpan, string measurement)
        {
            decimal ts = 0;
            if (decimal.TryParse(timeSpan, out ts) && ts > 0)
            {
                var schedule = new ScheduleController(_scheduleService, _termService, _mappingService, _collegeService, _educationalGroupService
                , _professorService, _educationalClassService, _studentEducationalClassService, _professorScoreService
                , _indicatorService, _mappingTypeService, _universityLevelMappingService, _educationalGroupScoreService
                , _logService, _logTypeService, _userService);
                var sch = schedule.GetSchedule();
                sch.TimeLapse = ts;
                sch.TimeLapseMeasurement = measurement;
                schedule.AddOrUpdateSchedule(sch);
            }
            return null;
        }

        public ActionResult LogsReport()
        {
            SetViewBag(title: "گزارش سیستم", menuItem: "LogsReport");
            ViewBag.LogTypes = _logTypeService.GetAll().Select(s => new Models.LogType
            {
                CreationDate = s.CreationDate,
                Id = s.Id,
                IsActive = s.IsActive,
                LastModifiedDate = s.LastModifiedDate,
                LogTypeID = s.LogTypeID,
                Name = s.Name
            }).ToList();

            if (TempData["FilteredModel"] == null)
            {
                var model = _logService.GetAll().Select(s => new Models.Log
                {
                    Date = s.Date,
                    Desacription = s.Desacription,
                    Id = s.Id,
                    LogType = new Models.LogType
                    {
                        CreationDate = s.LogType.CreationDate,
                        Id = s.LogType.Id,
                        IsActive = s.LogType.IsActive,
                        LastModifiedDate = s.LogType.LastModifiedDate,
                        LogTypeID = s.LogType.LogTypeID,
                        Name = s.LogType.Name
                    },
                    User = Mapper.Map<Models.User>(s.User)
                }).OrderByDescending(o => o.Date).ToList();
                return View(model);
            }
            else
            {
                ViewBag.FilterLogType = TempData["FilterLogType"];
                ViewBag.FilterFromDate = TempData["FilterFromDate"];
                ViewBag.FilterToDate = TempData["FilterToDate"];
                return View(TempData["FilteredModel"]);
            }
        }

        [HttpPost]
        public ActionResult LogReportFilter(string logType = null, string fromDate = null, string toDate = null)
        {
            var allLogs = _logService.GetAll();
            decimal logTypeId = 0;
            DateTime fDate = !string.IsNullOrEmpty(fromDate) ? GeneralMethods.ConvertToGregorian(fromDate) : DateTime.MinValue;
            DateTime tDate = !string.IsNullOrEmpty(toDate) ? GeneralMethods.ConvertToGregorian(toDate) : DateTime.MinValue;
            if (decimal.TryParse(logType, out logTypeId) && logTypeId > 0)
                allLogs = allLogs.Where(w => w.LogType.Id == logTypeId).ToList();
            if (fDate != DateTime.MinValue)
                allLogs = allLogs.Where(w => w.Date >= fDate).ToList();
            if (tDate != DateTime.MinValue)
                allLogs = allLogs.Where(w => w.Date <= tDate).ToList();
            var model = allLogs.Select(s => new Models.Log
            {
                Date = s.Date,
                Desacription = s.Desacription,
                Id = s.Id,
                LogType = new Models.LogType
                {
                    CreationDate = s.LogType.CreationDate,
                    Id = s.LogType.Id,
                    IsActive = s.LogType.IsActive,
                    LastModifiedDate = s.LogType.LastModifiedDate,
                    LogTypeID = s.LogType.LogTypeID,
                    Name = s.LogType.Name
                },
                User = Mapper.Map<Models.User>(s.User)
            }).OrderByDescending(o => o.Date).ToList();

            TempData["FilteredModel"] = model;
            TempData["FilterLogType"] = logType;
            TempData["FilterFromDate"] = fromDate;
            TempData["FilterToDate"] = toDate;
            return Json(Url.Action("LogsReport", "Pages"));
        }
        #endregion

        #region ManageUniversityLevels
        public ActionResult ManageUniversityLevels()
        {
            SetViewBag(title: "مدیریت سطوح دانشگاه ها", menuItem: "ManageUniversityLevels");
            ViewBag.Scores = _indicatorService.Get(g => g.CountOfType == "p" + (int)IndicatorProfessorName.رتبه_دانشگاه_محل_تحصیل).Scores.OrderBy(o => o.Id).ToList();
            var model = Mapper.Map<List<Models.Mapping>>(_mappingService.GetMany(g => g.MappingType.Id == 4).ToList());
            return View(model);
        }
        [HttpPost]
        public string GetCurrentUniversityLevel(string id)
        {
            var universityId = Convert.ToInt32(id);
            var score = _universityLevelMappingService.Get(g => g.UniversityId == universityId);
            if (score != null)
                return score.Score.Id.ToString();
            return "-1";
        }
        [HttpPost]
        public void SetUniversityLevel(string id, string scoreId)
        {
            var indicator = _indicatorService.Get(g => g.CountOfType == "p" + (int)IndicatorProfessorName.رتبه_دانشگاه_محل_تحصیل);
            var scores = indicator.Scores.ToList();
            var universityId = Convert.ToInt32(id);
            var university = _mappingService.Get(g => g.TypeId == universityId && g.MappingType.Id == 4);
            var uniLevel = _universityLevelMappingService.Get(g => g.UniversityId == universityId);
            var _scoreId = Convert.ToInt32(scoreId);
            var score = scores.FirstOrDefault(w => w.Id == _scoreId);
            if (uniLevel == null && score != null)
            {
                _universityLevelMappingService.Add(new UniversityLevelMapping
                {
                    CreationDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    IsActive = true,
                    Score = new Model.Models.Score
                    {
                        Id = score.Id,
                    },
                    UniversityId = universityId,
                    UniversityName = university.TypeName
                });
            }
            else if (uniLevel != null && score == null)
            {
                _universityLevelMappingService.Delete(uniLevel);
            }
            else
            {
                uniLevel.Score = new Model.Models.Score
                {
                    Id = score.Id,
                };
                _universityLevelMappingService.Update(uniLevel);
            }

            //-- Set Professor Scores Begin
            var currentTerm = _termService.Get(g => g.IsCurrentTerm);
            var professors = _professorService.GetMany(g => g.UniversityStudyPlace != null && g.UniversityStudyPlace != 0 && g.UniversityStudyPlace == universityId
                && g.Term.Id == currentTerm.Id && g.IsActive == true).ToList();
            foreach (var professor in professors)
            {
                var professorScores = _professorScoreService.GetMany(g => g.Professor.Id == professor.Id && g.Term.Id == currentTerm.Id).ToList();
                var professorScoresIndicator = professorScores.Where(w => scores.Select(s => s.Id).Contains(w.Score.Id));
                //var professorScore = _professorScoreService.GetMany(g => scores.Select(s => s.Id).Contains(g.Score.Id) && g.Professor.Id == professor.Id).ToList();
                if (professorScoresIndicator.Count() > 0)
                    foreach (var ps in professorScoresIndicator)
                    {
                        ps.Score.Id = score.Id;
                        _professorScoreService.Update(ps);
                    }
                else
                    foreach (var group in _educationalClassService.GetMany(g => g.Professor.Id == professor.Id).Select(s => s.EducationalGroup))
                        _professorScoreService.Add(new ProfessorScore
                        {
                            EducationalGroup = group,
                            Professor = professor,
                            Score = score,
                            Term = currentTerm,
                            CurrentScore = Convert.ToInt32(score.Point * indicator.Ratio.Point)
                        });

            }
            //-- Set Professor Scores End
        }
        #endregion

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
        public IEnumerable<SelectListItem> Ratios()
        {
            //return new SelectList(_ratioService.GetAll(), "Id", "Name");
            return null;
        }
        [HttpPost]
        public JsonResult GetGroupsOfCollege(int id = 0)
        {
            if (id > 0)
            {
                var term = _termService.GetMany(g => g.IsCurrentTerm).FirstOrDefault();
                return Json(_educationalGroupService.GetMany(w => w.College.Id == id && w.Term.TermCode == term.TermCode).Select(s => new { Name = s.Name, Code = s.EducationalGroupCode }));
            }
            return null;
        }

        #endregion
    }
}