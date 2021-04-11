using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class MenuSectionService : BaseService<MenuSection>, IMenuSectionService
    {
        public MenuSectionService(IRepository<MenuSection> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
