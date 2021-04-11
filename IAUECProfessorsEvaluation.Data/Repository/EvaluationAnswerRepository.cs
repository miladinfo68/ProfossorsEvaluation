using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.IRepository;
using IAUECProfessorsEvaluation.Model.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class EvaluationAnswerRepository : RepositoryBase<EvaluationAnswer>, IEvaluationAnswerRepository
    {
        private IDbSet<EvaluationAnswer> _dbSet;
        public EvaluationAnswerRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<EvaluationAnswer>();
        }
        public override IEnumerable<EvaluationAnswer> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

    }
}