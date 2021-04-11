using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Data.ContextEntities
{
    public class ProfessorsEvaluationMockData:DropCreateDatabaseIfModelChanges<ProfessorsEvaluationEntities>
    {
        protected override void Seed(ProfessorsEvaluationEntities context)
        {
            base.Seed(context);
           
        }
    }
}
