using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class ScoreRepository : RepositoryBase<Score>, IScoreRepository
    {
        private IDbSet<Score> _dbSet;
        public ScoreRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<Score>();
        }
        public override IEnumerable<Score> GetMany(Expression<Func<Score, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition)
                .Include(i => i.Indicator)
                .AsEnumerable();
        }
    }
}
