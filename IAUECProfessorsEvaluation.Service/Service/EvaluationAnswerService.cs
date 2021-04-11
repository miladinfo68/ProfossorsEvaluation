using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;


namespace IAUECProfessorsEvaluation.Service.Service
{
    public class EvaluationAnswerService : BaseService<EvaluationAnswer>, IEvaluationAnswerService
    {
        public EvaluationAnswerService(IRepository<EvaluationAnswer> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {

        }
        
    }
}
