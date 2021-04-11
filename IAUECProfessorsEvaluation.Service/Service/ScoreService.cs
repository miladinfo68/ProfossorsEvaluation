using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class ScoreService : BaseService<Score>, IScoreService
    {
        public ScoreService(IRepository<Score> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}