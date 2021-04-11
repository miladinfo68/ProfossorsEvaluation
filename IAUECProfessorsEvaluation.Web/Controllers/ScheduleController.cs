using AutoMapper;
using IAUECProfessorsEvaluation.Core.Helper;
using IAUECProfessorsEvaluation.Service.IService;
using IAUECProfessorsEvaluation.Service.Service.Sync;
using IAUECProfessorsEvaluation.Web.Models;
using IAUECProfessorsEvaluation.Web.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IAUECProfessorsEvaluation.Web.Controllers
{
    public class ScheduleController : Controller
    {
        private static IScheduleService _scheduleService;
        private static ITermService _termService;
        private static IMappingService _mappingService;
        private static ICollegeService _collegeService;
        private static IEducationalGroupService _educationalGroupService;
        private static IProfessorService _professorService;
        private static IEducationalClassService _educationalClassService;
        private static IStudentEducationalClassService _studentEducationalClassService;
        private static IProfessorScoreService _professorScoreService;
        private static IIndicatorService _indicatorService;
        private static IMappingTypeService _mappingTypeService;
        private static IUniversityLevelMappingService _universityLevelMappingService;
        private static IEducationalGroupScoreService _educationalGroupScoreService;
        private static ILogService _logService;
        private static ILogTypeService _logTypeService;
        private static IUserService _userService;
        public ScheduleController(IScheduleService scheduleService,
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
            IUserService userService)
        {
            _scheduleService = scheduleService;
            _termService = termService;
            _mappingService = mappingService;
            _collegeService = collegeService;
            _educationalGroupService = educationalGroupService;
            _professorService = professorService;
            _educationalClassService = educationalClassService;
            _studentEducationalClassService = studentEducationalClassService;
            _professorScoreService = professorScoreService;
            _indicatorService = indicatorService;
            _logService = logService;
            _logTypeService = logTypeService;
            _mappingTypeService = mappingTypeService;
            _userService = userService;
            _universityLevelMappingService = universityLevelMappingService;
            _educationalGroupScoreService = educationalGroupScoreService;
        }

        public ScheduleModel GetSchedule(decimal id = 0)
        {
            if (id > 0)
                return Mapper.Map<ScheduleModel>(_scheduleService.GetMany(g => g.Id == id).FirstOrDefault());
            else
                return Mapper.Map<ScheduleModel>(_scheduleService.GetAll().LastOrDefault());
        }

        public List<ScheduleModel> ListAllSchedules()
        {
            return Mapper.Map<List<ScheduleModel>>(_scheduleService.GetMany(g => g.IsActive == true).ToList());
        }

        public void DeleteSchedule(ScheduleModel schedule)
        {
            _scheduleService.Delete(schedule.Id);
        }

        public void AddOrUpdateSchedule(ScheduleModel schedule)
        {
            _scheduleService.AddOrUpdate(Mapper.Map<Model.Models.Schedule>(schedule));
        }

        public void RunSchedule(ScheduleModel schedule, bool manualRun = false, string username = "Sync", string termCode = null)
        {
            var log = new List<string>();
            log.Add("Test Action 5 at: " + DateTime.Now);
            //System.IO.File.AppendAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "log.txt", log);
            if ((schedule.NextRunDate == null || schedule.NextRunDate == DateTime.MinValue) || schedule.NextRunDate < DateTime.Now || manualRun)
            {
                //System.IO.File.AppendAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "log.txt", new string[] { "Test Action 6 at: " + DateTime.Now });
                var actionClass = Type.GetType(schedule.ActionMethod.Remove(schedule.ActionMethod.LastIndexOf('.'))+ ",IAUECProfessorsEvaluation.Service");
                if (actionClass != null)
                {
                    //System.IO.File.AppendAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "log.txt", new string[] { "Test Action 7 at: " + DateTime.Now });
                    var action = schedule.ActionMethod.Remove(0, schedule.ActionMethod.LastIndexOf('.') + 1);
                    var method = actionClass.GetMethod(action, BindingFlags.Static | BindingFlags.Public);
                    if (method != null)
                    {
                        //System.IO.File.AppendAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "log.txt", new string[] { "Test Action 8 at: " + DateTime.Now });
                        schedule.NextRunDate = DateTime.Now.AddMinutes(UtilityFunction.GetTimeLapse(schedule.TimeLapse, schedule.TimeLapseMeasurement));
                        schedule.LastRunDate = DateTime.Now;
                        _scheduleService.AddOrUpdate(Mapper.Map<Model.Models.Schedule>(schedule));
                        //System.IO.File.AppendAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "log.txt", new string[] { "Test Action 9 at: " + DateTime.Now });
                        object[] par = new object[] {
                            _termService, _mappingService, _collegeService, _educationalGroupService, _professorService, _educationalClassService,
                            _studentEducationalClassService, _professorScoreService, _indicatorService, _mappingTypeService, _universityLevelMappingService,
                            _educationalGroupScoreService, _logService, _logTypeService, _userService, termCode };
                        var user = _userService.Get(x => x.Username.ToLower() == username.ToLower());
                        //System.IO.File.AppendAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "log.txt", new string[] { "Test Action 10 at: " + DateTime.Now });
                        SyncService.LogSync(_logService, _logTypeService, _userService, user, (int)LogTypeValue.شروع_عملیات_بروزرسانی_داده_ها);
                        method.Invoke(null, par);
                        SyncService.LogSync(_logService, _logTypeService, _userService, user, (int)LogTypeValue.پایان_عملیات_بروزرسانی_داده_ها);
                    }
                }
            }
        }
    }
}