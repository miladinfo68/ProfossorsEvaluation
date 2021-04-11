using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class MenuListService : BaseService<MenuList>, IMenuListService
    {
        public MenuListService(IRepository<MenuList> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
