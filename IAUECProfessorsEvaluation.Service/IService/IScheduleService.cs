using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Service.Infrastructure;

namespace IAUECProfessorsEvaluation.Service.IService
{
    public interface IScheduleService : IBaseService<Schedule>
    {
        void AddOrUpdate(Schedule model);
        int Remove(Schedule professor);
    }
}
