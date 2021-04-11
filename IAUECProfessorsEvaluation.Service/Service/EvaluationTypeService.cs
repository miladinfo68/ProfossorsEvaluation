using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class EvaluationTypeService : BaseService<EvaluationType>, IEvaluationTypeService
    {
        public EvaluationTypeService(IRepository<EvaluationType> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
