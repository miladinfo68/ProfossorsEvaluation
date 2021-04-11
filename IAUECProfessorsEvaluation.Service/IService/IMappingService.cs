using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Model.SyncModel;

namespace IAUECProfessorsEvaluation.Service.IService
{
    public interface IMappingService : IBaseService<Mapping>
    {
        int AddOrUpdate(MappingSyncModel mapping);
        int Remove(MappingSyncModel mappingSyncModel);
    }
}