using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.IRepository;
using IAUECProfessorsEvaluation.Model.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class EvaluationQuestionRepository : RepositoryBase<EvaluationQuestion>, IEvaluationQuestionRepository
    {
        private IDbSet<EvaluationQuestion> _dbSet;
        public EvaluationQuestionRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<EvaluationQuestion>();
        }
        public override IEnumerable<EvaluationQuestion> GetAll()
        {
            return _dbSet.AsEnumerable();
        }
       
    }
}
