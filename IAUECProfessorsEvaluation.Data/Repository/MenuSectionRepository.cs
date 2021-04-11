using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.IRepository;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class MenuSectionRepository : RepositoryBase<MenuSection>, IMenuSectionRepository
    {
        public MenuSectionRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
