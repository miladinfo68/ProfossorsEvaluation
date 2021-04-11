using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class LogTypeService : BaseService<LogType>, ILogTypeService
    {
        public LogTypeService(IRepository<LogType> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}