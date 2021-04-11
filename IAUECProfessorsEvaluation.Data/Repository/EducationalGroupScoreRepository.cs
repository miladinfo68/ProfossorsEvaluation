using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class EducationalGroupScoreRepository : RepositoryBase<EducationalGroupScore>, IEducationalGroupScoreRepository
    {
        public EducationalGroupScoreRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
        public IQueryable<EducationalGroupScore> GetManyWithScoreAndIndicator(Expression<Func<EducationalGroupScore, bool>> whereCondition)
        {
            return DataContext.EducationalGroupScores.Where(whereCondition)
                .Include(s=>s.Score)
                .Include(si=>si.Score.Indicator)
                .Include(g=>g.EducationalGroup)
                .Include(gc => gc.EducationalGroup.College)
                .Include(t=>t.Term);
        }

    }
}
