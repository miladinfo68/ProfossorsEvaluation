using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(IRepository<Role> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}