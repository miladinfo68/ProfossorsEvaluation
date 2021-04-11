using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class RatioService : BaseService<Ratio>, IRatioService
    {
        public RatioService(IRepository<Ratio> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}