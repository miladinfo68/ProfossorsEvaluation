using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class AccessRepository : RepositoryBase<Access>, IAccessRepository
    {
        private IDbSet<Access> _dbSet;
        public AccessRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<Access>();
        }
        public override IEnumerable<Access> GetAll()
        {
            return _dbSet.Include(i => i.MenuList).AsEnumerable();
        }
    }
}
