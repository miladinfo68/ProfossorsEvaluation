using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAUECProfessorsEvaluation.Data.ContextEntities;

namespace IAUECProfessorsEvaluation.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private ProfessorsEvaluationEntities _dataContext;
        public ProfessorsEvaluationEntities Get()
        {
            return _dataContext ?? (_dataContext = new ProfessorsEvaluationEntities());
        }

        protected override void DisposeCore()
        {
            _dataContext?.Dispose();
        }
    }
}
