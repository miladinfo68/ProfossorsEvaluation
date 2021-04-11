using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class UniversityLevelMappingRepository : RepositoryBase<UniversityLevelMapping>, IUniversityLevelMappingRepository
    {
        private IDbSet<UniversityLevelMapping> _dbSet;
        public UniversityLevelMappingRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<UniversityLevelMapping>();
        }
        public override IEnumerable<UniversityLevelMapping> GetMany(Expression<Func<UniversityLevelMapping, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition)
                .Include(i => i.Score)
                .AsEnumerable();
        }
    }
}