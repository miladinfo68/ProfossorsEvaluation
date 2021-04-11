using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class ObjectTypeService : BaseService<ObjectType>, IObjectTypeService
    {
        public ObjectTypeService(IRepository<ObjectType> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {

        }
    }
}