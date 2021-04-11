using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.IService;
using IAUECProfessorsEvaluation.Service.Service;
using IAUECProfessorsEvaluation.Web.Controllers;
using IAUECProfessorsEvaluation.Web.Helpers;
using IAUECProfessorsEvaluation.Web.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace IAUECProfessorsEvaluation.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<Professor, Models.Professor>();
                x.CreateMap<Models.Professor, Professor>();
                x.CreateMap<ProfessorScore, Models.ProfessorScore>();
                x.CreateMap<Models.ProfessorScore, ProfessorScore>();
                x.CreateMap<EducationalGroup, Models.EducationalGroup>();
                x.CreateMap<Models.EducationalGroup, EducationalGroup>();
                x.CreateMap<College, Models.College>();
                x.CreateMap<Models.College, College>();
                x.CreateMap<CollegeScore, Models.CollegeScore>();
                x.CreateMap<Models.CollegeScore, CollegeScore>();
                x.CreateMap<Indicator, Models.Indicator>();
                x.CreateMap<Models.Indicator, Indicator>();
                x.CreateMap<ObjectType, Models.ObjectType>();
                x.CreateMap<Models.ObjectType, ObjectType>();
                x.CreateMap<Ratio, Models.Ratio>();
                x.CreateMap<Models.Ratio, Ratio>();
                x.CreateMap<UniversityLevelMapping, Models.UniversityLevel>();
                x.CreateMap<Models.UniversityLevel, UniversityLevelMapping>();
                x.CreateMap<Mapping, Models.Mapping>();
                x.CreateMap<Models.Mapping, Mapping>();
                x.CreateMap<MappingType, Models.MappingType>();
                x.CreateMap<Models.MappingType, MappingType>();
                x.CreateMap<Professor, Professor>();
                x.CreateMap<Score, Score>();
                x.CreateMap<Term, Term>();
                x.CreateMap<EducationalGroup, EducationalGroup>();
                x.CreateMap<RoleAccess, Models.RoleAccess>();
                x.CreateMap<Models.RoleAccess, RoleAccess>();
                x.CreateMap<MenuSection, Models.MenuSection>();
                x.CreateMap<Models.MenuSection, MenuSection>();
                x.CreateMap<Models.ScheduleModel, Schedule>();
                x.CreateMap<Schedule, Models.ScheduleModel>();
                x.CreateMap<Models.Log, Log>();
                x.CreateMap<Log, Models.Log>();
                x.CreateMap<Models.LogType, LogType>();
                x.CreateMap<LogType, Models.LogType>();
                x.CreateMap<Models.User, User>();
                x.CreateMap<User, Models.User>();
            });

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterControllers(typeof(Global).Assembly);

            builder.Register(c => new ProfessorService(new ProfessorRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IProfessorService>().InstancePerRequest();
            builder.Register(c => new ProfessorScoreService(new ProfessorScoreRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IProfessorScoreService>().InstancePerRequest();
            builder.Register(c => new EducationalClassService(new EducationalClassRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IEducationalClassService>().InstancePerRequest();
            builder.Register(c => new EducationalGroupService(new EducationalGroupRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IEducationalGroupService>().InstancePerRequest();
            builder.Register(c => new EducationalGroupScoreService(new EducationalGroupScoreRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IEducationalGroupScoreService>().InstancePerRequest();
            builder.Register(c => new StudentEducationalClassService(
                    new StudentEducationalClassRepository(new DatabaseFactory()),
                    new UnitOfWork(new DatabaseFactory())))
                .As<IStudentEducationalClassService>().InstancePerRequest();

            builder.Register(c => new CollegeService(new CollegeRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<ICollegeService>().InstancePerRequest();
            builder.Register(c => new CollegeScoreService(new CollegeScoreRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<ICollegeScoreService>().InstancePerRequest();
            builder.Register(c => new IndicatorService(new IndicatorRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IIndicatorService>().InstancePerRequest();
            builder.Register(c => new ObjectTypeService(new ObjectTypeRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IObjectTypeService>().InstancePerRequest();
            builder.Register(c => new RatioService(new RatioRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IRatioService>().InstancePerRequest();
            builder.Register(c => new TermService(new TermRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<ITermService>().InstancePerRequest();
            builder.Register(c => new ScoreService(new ScoreRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IScoreService>().InstancePerRequest();
            builder.Register(c => new UserService(new UserRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IUserService>().InstancePerRequest();
            builder.Register(c => new RoleService(new RoleRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IRoleService>().InstancePerRequest();
            builder.Register(c => new UserRoleService(new UserRoleRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IUserRoleService>().InstancePerRequest();
            builder.Register(c => new RoleAccessService(new RoleAccessRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IRoleAccessService>().InstancePerRequest();
            builder.Register(c => new AccessService(new AccessRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IAccessService>().InstancePerRequest();
            builder.Register(c => new UniversityLevelMappingService(new UniversityLevelMappingRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IUniversityLevelMappingService>().InstancePerRequest();
            builder.Register(c => new MappingService(new MappingRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IMappingService>().InstancePerRequest();
            builder.Register(c => new MappingTypeService(new MappingTypeRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IMappingTypeService>().InstancePerRequest();
            builder.Register(c => new ScheduleService(new ScheduleRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IScheduleService>().InstancePerRequest();
            builder.Register(c => new LogService(new LogRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<ILogService>().SingleInstance();
            builder.Register(c => new LogTypeService(new LogTypeRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<ILogTypeService>().SingleInstance();
            builder.Register(c => new ServiceUsersMappingService(new ServiceUsersMappingRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IServiceUsersMappingService>().SingleInstance();

            builder.Register(c => new EvaluationQuestionService(new EvaluationQuestionRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IEvaluationQuestionService>().SingleInstance();
            builder.Register(c => new EvaluationAnswerService(new EvaluationAnswerRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IEvaluationAnswerService>().SingleInstance();
            builder.Register(c => new EvaluationTypeService(new EvaluationTypeRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()))).As<IEvaluationTypeService>().SingleInstance();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //-------------------------//
            RegisterCacheEntry();
            //-------------------------//
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-Powered-By");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");

            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            if (currentContext.Request.Params.AllKeys.Contains("ReturnUrl"))
                Response.Redirect("~/Pages/Login");

            if (HttpContext.Current.Cache["ScheduleItem_1"] == null)//(HttpContext.Current.Request.Url.ToString() == WebConfigurationManager.AppSettings["BaseURL"] + "/Pages/SystemManagement/StartService")
                RegisterCacheEntry();
            if(HttpContext.Current.Cache["IsRun"] == null)
            {
                var _scheduleService = new ScheduleService(new ScheduleRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _termService = new TermService(new TermRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _mappingService = new MappingService(new MappingRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _collegeService = new CollegeService(new CollegeRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _educationalGroupService = new EducationalGroupService(new EducationalGroupRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _professorService = new ProfessorService(new ProfessorRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _educationalClassService = new EducationalClassService(new EducationalClassRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _studentEducationalClassService = new StudentEducationalClassService(new StudentEducationalClassRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _professorScoreService = new ProfessorScoreService(new ProfessorScoreRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _indicatorService = new IndicatorService(new IndicatorRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _mappingTypeService = new MappingTypeService(new MappingTypeRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _universityLevelMappingService = new UniversityLevelMappingService(new UniversityLevelMappingRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _educationalGroupScoreService = new EducationalGroupScoreService(new EducationalGroupScoreRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _logService = new LogService(new LogRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _logTypeService = new LogTypeService(new LogTypeRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
                var _userService = new UserService(new UserRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));

                var schedule = new ScheduleController(_scheduleService, _termService, _mappingService, _collegeService, _educationalGroupService
                    , _professorService, _educationalClassService, _studentEducationalClassService, _professorScoreService, _indicatorService, _mappingTypeService
                    , _universityLevelMappingService, _educationalGroupScoreService, _logService, _logTypeService, _userService);

                var schedules = schedule.ListAllSchedules();
                foreach (var scheduleItem in schedules)
                    CheckScheduleTimeLapse("ScheduleItem_" + scheduleItem.Id);
            }
        }


        private bool RegisterCacheEntry()
        {
            var _scheduleService = new ScheduleService(new ScheduleRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _termService = new TermService(new TermRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _mappingService = new MappingService(new MappingRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _collegeService = new CollegeService(new CollegeRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _educationalGroupService = new EducationalGroupService(new EducationalGroupRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _professorService = new ProfessorService(new ProfessorRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _educationalClassService = new EducationalClassService(new EducationalClassRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _studentEducationalClassService = new StudentEducationalClassService(new StudentEducationalClassRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _professorScoreService = new ProfessorScoreService(new ProfessorScoreRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _indicatorService = new IndicatorService(new IndicatorRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _mappingTypeService = new MappingTypeService(new MappingTypeRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _universityLevelMappingService = new UniversityLevelMappingService(new UniversityLevelMappingRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _educationalGroupScoreService = new EducationalGroupScoreService(new EducationalGroupScoreRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _logService = new LogService(new LogRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _logTypeService = new LogTypeService(new LogTypeRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _userService = new UserService(new UserRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));

            var schedule = new ScheduleController(_scheduleService, _termService, _mappingService, _collegeService, _educationalGroupService
                , _professorService, _educationalClassService, _studentEducationalClassService,_professorScoreService,_indicatorService,_mappingTypeService
                ,_universityLevelMappingService,_educationalGroupScoreService, _logService, _logTypeService, _userService);

            if (HttpContext.Current.Cache["IsRun"] == null)
                HttpContext.Current.Cache.Add("IsRun", "True", null, DateTime.MaxValue, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);


            var schedules = schedule.ListAllSchedules();
            foreach (var scheduleItem in schedules)
            {
                if (null != HttpContext.Current.Cache["ScheduleItem_" + scheduleItem.Id]) return false;

                HttpContext.Current.Cache.Add("ScheduleItem_" + scheduleItem.Id, "Test", null,
                    DateTime.MaxValue, TimeSpan.FromMinutes(2),
                    CacheItemPriority.Normal,
                    new CacheItemRemovedCallback(CacheItemRemovedCallback));
            }
            return true;
        }
        public async void CacheItemRemovedCallback(string key,
            object value, CacheItemRemovedReason reason)
        {
            await CheckScheduleTimeLapse(key);
            WebClient client = new WebClient();
            client.DownloadData(WebConfigurationManager.AppSettings["BaseURL"] + "/Pages/SystemManagement");
        }
        public async Task CheckScheduleTimeLapse(string key)
        {
            var log = new List<string>();
            log.Add("Test Action 1 at: " + DateTime.Now);
            //System.IO.File.AppendAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "log.txt", log);

            var _scheduleService = new ScheduleService(new ScheduleRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _termService = new TermService(new TermRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _mappingService = new MappingService(new MappingRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _collegeService = new CollegeService(new CollegeRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _educationalGroupService = new EducationalGroupService(new EducationalGroupRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _professorService = new ProfessorService(new ProfessorRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _educationalClassService = new EducationalClassService(new EducationalClassRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _studentEducationalClassService = new StudentEducationalClassService(new StudentEducationalClassRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _professorScoreService = new ProfessorScoreService(new ProfessorScoreRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _indicatorService = new IndicatorService(new IndicatorRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _mappingTypeService = new MappingTypeService(new MappingTypeRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _universityLevelMappingService = new UniversityLevelMappingService(new UniversityLevelMappingRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _educationalGroupScoreService = new EducationalGroupScoreService(new EducationalGroupScoreRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _logService = new LogService(new LogRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _logTypeService = new LogTypeService(new LogTypeRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));
            var _userService = new UserService(new UserRepository(new DatabaseFactory()), new UnitOfWork(new DatabaseFactory()));

            var schedule = new ScheduleController(_scheduleService, _termService, _mappingService, _collegeService, _educationalGroupService
                , _professorService, _educationalClassService, _studentEducationalClassService, _professorScoreService, _indicatorService, _mappingTypeService
                , _universityLevelMappingService, _educationalGroupScoreService, _logService, _logTypeService, _userService);

            var sid = Convert.ToDecimal(key.Remove(0, 13));

            log.Add("Test Action 2 at: " + DateTime.Now);
            var sch = schedule.GetSchedule(sid);
            log.Add("Test Action 3 at: " + DateTime.Now);
            if (!string.IsNullOrEmpty(sch.ActionMethod) && sch.ActionMethod.LastIndexOf('.') > 0)
                await Task.Run(() => { schedule.RunSchedule(sch); });
            log.Add("Test Action 4 at: " + DateTime.Now);
        }
    }
}