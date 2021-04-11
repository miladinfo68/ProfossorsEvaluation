using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAUECProfessorsEvaluation.Data.ContextEntities;

namespace IAUECProfessorsEvaluation.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        ProfessorsEvaluationEntities Get();
    }
}
