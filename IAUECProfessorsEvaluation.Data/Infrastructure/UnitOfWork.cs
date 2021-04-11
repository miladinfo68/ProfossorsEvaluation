using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAUECProfessorsEvaluation.Data.ContextEntities;

namespace IAUECProfessorsEvaluation.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private  IDatabaseFactory _databaseFactory;
        private ProfessorsEvaluationEntities _dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        protected ProfessorsEvaluationEntities DataContext => _dataContext ?? (_dataContext = _databaseFactory.Get());

        public void Commit()
        {
            DataContext.Commit();
        }
    }
}
