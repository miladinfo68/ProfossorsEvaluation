using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.IRepository;
using IAUECProfessorsEvaluation.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class EvaluationTypeRepository : RepositoryBase<EvaluationType>, IEvaluationTypeRepository
    {
        public EvaluationTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
           
        }
    }
}
