using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using System.Data.Entity;
using System.Linq;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class IndicatorRepository : RepositoryBase<Indicator>, IIndicatorRepository
    {
        private IDbSet<Indicator> _dbSet;
        public IndicatorRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<Indicator>();
        }
        public override IEnumerable<Indicator> GetMany(Expression<Func<Indicator, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition)
                .Include(i=> i.ObjectType)
                .Include(i=> i.Ratio)
                .Include(i=> i.Scores)
                .AsEnumerable();
        }
        public IEnumerable<Indicator> GetAllWithObjectTypeAndScore()
        {
            return DataContext.Indicators.Include(i => i.ObjectType).Include(s => s.Scores).AsEnumerable();
        }

    }
}
