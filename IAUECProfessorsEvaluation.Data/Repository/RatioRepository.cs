using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class RatioRepository : RepositoryBase<Ratio>, IRatioRepository
    {
        public RatioRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
