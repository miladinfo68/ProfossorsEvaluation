using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class ProfessorScoreService : BaseService<ProfessorScore>, IProfessorScoreService
    {
        public ProfessorScoreService(IRepository<ProfessorScore> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}