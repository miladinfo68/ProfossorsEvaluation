using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;
using IAUECProfessorsEvaluation.Model.SyncModel;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class MappingService : BaseService<Mapping>, IMappingService
    {
        public MappingService(IRepository<Mapping> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
        public int AddOrUpdate(MappingSyncModel mapping)
        {
            var rep = new MappingRepository(new DatabaseFactory());
            return rep.AddOrUpdate(mapping);
        }

        public int Remove(MappingSyncModel mappingSyncModel)
        {
            var repo= new MappingRepository(new DatabaseFactory());
            return repo.Remove(mappingSyncModel, true);
        }
    }
}