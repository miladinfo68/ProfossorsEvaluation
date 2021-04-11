using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class LogRepository : RepositoryBase<Log>, ILogRepository
    {
        private IDbSet<Log> _dbSet;
        public LogRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<Log>();
        }
        public override IEnumerable<Log> GetAll()
        {
            return _dbSet
                .Include(i => i.LogType)
                .Include(i => i.User)
                .AsEnumerable();
        }
    }
}