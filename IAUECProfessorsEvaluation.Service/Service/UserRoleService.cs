using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class UserRoleService : BaseService<UserRole>, IUserRoleService
    {
        public UserRoleService(IRepository<UserRole> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
