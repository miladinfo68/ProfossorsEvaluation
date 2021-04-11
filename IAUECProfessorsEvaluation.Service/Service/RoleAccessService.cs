using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class RoleAccessService : BaseService<RoleAccess>, IRoleAccessService
    {
        public RoleAccessService(IRepository<RoleAccess> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
