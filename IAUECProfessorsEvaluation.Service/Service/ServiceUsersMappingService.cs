using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class ServiceUsersMappingService : BaseService<ServiceUsersMapping>, IServiceUsersMappingService
    {
        public ServiceUsersMappingService(IRepository<ServiceUsersMapping> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {

        }
    }
}
