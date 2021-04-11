using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class ProfessorScoreRepository : RepositoryBase<ProfessorScore>, IProfessorScoreRepository
    {
        private IDbSet<ProfessorScore> _dbSet;
        public ProfessorScoreRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<ProfessorScore>();
        }
        public override IEnumerable<ProfessorScore> GetAll()
        {
            return _dbSet
                //.Include(i => i.Professor)
                //.Include(i=> i.Score)
                //.Include(i=> i.Score.Indicator)
                //.Include(i=> i.Term)
                .AsEnumerable();
        }
        public override IEnumerable<ProfessorScore> GetMany(Expression<Func<ProfessorScore, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition)
                .Include(i => i.Professor)
                .Include(i => i.Score)
                .Include(i => i.Score.Indicator)
                .Include(i=> i.Score.Indicator.Scores)
                .Include(i=> i.Score.Indicator.Ratio)
                .Include(i=> i.Term)
                .Include(i=>i.EducationalGroup)
                .AsEnumerable();
        }
    }
}
