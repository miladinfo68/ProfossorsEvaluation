using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class AccessService : BaseService<Access>, IAccessService
    {
        public AccessService(IRepository<Access> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
