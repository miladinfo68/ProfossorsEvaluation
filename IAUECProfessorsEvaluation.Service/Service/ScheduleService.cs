using System;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;
using IAUECProfessorsEvaluation.Data.Repository;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class ScheduleService : BaseService<Schedule>, IScheduleService
    {
        public ScheduleService(IRepository<Schedule> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public void AddOrUpdate(Schedule model)
        {
            var rep = new ScheduleRepository(new DatabaseFactory());
            rep.AddOrUpdate(model);
        }

        public int Remove(Schedule schedule)
        {
            var rep = new ScheduleRepository(new DatabaseFactory());
            return rep.Remove(schedule);
        }
    }
}
