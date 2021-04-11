using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class CollegeScoreService : BaseService<CollegeScore>, ICollegeScoreService
    {
        public CollegeScoreService(IRepository<CollegeScore> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}