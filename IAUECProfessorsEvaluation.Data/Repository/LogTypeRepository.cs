using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class LogTypeRepository : RepositoryBase<LogType>, ILogTypeRepository
    {
        public LogTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}