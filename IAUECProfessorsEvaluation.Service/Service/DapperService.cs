using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAUECProfessorsEvaluation.Data.ReportModel;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.IService;


namespace IAUECProfessorsEvaluation.Service.Service
{
    public class DapperService
    {
        private DapperRepository _repository;
        private IEducationalGroupService _educationalGroupService;
        public DapperService(IEducationalGroupService educationalGroupService)
        {
            _repository = new DapperRepository();
            _educationalGroupService = educationalGroupService;

        }

        // level 1

        public IEnumerable<CollegeReportModel> GetCollegeReportAllCollege(int? modelTermId)
        {
            return _repository.GetCollegeReportAllCollege(modelTermId.Value);
        }
        public IEnumerable<CollegeReportModel> GetCollegeReportNotCollege(int? modelTermId, List<string> collegeList)
        {

            return _repository.GetCollegeReportNotCollege(modelTermId.Value, collegeList);
        }

        public IEnumerable<CollegeReportModel> GetCollegeReportAllCollegWithIndicator(int? modelTermId, IEnumerable<int?> indicatorList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetCollegeReportAllCollegWithIndicator(modelTermId.Value, condition);
        }
        public IEnumerable<CollegeReportModel> GetCollegeReportAllCollegeWithScore(int? modelTermId, IEnumerable<int?> socreList)
        {
            var condition = socreList.Where(x => x.HasValue).ToArray();

            return _repository.GetCollegeReportAllCollegeWithScore(modelTermId.Value, condition);
        }
        public IEnumerable<CollegeReportModel> GetCollegeReportNotAllCollegeWithIndicator(int? modelTermId, IEnumerable<int?> indicatorList, List<string> collegeList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetCollegeReportNotAllCollegeWithIndicator(modelTermId.Value, condition, collegeList);
        }
        public IEnumerable<CollegeReportModel> GetCollegeReportNotAllCollegeWithScore(int? modelTermId, IEnumerable<int?> socreList, List<string> collegeList)
        {
            var condition = socreList.Where(x => x.HasValue).ToArray();

            return _repository.GetCollegeReportNotAllCollegeWithScore(modelTermId.Value, condition, collegeList);
        }

        //level 2

        public IEnumerable<GroupReportModel> GetGroupReportAllCollegeAllGroup
            (int? modelTermId)
        {
            return _repository.GetGroupReportAllCollegeAllGroup(modelTermId.Value);
        }

        public IEnumerable<GroupReportModel> GetGroupReportAllCollegeNotGroup
            (int? modelTermId, List<string> groupList)
        {
            return _repository.GetGroupReportAllCollegeNotGroup(modelTermId.Value, groupList);
        }

        public IEnumerable<GroupReportModel> GetGroupReportNotCollegeAllGroup
            (int? modelTermId, List<string> collegeList)
        {
            return _repository.GetGroupReportNotCollegeAllGroup
                (modelTermId.Value, collegeList);
        }

        public IEnumerable<GroupReportModel> GetGroupReportNotCollegeNotGroup
            (int? modelTermId, List<string> groupList, List<string> collegeList)
        {
            return _repository.GetGroupReportNotCollegeNotGroup(modelTermId.Value, groupList, collegeList);
        }


        public IEnumerable<GroupReportModel> GetGroupReportAllCollegeAllGroupWithIndicator(int? modelTermId, IEnumerable<int?> indicatorList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetGroupReportAllCollegeAllGroupWithIndicator(modelTermId.Value, condition);
        }
        public IEnumerable<GroupReportModel> GetGroupReportAllCollegeAllGroupWithScore(int? modelTermId, IEnumerable<int?> socreList)
        {
            var condition = socreList.Where(x => x.HasValue).ToArray();

            return _repository.GetGroupReportAllCollegeAllGroupWithScore(modelTermId.Value, condition);
        }

        public IEnumerable<GroupReportModel> GetGroupReportAllCollegeNotAllGroupWithIndicator(int? modelTermId, IEnumerable<int?> indicatorList, List<string> groupList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetGroupReportAllCollegeNotAllGroupWithIndicator(modelTermId.Value, condition, groupList);
        }
        public IEnumerable<GroupReportModel> GetGroupReportAllCollegeNotAllGroupWithScore(int? modelTermId, IEnumerable<int?> indicatorList, List<string> groupList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetGroupReportAllCollegeNotGroupWithScore(modelTermId.Value, condition, groupList);
        }


        public IEnumerable<GroupReportModel> GetGroupReportNotCollegeAllGroupWithIndicator
            (int? modelTermId, IEnumerable<int?> indicatorList, List<string> collegeList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetGroupReportNotCollegeAllGroupWithIndicator(modelTermId.Value, condition, collegeList);
        }
        public IEnumerable<GroupReportModel> GetGroupReportNotCollegeAllGroupWithScore
            (int? modelTermId, IEnumerable<int?> socreList, List<string> collegeList)
        {
            var condition = socreList.Where(x => x.HasValue).ToArray();

            return _repository.GetGroupReportNotCollegeAllGroupWithScore(modelTermId.Value, condition, collegeList);
        }

        public IEnumerable<GroupReportModel> GetGroupReportNotCollegeNotAllGroupWithIndicator
            (int? modelTermId, IEnumerable<int?> indicatorList, List<string> groupList, List<string> collegeList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetGroupReportNotCollegeNotAllGroupWithIndicator(modelTermId.Value, condition, groupList, collegeList);
        }
        public IEnumerable<GroupReportModel> GetGroupReportNotCollegeNotAllGroupWithScore
            (int? modelTermId, IEnumerable<int?> indicatorList, List<string> groupList, List<string> collegeList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetGroupReportNotCollegeNotGroupWithScore(modelTermId.Value, condition, groupList, collegeList);
        }



        //level 3

        //all college all group all Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeAllGroupAllProfessor
            (int? modelTermId)
        {
            return _repository.GetProfessorReportAllCollegeAllGroupAllProfessor(modelTermId.Value);
        }

        //all college all group not Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeAllGroupNotProfessor
            (int? modelTermId, int[] professoresList)
        {
            return _repository.GetProfessorReportAllCollegeAllGroupNotProfessor
                (modelTermId.Value, professoresList);
        }

        //all college not group all Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeNotGroupAllProfessor
            (int? modelTermId, List<string> groupList)
        {
            return _repository.GetProfessorReportAllCollegeNotGroupAllProfessor
                (modelTermId.Value, groupList);
        }

        //all college not group not Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeNotGroupNotProfessor
            (int? modelTermId, List<string> groupList, int[] professoresList)
        {
            return _repository.GetProfessorReportAllCollegeNotGroupNotProfessor
                (modelTermId.Value, groupList, professoresList);
        }

        //not college all group all Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeAllGroupAllProfessor
            (int? modelTermId, List<string> collegeList)
        {
            return _repository.GetProfessorReportNotCollegeAllGroupAllProfessor
                (modelTermId.Value, collegeList);
        }

        //not college all group not Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeAllGroupNotProfessor
            (int? modelTermId, List<string> collegeList, int[] professoresList)
        {
            return _repository.GetProfessorReportNotCollegeAllGroupNotProfessor
                (modelTermId.Value, collegeList, professoresList);
        }

        //not college not group all Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeNotGroupAllProfessor
            (int? modelTermId, List<string> collegeList, List<string> groupList)
        {
            return _repository.GetProfessorReportNotCollegeNotGroupAllProfessor
                (modelTermId.Value, collegeList, groupList);
        }

        //not college not group not Professor
        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeNotGroupNotProfessor
            (int? modelTermId, List<string> collegeList, List<string> groupList, int[] professoresList)
        {
            return _repository.GetProfessorReportNotCollegeNotGroupNotProfessor
                (modelTermId.Value, collegeList, groupList, professoresList);
        }


        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeAllGroupAllProfessorWithIndicator
            (int? modelTermId, IEnumerable<int?> indicatorList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetProfessorReportAllCollegeAllGroupAllProfessorWithIndicator(modelTermId.Value, condition);
        }

        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeAllGroupAllProfessorWithScore
            (int? modelTermId, IEnumerable<int?> socreList)
        {
            var condition = socreList.Where(x => x.HasValue).ToArray();

            return _repository.GetProfessorReportAllCollegeAllGroupAllProfessorWithScore(modelTermId.Value, condition);
        }

        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeAllGroupNotProfessorWithIndicator
            (int? modelTermId, IEnumerable<int?> indicatorList, int[] professoresList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetProfessorReportAllCollegeAllGroupNotProfessorWithIndicator
                (modelTermId.Value, condition, professoresList);
        }

        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeAllGroupNotProfessorWithScore
            (int? modelTermId, IEnumerable<int?> scoresList, int[] professoresList)
        {
            var condition = scoresList.Where(x => x.HasValue).ToArray();
            return _repository.GetProfessorReportAllCollegeAllGroupNotProfessorWithScore
                (modelTermId.Value, condition, professoresList);
        }


        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeNotGroupAllProfessorWithIndicator
            (int? modelTermId, IEnumerable<int?> indicatorList, List<string> groupList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetProfessorReportAllCollegeNotGroupAllProfessorWithIndicator
                (modelTermId.Value, condition, groupList);
        }

        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeNotGroupAllProfessorWithScore
            (int? modelTermId, IEnumerable<int?> socreList, List<string> groupList)
        {
            var condition = socreList.Where(x => x.HasValue).ToArray();

            return _repository.GetProfessorReportAllCollegeNotGroupAllProfessorWithScore
                (modelTermId.Value, condition, groupList);
        }

        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeNotGroupNotProfessorWithIndicator
            (int? modelTermId, IEnumerable<int?> indicatorList, List<string> groupList, int[] professoresList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetProfessorReportAllCollegeNotGroupNotProfessorWithIndicator
                (modelTermId.Value, condition, groupList, professoresList);
        }

        public IEnumerable<ProfessorReportModel> GetProfessorReportAllCollegeNotGroupNotProfessorWithScore
            (int? modelTermId, IEnumerable<int?> scoresList, List<string> groupList, int[] professoresList)
        {
            var condition = scoresList.Where(x => x.HasValue).ToArray();
            return _repository.GetProfessorReportAllCollegeNotGroupNotProfessorWithScore
                (modelTermId.Value, condition, groupList, professoresList);
        }






        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeAllGroupAllProfessorWithIndicator
            (int? modelTermId, IEnumerable<int?> indicatorList, List<string> collegeList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetProfessorReportNotCollegeAllGroupAllProfessorWithIndicator
                (modelTermId.Value, condition, collegeList);
        }

        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeAllGroupAllProfessorWithScore
            (int? modelTermId, IEnumerable<int?> socreList, List<string> collegeList)
        {
            var condition = socreList.Where(x => x.HasValue).ToArray();

            return _repository.GetProfessorReportNotCollegeAllGroupAllProfessorWithScore
                (modelTermId.Value, condition, collegeList);
        }

        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeAllGroupNotProfessorWithIndicator
            (int? modelTermId, IEnumerable<int?> indicatorList, List<string> collegeList, int[] professoresList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetProfessorReportNotCollegeAllGroupNotProfessorWithIndicator
                (modelTermId.Value, condition, collegeList, professoresList);
        }

        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeAllGroupNotProfessorWithScore
            (int? modelTermId, IEnumerable<int?> scoresList, List<string> collegeList, int[] professoresList)
        {
            var condition = scoresList.Where(x => x.HasValue).ToArray();
            return _repository.GetProfessorReportNotCollegeAllGroupNotProfessorWithScore
                (modelTermId.Value, condition, collegeList, professoresList);
        }


        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeNotGroupAllProfessorWithIndicator
            (int? modelTermId, IEnumerable<int?> indicatorList, List<string> collegeList, List<string> groupList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetProfessorReportNotCollegeNotGroupAllProfessorWithIndicator
                (modelTermId.Value, condition, collegeList, groupList);
        }

        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeNotGroupAllProfessorWithScore
            (int? modelTermId, IEnumerable<int?> socreList, List<string> collegeList, List<string> groupList)
        {
            var condition = socreList.Where(x => x.HasValue).ToArray();

            return _repository.GetProfessorReportNotCollegeNotGroupAllProfessorWithScore
                (modelTermId.Value, condition, collegeList, groupList);
        }

        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeNotGroupNotProfessorWithIndicator
            (int? modelTermId, IEnumerable<int?> indicatorList, List<string> collegeList, List<string> groupList, int[] professoresList)
        {
            var condition = indicatorList.Where(x => x.HasValue).ToArray();
            return _repository.GetProfessorReportNotCollegeNotGroupNotProfessorWithIndicator
                (modelTermId.Value, condition, collegeList, groupList, professoresList);
        }

        public IEnumerable<ProfessorReportModel> GetProfessorReportNotCollegeNotGroupNotProfessorWithScore
            (int? modelTermId, IEnumerable<int?> scoresList, List<string> collegeList, List<string> groupList, int[] professoresList)
        {
            var condition = scoresList.Where(x => x.HasValue).ToArray();
            return _repository.GetProfessorReportNotCollegeNotGroupNotProfessorWithScore
                (modelTermId.Value, condition, collegeList, groupList, professoresList);
        }

        //Daynamic Query
        public IEnumerable<ProfessorDetialReportModel> GetProfessorReport
        (int termId, int[] indictorList, int[] scoreList,
            List<int> collegeList, List<int> groupList, int[] professoresList
            , string allColleges, string allGroups, string allProfessors,
            string allProfessorIndicators, string allProfessorScores, string allProfessorForGroupsCollege)
        {
            return _repository.GetProfessorReport
            (termId, indictorList, scoreList,
                collegeList, groupList, professoresList
                , allColleges, allGroups, allProfessors,
                allProfessorIndicators, allProfessorScores, allProfessorForGroupsCollege);
        }

        public List<ProfessorDetialReportModel> GetProfessorDetials(int termId
            , int[] indictorList,
            List<int> collegeList, List<int> groupList
            , string allColleges, string allGroups,
            string allProfessorIndicators)
        {
            return _repository.GetProfessorDetials(termId, indictorList, collegeList
                , groupList, allColleges, allGroups, allProfessorIndicators);
        }

        public IEnumerable<GroupReportModel> GetGroupReport
        (int termId, List<int> indictorList, List<int> scoreList,
            List<int> collegeList, List<int> groupList
            , string allColleges, string allGroups,
            string allGroupIndicators, string allGroupScores)
        {
            return _repository.GetGroupReport
            (termId, indictorList, scoreList,
                collegeList, groupList
                , allColleges, allGroups,
            allGroupIndicators, allGroupScores);

        }

        public IEnumerable<GroupReportModel> GetCollegeReport
        (int termId, List<int> indictorList, List<int> scoreList,
            List<int> collegeList, string allColleges,
            string allCollegeIndicators, string allCollegeScores)
        {
            return _repository.GetCollegeReport(termId, indictorList, scoreList,
                collegeList, allColleges,
                allCollegeIndicators, allCollegeScores);

        }


        public CompleteCollegeResult GetCollegeReport
                (int termId, List<int> indictorList = null, List<int> scoreList = null,
                    List<int> collegeList = null, bool allColleges = true,
                    bool allCollegeIndicators = true, bool allCollegeScores = true, int countShow = 101, int orderingType = 1)
        {
            if (indictorList == null)
                indictorList = new List<int>();
            if (scoreList == null)
                scoreList = new List<int>();

            var resualt = new CompleteCollegeResult();
            var iAllColleges = string.Empty;
            var iAllCollegeIndicators = String.Empty;
            var iAllCollegeScores = String.Empty;


            if (!allColleges)
                iAllColleges = " and c.Id in @colleges ";


            if (indictorList.Any())
                iAllCollegeIndicators = " and i.Id in @indicators ";

            if (scoreList.Any())
                iAllCollegeScores = " and g.Id in @specialGroupsList ";

            resualt.General = GetCollegeReport(termId,
                indictorList, scoreList, collegeList, iAllColleges,
                iAllCollegeIndicators, iAllCollegeScores).ToList();



            var colmodel = resualt.General;

            if (!resualt.General.Any())
                resualt.General = new List<GroupReportModel>();
            else
            {
                var tempList = new List<GroupReportModel>();
                foreach (var item in resualt.General)
                {
                    if (tempList.FirstOrDefault(x => x.CollegeId == item.CollegeId) == null)
                        tempList.Add(item);
                }

                resualt.General = tempList;


                var tempList2 = new List<GroupReportModel>();
                foreach (var item in colmodel)
                {
                    if (tempList2.FirstOrDefault(x => x.GroupId == item.GroupId) == null)
                        tempList2.Add(item);
                }

                colmodel = tempList2;
            }


            if (resualt.General != null)
            {
                if (orderingType == 2)
                {
                    if (countShow != 101)
                    {
                        resualt.General = resualt.General.OrderBy(x => x.CollegeScore)

                            .Take(countShow).ToList();
                    }
                    else
                    {
                        resualt.General = resualt.General.OrderBy(x => x.CollegeScore).ToList();
                    }
                }
                else
                {
                    if (countShow != 101)
                    {
                        resualt.General = resualt.General.OrderByDescending(x => x.CollegeScore)
                            .Take(countShow).ToList();
                    }
                    else
                    {
                        resualt.General = resualt.General.OrderByDescending(x => x.CollegeScore).ToList();
                    }
                }
            }


            resualt.Detial = colmodel;
            return resualt;

        }

        public CompleteGroupResult GetGroupReport
        (int termId, List<int> indictorList = null, List<int> scoreList = null,
            List<int> collegeList = null, List<int> groupList = null
            , bool allColleges = true, bool allGroups = true,
            bool allGroupIndicators = true, bool allGroupScores = true, int countShow = 101, int orderingType = 1)
        {
            if (indictorList == null)
                indictorList = new List<int>();
            if (scoreList == null)
                scoreList = new List<int>();
            if (collegeList == null)
                collegeList = new List<int>();
            if (groupList == null)
                groupList = new List<int>();

            var resualt = new CompleteGroupResult();

            var gAllColleges = string.Empty;
            var gAllGroups = string.Empty;
            var gAllGroupIndicators = String.Empty;
            var gAllGroupScores = String.Empty;

            if (!allColleges)
                gAllColleges = " and c.Id in @colleges ";

            if (!allGroups)
                gAllGroups = " and g.Id in @groups ";


            if (indictorList.Any())
                gAllGroupIndicators = " and i.Id in @indicators ";
            if (scoreList.Any())
                gAllGroupScores = " and g.Id in @specialGroupsList ";

            var groupResualt = GetGroupReport(termId, indictorList
                , scoreList, collegeList, groupList
                , gAllColleges, gAllGroups, gAllGroupIndicators, gAllGroupScores);




            IEnumerable<GroupReportModel> grpResualt = new List<GroupReportModel>();
            if (groupResualt != null)
            {
                grpResualt = orderingType == 2 ? groupResualt.OrderBy(x => x.GroupScore) : groupResualt.OrderByDescending(x => x.GroupScore);
            }

            var resualtGroup = new List<GroupReportModel>();
            grpResualt.GroupBy(x => x.GroupId).ToList().ForEach(x => resualtGroup.Add(x.ToList().FirstOrDefault()));

            resualt.General = countShow != 101 ? resualtGroup.Take(countShow).ToList() : resualtGroup.ToList();
            resualt.Detial = grpResualt.ToList();

            return resualt;

        }

        public CompleteProfessorResult GetProfessorReport(int termId, List<int> indictorList = null, List<int> scoreList = null,
            List<int> collegeList = null, List<int> groupList = null, List<int> professoresList = null
            , bool allColleges = true, bool allGroups = true, bool allProfessors = true,
            bool allProfessorIndicators = true, bool allProfessorScores = true, int countShow = 101, int orderingType = 1)
        {

            if (indictorList == null)
                indictorList = new List<int>();
            if (scoreList == null)
                scoreList = new List<int>();
            if (collegeList == null)
                collegeList = new List<int>();
            if (groupList == null)
                groupList = new List<int>();
            if (professoresList == null)
                professoresList = new List<int>();

            var pAllColleges = string.Empty;
            var pAllGroups = string.Empty;
            var pAllProfessors = string.Empty;
            var pAllProfessorIndicators = String.Empty;
            var pAllProfessorScores = String.Empty;
            var pAllProfessorForGroupsCollege = string.Empty;


            if (!allColleges)
                pAllColleges = " and c.Id in @colleges ";

            if (!allGroups)
                pAllGroups = " and g.Id in @groups and ps.EducationalGroup_Id in @groups ";

            if (!allProfessors)
                pAllProfessors = " and p.Id in @professores ";

            if (indictorList.Any())
                pAllProfessorIndicators = " and i.Id in @indicators ";
            if (scoreList.Any())
                pAllProfessorScores = " and p.Id in @specialProfessors ";

            if (!allColleges && allGroups)
            {
                pAllProfessorForGroupsCollege = "and ps.EducationalGroup_Id in @groups ";
                var groups = _educationalGroupService.GetMany(x => collegeList.Contains(x.College.Id)).Select(p => p.Id);
                groupList.AddRange(groups);
                var grp = groupList.Distinct();
                groupList = grp.ToList();
            }


            var professorResualt = GetProfessorReport(termId,
                indictorList.ToArray(),
                scoreList.ToArray(),
                collegeList, groupList, professoresList.ToArray(),
                pAllColleges, pAllGroups, pAllProfessors, pAllProfessorIndicators, pAllProfessorScores, pAllProfessorForGroupsCollege);


            var proResualt = new List<ProfessorDetialReportModel>();
            if (professorResualt != null)
            {

                if (orderingType == 2)
                {
                    if (countShow != 101)
                    {
                        proResualt = professorResualt.OrderBy(x => x.ProfessorScore)
                           .Take(countShow).ToList();
                    }
                    else
                    {
                        proResualt = professorResualt.OrderBy(x => x.ProfessorScore).ToList();
                    }
                }
                else
                {
                    if (countShow != 101)
                    {
                        proResualt = professorResualt.OrderByDescending(x => x.ProfessorScore)
                           .Take(countShow).ToList();
                    }
                    else
                    {
                        proResualt = professorResualt.OrderByDescending(x => x.ProfessorScore).ToList();
                    }
                }
            }

            var resualt = new CompleteProfessorResult
            {
                Detial = GetProfessorDetials(termId,
                    indictorList.ToArray(), collegeList, groupList
                    , pAllColleges, pAllGroups, pAllProfessorIndicators),
                General = proResualt
            };
            return resualt;
        }
    }
}
