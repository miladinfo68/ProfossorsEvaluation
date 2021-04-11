using System.Globalization;
using System.Linq;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class LogService : BaseService<Log>, ILogService
    {
        public LogService(IRepository<Log> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {

        }

        public string LastUpdate()
        {
            var lastOrDefault = base.GetMany(g=> g.LogType.LogTypeID == 60).LastOrDefault();
            if (lastOrDefault != null)
            {
                var lastDate = lastOrDefault.Date;
                var pc = new PersianCalendar();

                var date = $"{pc.GetYear(lastDate)}/{pc.GetMonth(lastDate)}/{pc.GetDayOfMonth(lastDate)}";
                var time = $"{pc.GetHour(lastDate)}:{pc.GetMinute(lastDate)}:{pc.GetSecond(lastDate)}";

                return time + "-" + date;

            }
            else
            {

                return "موجود نمی باشد";

            }

        }
    }
}