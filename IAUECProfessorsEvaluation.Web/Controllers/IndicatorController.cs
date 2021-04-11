using AutoMapper;
using IAUECProfessorsEvaluation.Core.Helper;
using IAUECProfessorsEvaluation.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAUECProfessorsEvaluation.Web.Controllers
{
    public class IndicatorController : Controller
    {
        private IIndicatorService _indicatorService;
        private IRatioService _ratioService;
        private IScoreService _scoreService;
        private IObjectTypeService _objectTypeService;
        private IRoleAccessService _roleAccessService;
        private IUserRoleService _userRoleService;

        public IndicatorController(
            IObjectTypeService objectTypeService
            , IIndicatorService indicatorService
            , IRatioService ratioService
            , IScoreService scoreService
            , IRoleAccessService roleAccessService
            , IUserRoleService userRoleService
            )
        {
            _indicatorService = indicatorService;
            _ratioService = ratioService;
            _scoreService = scoreService;
            _objectTypeService = objectTypeService;
            _roleAccessService = roleAccessService;
            _userRoleService = userRoleService;
        }

        #region ProfessorIndicator
        [Authorize]
        public ActionResult ProfessorIndicatorManagement()
        {
            Session["ScoreList"] = new List<Model.Models.Score>();

            SetViewBag(title: "مدیریت شاخص های اساتید", menuItem: "ProfessorIndicatorManagement");
            var professorIndicators = _indicatorService.GetMany(g => g.ObjectType.Name == "اساتید").OrderBy(o => o.Id).ToList();
            return View(Mapper.Map<List<Model.Models.Indicator>, List<Models.Indicator>>(professorIndicators));
        }

        [Authorize]
        public ActionResult AddOrUpdateProfessorIndicator(int id = 0)
        {
            var ratios = _ratioService.GetAll();
            var rationList = new List<SelectListItem>();
            foreach (var ratio in ratios)
            {
                rationList.Add(new SelectListItem
                {
                    Text = ratio.Name,
                    Value = ratio.Id.ToString()
                });
            }

            ViewBag.ratio = rationList;
            var indicator = new Models.Indicator();
            indicator.Scores = new List<Models.Score>();
            if (id > 0)
            {
                var currentIndicator = _indicatorService.Get(g => g.Id == id);
                if (currentIndicator.Ratio != null)
                    rationList.FirstOrDefault(x => x.Value == currentIndicator.Ratio.Id.ToString()).Selected = true;
                indicator = Mapper.Map<Model.Models.Indicator, Models.Indicator>(currentIndicator);
            }
            ViewBag.ratio = rationList;
            ViewBag.IndicatorHasLimit = IndicatorHasLimit(indicator.CountOfType);
            return PartialView(indicator);
        }
        [Authorize]
        public ActionResult EditScoreForIndicator(int id = 0, bool hasLimit = true)
        {
            var score = _scoreService.Get(x => x.Id == id);
            ViewBag.hasLimit = hasLimit;
            return PartialView(score);
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditScoreForIndicator1(string id, string name, string minValue, string maxValue, string point)
        {
            var num = Convert.ToInt32(id);
            var score = _scoreService.Get(x => x.Id == num);
            if (!string.IsNullOrEmpty(name) || !string.IsNullOrWhiteSpace(name))
                score.Name = name;
            if (!string.IsNullOrEmpty(minValue) || !string.IsNullOrWhiteSpace(minValue))
                score.MinValue = Convert.ToDecimal(minValue);
            else
                score.MinValue = null;
            if (!string.IsNullOrEmpty(maxValue) || !string.IsNullOrWhiteSpace(maxValue))
                score.MaxValue = Convert.ToDecimal(maxValue);
            else
                score.MaxValue = null;
            //if (!string.IsNullOrEmpty(upperBound) || !string.IsNullOrWhiteSpace(upperBound))
            //    score.UpperBound = Convert.ToInt32(upperBound);
            //else
            //    score.UpperBound = null;
            //if (!string.IsNullOrEmpty(lowerBound) || !string.IsNullOrWhiteSpace(lowerBound))
            //    score.LowerBound = Convert.ToInt32(lowerBound);
            if (!string.IsNullOrEmpty(point) || !string.IsNullOrWhiteSpace(point))
                score.Point = Convert.ToInt32(point);
            else
                score.Point = 0;
            score.LastModifiedDate = DateTime.Now;
            var indicator = _indicatorService.Get(x => x.Id == score.Indicator.Id);
            foreach (var s in indicator.Scores)
            {
                if (s.Id == score.Id)
                {
                    //s.LowerBound = score.LowerBound;
                    //s.UpperBound = score.UpperBound;
                    s.MaxValue = score.MaxValue;
                    s.MinValue = score.MinValue;
                    s.Point = score.Point;
                    s.Name = score.Name;
                }
            };
            _indicatorService.Update(indicator);
            return RedirectToAction("AddOrUpdateProfessorIndicator", new { id = score.Indicator.Id });
            //return Json(new { });
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteScoreForProfessorIndicator(int id)
        {
            var num = Convert.ToInt32(id);
            var score = _scoreService.Get(x => x.Id == num);
            var indicator = _indicatorService.Get(x => x.Id == score.Indicator.Id);
            foreach (var s in indicator.Scores)
            {
                if (s.Id == score.Id)
                {
                    _scoreService.Delete(s.Id);
                }
            };
            _indicatorService.Update(indicator);
            return RedirectToAction("AddOrUpdateProfessorIndicator", new { id = indicator.Id });
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteScoreFromIndicator(int id)
        {
            var score = _scoreService.Get(x => x.Id == id);
            var indicator = _indicatorService.Get(x => x.Id == score.Indicator.Id);
            foreach (var indicatorScore in indicator.Scores)
            {

            }
            indicator.Scores.Remove(score);
            _indicatorService.Update(indicator);
            return RedirectToAction("AddOrUpdateProfessorIndicator", new { id = score.Indicator.Id });
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddOrUpdateProfessorIndicator(Models.Indicator model)
        {
            if (model.Id > 0)
            {
                var indicator = _indicatorService.Get(g => g.Id == model.Id);
                indicator.Name = model.Name;
                indicator.ObjectType = _objectTypeService.Get(g => g.Id == 1);
                indicator.Ratio = _ratioService.Get(g => g.Id == model.Ratio.Id);
                foreach (var score in (List<Model.Models.Score>)Session["ScoreList"])
                {
                    score.Id = 0;
                    score.CreationDate = DateTime.Now;
                    indicator.Scores.Add(score);
                }
                _indicatorService.Update(indicator);
            }
            else
            {
                model.CreationDate = DateTime.Now;
                model.IsActive = true;
                model.LastModifiedDate = DateTime.Now;
                //model.ObjectType = Mapper.Map<Model.Models.ObjectType, Models.ObjectType>(_objectTypeService.Get(g => g.Id == 1));
                var ratio = _ratioService.Get(g => g.Id == model.Ratio.Id);
                var indicator = Mapper.Map<Models.Indicator, Model.Models.Indicator>(model);
                indicator.Ratio = ratio;
                indicator.ObjectType = _objectTypeService.Get(g => g.Id == 1);
                foreach (var score in (List<Model.Models.Score>)Session["ScoreList"])
                {
                    score.Id = 0;
                    score.CreationDate = DateTime.Now;
                }
                indicator.Scores = (List<Model.Models.Score>)Session["ScoreList"];
                _indicatorService.Add(indicator);
            }
            return Json(new { });
        }

        [Authorize]
        public ActionResult AddScoreProfessoreIndicator(int indexer = 1, bool hasLimit = true)
        {
            var score = new Model.Models.Score();

            //if (indexer == 1)
            //{

            //    ViewBag.indexr = indexer;
            //    return PartialView(score);
            //}
            //else
            //{
            ViewBag.indexr = indexer;
            ViewBag.hasLimit = hasLimit;
            return PartialView("AddScoreRow", score);
            //}
        }

        [Authorize]
        public ActionResult AddScoreInSession(string id, string name, string minValue, string maxValue, string point)
        {
            var score = new Model.Models.Score();
            if (id != null)
                score.Id = Convert.ToInt32(id);
            if (name != null)
                score.Name = name;
            if (minValue != null)
                score.MinValue = Convert.ToInt32(minValue);
            if (maxValue != null)
                score.MaxValue = Convert.ToInt32(maxValue);
            //if (upperBound != null)
            //    score.UpperBound = Convert.ToInt32(upperBound);
            //if (lowerBound != null)
            //    score.LowerBound = Convert.ToInt32(lowerBound);
            if (point != null && point != "0")
                score.Point = Convert.ToInt32(point);

            (Session["ScoreList"] as List<Model.Models.Score>)?.Add(score);
            return Json(new { });
        }
        [Authorize]
        public ActionResult RemoveScoreInSession(string id)
        {
            var score = (Session["ScoreList"] as List<Model.Models.Score>)?.FirstOrDefault(x => x.Id == Convert.ToInt32(id));
            (Session["ScoreList"] as List<Model.Models.Score>)?.Remove(score);
            return Json(new { });
        }
        [Authorize]
        [HttpPost]
        public ActionResult DeleteProfessorIndicator(string Id)
        {
            var indicatorId = Convert.ToInt32(Id);
            _scoreService.DeleteMany(x => x.Indicator.Id == indicatorId);

            _indicatorService.Delete(_indicatorService.Get(g => g.Id == indicatorId));

            return Json(new { });
        }

        [Authorize]
        [HttpPost]
        public ActionResult EnableOrDisableIndicator(string Id)
        {
            var indicatorId = Convert.ToInt32(Id);
            var indicator = _indicatorService.Get(g => g.Id == indicatorId);
            indicator.IsActive = (bool)indicator.IsActive ? false : true;
            _indicatorService.Update(indicator);
            return Json(new { status = indicator.IsActive });
        }
        #endregion

        #region EducationalGroupIndicator
        [Authorize]
        public ActionResult GroupIndicatorManagement()
        {
            Session["ScoreList"] = new List<Model.Models.Score>();
            SetViewBag(title: "مدیریت شاخص های گروه های آموزشی", menuItem: "GroupIndicatorManagement");
            var groupIndicators = _indicatorService.GetMany(g => g.ObjectType.Name == "گروهای آموزشی").OrderBy(o => o.Id).ToList();
            return View(Mapper.Map<List<Model.Models.Indicator>, List<Models.Indicator>>(groupIndicators));
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddOrUpdateGroupIndicator(Models.Indicator model)
        {
            if (model.Id > 0)
            {
                var indicator = _indicatorService.Get(g => g.Id == model.Id);
                indicator.Name = model.Name;
                indicator.ObjectType = _objectTypeService.Get(g => g.Id == 2);
                indicator.Ratio = _ratioService.Get(g => g.Id == model.Ratio.Id);
                foreach (var score in (List<Model.Models.Score>)Session["ScoreList"])
                {
                    score.Id = 0;
                    indicator.Scores.Add(score);
                }
                _indicatorService.Update(indicator);
            }
            else
            {
                model.CreationDate = DateTime.Now;
                model.IsActive = true;
                model.LastModifiedDate = DateTime.Now;
                var ratio = _ratioService.Get(g => g.Id == model.Ratio.Id);
                var indicator = Mapper.Map<Models.Indicator, Model.Models.Indicator>(model);
                indicator.Ratio = ratio;
                indicator.ObjectType = _objectTypeService.Get(g => g.Id == 2);
                foreach (var score in (List<Model.Models.Score>)Session["ScoreList"])
                {
                    score.Id = 0;
                    score.Indicator = null;
                }
                indicator.Scores = (List<Model.Models.Score>)Session["ScoreList"];
                _indicatorService.Add(indicator);
            }
            return Json(new { });
        }
        [Authorize]
        public ActionResult AddOrUpdateGroupIndicator(int id = 0)
        {
            var ratios = _ratioService.GetAll();
            var rationList = new List<SelectListItem>();
            foreach (var ratio in ratios)
            {
                rationList.Add(new SelectListItem
                {
                    Text = ratio.Name,
                    Value = ratio.Id.ToString()
                });
            }

            ViewBag.ratio = rationList;
            var indicator = new Models.Indicator();
            indicator.Scores = new List<Models.Score>();
            if (id > 0)
            {
                var currentIndicator = _indicatorService.Get(g => g.Id == id);
                if (currentIndicator.Ratio != null)
                    rationList.FirstOrDefault(x => x.Value == currentIndicator.Ratio.Id.ToString()).Selected = true;
                indicator = Mapper.Map<Model.Models.Indicator, Models.Indicator>(currentIndicator);
            }
            ViewBag.ratio = rationList;
            ViewBag.IndicatorHasLimit = IndicatorHasLimit(indicator.CountOfType);
            return PartialView(indicator);
        }
        [Authorize]
        public ActionResult AddScoreGroupIndicator(int indexer = 1, bool hasLimit = true)
        {
            var score = new Model.Models.Score();

            //if (indexer == 1)
            //{

            //    ViewBag.indexr = indexer;
            //    return PartialView(score);
            //}
            //else
            //{
            ViewBag.indexr = indexer;
            ViewBag.hasLimit = hasLimit;
            return PartialView("AddScoreRow", score);
            //}
        }
        [Authorize]
        [HttpPost]
        public ActionResult DeleteScoreForGroupIndicator(int id)
        {
            var num = Convert.ToInt32(id);
            var score = _scoreService.Get(x => x.Id == num);
            var indicator = _indicatorService.Get(x => x.Id == score.Indicator.Id);
            foreach (var s in indicator.Scores)
            {
                if (s.Id == score.Id)
                {
                    _scoreService.Delete(s.Id);
                }
            };
            _indicatorService.Update(indicator);
            return RedirectToAction("AddOrUpdateGroupIndicator", new { id = indicator.Id });
        }
        [Authorize]
        [HttpPost]
        public ActionResult DeleteGroupIndicator(string Id)
        {
            var indicatorId = Convert.ToInt32(Id);
            _scoreService.DeleteMany(x => x.Indicator.Id == indicatorId);
            _indicatorService.Delete(_indicatorService.Get(g => g.Id == indicatorId));
            return Json(new { });
        }
        #endregion


        [Authorize]
        public ActionResult CollegeIndicatorManagement()
        {
            SetViewBag(title: "مدیریت شاخص های دانشکده ها", menuItem: "CollegeIndicatorManagement");
            var collegeIndicators = _indicatorService.GetMany(g => g.ObjectType.Name == "دانشکده").OrderBy(o => o.Id).ToList();
            return View(Mapper.Map<List<Model.Models.Indicator>, List<Models.Indicator>>(collegeIndicators));
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

        public bool IndicatorHasLimit(string countOfType)
        {
            switch (countOfType)
            {
                case "p1": return false;
                case "p2": return true;
                case "p3": return false;
                case "p4": return false;
                case "p5": return false;
                case "p6": return true;
                case "p7": return true;
                case "p8": return true;
                case "p9": return true;
                case "p10": return true;
                case "p11": return false;
                case "p12": return false;

                case "g1": return true;
                case "g2": return true;
                case "g3": return true;
                case "g4": return true;
                case "g5": return true;
                case "g6": return true;
                case "g7": return true;
                case "g8": return true;
                case "g9": return false;
                case "g10": return false;
                case "g11": return true;
                default: return true;
            }
        }
        #endregion
    }
}