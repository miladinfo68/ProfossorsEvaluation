using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.IRepository;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class MenuListRepository : RepositoryBase<MenuList>, IMenuListRepository
    {
        public MenuListRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
