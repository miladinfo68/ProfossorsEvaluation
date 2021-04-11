using System;
using System.Collections.Generic;
using System.Linq;
using IAUECProfessorsEvaluation.Core.Helper;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.IService;
using IAUECProfessorsEvaluation.Model.SyncModel;

namespace IAUECProfessorsEvaluation.Service.Service.Sync
{
    public static class SyncService
    {
        public static void LogSync(ILogService logService, ILogTypeService logTypeService, IUserService userService, User user, int logTypeId, string description = null)
        {
            if (!(logTypeId == (int)LogTypeValue.عملیات_ناموفق_جهت_ثبت_امتیازات_استاد || logTypeId == (int)LogTypeValue.عملیات_ناموفق_جهت_ثبت_امتیازات_گروه))
            {
                var logType = logTypeService.Get(x => x.LogTypeID == logTypeId);
                if (description == null)
                    logService.Add(new Log
                    {
                        Date = DateTime.Now,
                        LogType = logType,
                        User = user
                    });
                else
                    logService.Add(new Log
                    {
                        Date = DateTime.Now,
                        LogType = logType,
                        User = user,
                        Desacription = description
                    });
            }

        }

        public static void RunAll(
            ITermService termService,
            IMappingService mappingService,
            ICollegeService collegeService,
            IEducationalGroupService educationalGroupService,
            IProfessorService professorService,
            IEducationalClassService educationalClassService,
            IStudentEducationalClassService studentEducationalClassService,
            IProfessorScoreService professorScoreService,
            IIndicatorService indicatorService,
            IMappingTypeService mappingTypeService,
            IUniversityLevelMappingService universityLevelMappingService,
            IEducationalGroupScoreService educationalGroupScoreService,
            ILogService logService,
            ILogTypeService logTypeService,
            IUserService userService,
            string termCode)
        {
            //The order of the execution on the commands is important


            var user = userService.Get(x => x.Username.ToLower() == "Sync".ToLower());
            if (string.IsNullOrEmpty(termCode))
                termCode = ClientHelper.GetScalarValue<string>(StaticValue.CurrentTerm);




            //TermSync.SyncAddOrUpdateTerms(termService, logService, logTypeService, userService, user);
            //MappingSync.Run(mappingService, logService, logTypeService, userService, user);
            //CollegeSync.Run(collegeService, logService, logTypeService, userService, user);
            //ProfessorSync.SyncAddOrUpdateProfessor(professorService, logService, logTypeService, userService, user, termCode);
            //EducationalGroupSync.SyncAddOrUpdateEducationalGroup(educationalGroupService, logService, logTypeService, userService, user, termCode);
            EducationalClassSync.Run(educationalClassService, logService, logTypeService, userService, user, termCode);
            StudentEducationalClassSync.Run(studentEducationalClassService, logService, logTypeService, userService, user, termCode);

            ProfessorSync.SyncProfessorRemoveScore(professorService, logService, logTypeService, userService, user, termCode);

            ProfessorSync.SyncProfessorAddScore(professorService, professorScoreService, indicatorService, termService,
                mappingService, mappingTypeService, educationalClassService, universityLevelMappingService,
                logService, logTypeService, userService, user, termCode);

            EducationalGroupSync.SyncEducationalGroupRemoveScore(educationalGroupService
            , logService, logTypeService, userService, user, termCode);

            EducationalGroupSync.SyncEducationalGroupAddScore(educationalGroupService, educationalGroupScoreService,
               indicatorService, termService, professorService, professorScoreService, educationalClassService
               , logService, logTypeService, userService, user, termCode);


            var groupManagerScores = indicatorService.Get(w => w.CountOfType == ("g" + (int)IndicatorGroupName.ضریب_مدیر_گروه) && w.IsActive == true).Scores.Select(s => s.Id).ToList();
            var groupmanagerGroupScore = educationalGroupScoreService.GetMany(g => groupManagerScores.Contains(g.Score.Id) && g.Term.TermCode == termCode)
                .Select(s => s.EducationalGroup.EducationalGroupCode);
            var groupManagerReCalculateList = educationalGroupService
                .GetMany(w => !groupmanagerGroupScore.Contains(w.EducationalGroupCode) && w.Term.TermCode == termCode && w.IsActive == true).ToList();

            if (groupManagerReCalculateList.Count() > 0)
            {
                var professores = ClientHelper.GetValue<ProfessorSyncModel>(StaticValue.ProfessorRelativeAddress + $"/{termCode}");
                var reCalcProfs = professores.Where(w => groupManagerReCalculateList.Where(ww => ww.GroupManger != null).Select(s => s.GroupManger.ProfessorCode).Contains(w.ProfessoreCode)).ToList();
                foreach (var item in reCalcProfs)
                {
                    ProfessorSync.SyncGroupManagerProfessorScores(professorService, professorScoreService, indicatorService, termService, mappingService, mappingTypeService
                        , educationalClassService, universityLevelMappingService, item, logService, logTypeService, userService, user, educationalGroupService);
                }

                var groups = ClientHelper.GetValue<GroupSyncModel>(StaticValue.GroupRelativeAddress + $"/{termCode}");
                var reCalcGroups = groups.Where(w => groupManagerReCalculateList.Where(ww => ww.GroupManger != null).Select(s => s.EducationalGroupCode).Contains(w.EducationalGroupCode)).ToList();
                foreach (var item in reCalcGroups)
                {
                    EducationalGroupSync.SyncGroupManagerEducationalGroupScores(educationalGroupService, educationalGroupScoreService, indicatorService, termService
                        , professorService, professorScoreService, item, educationalClassService, logService, logTypeService, userService, user);
                }
            }



        }

        //public class Resualt
        //{
        //    public Dictionary<string, string> Term { get; set; }
        //    public MappingSync.ReturnValue Mapping { get; set; }
        //    public CollegeSync.ReturnValue College { get; set; }
        //    public Dictionary<string, string> Group { get; set; }
        //    public Dictionary<string, string> Professor { get; set; }
        //    public EducationalClassSync.ReturnValue EducationalClass { get; set; }
        //    public StudentEducationalClassSync.ReturnValue StudentClass { get; set; }
        //    public Dictionary<string, string> ProfessorRemoveScore { get; set; }
        //    public Dictionary<string, List<KeyValuePair<string, string>>> ProfessorAddScore { get; set; }
        //    public Dictionary<string, string> GroupRemoveScore { get; set; }
        //    public Dictionary<string, List<KeyValuePair<string, string>>> GroupAddScore { get; set; }

        //}

    }
}
