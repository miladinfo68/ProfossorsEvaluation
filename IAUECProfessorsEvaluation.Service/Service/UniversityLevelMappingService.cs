using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class UniversityLevelMappingService : BaseService<UniversityLevelMapping>, IUniversityLevelMappingService
    {
        public UniversityLevelMappingService(IRepository<UniversityLevelMapping> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}