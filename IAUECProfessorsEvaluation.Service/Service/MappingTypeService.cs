using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class MappingTypeService : BaseService<MappingType>, IMappingTypeService
    {
        public MappingTypeService(IRepository<MappingType> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}