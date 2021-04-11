using System.Collections.Generic;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class IndicatorService : BaseService<Indicator>, IIndicatorService
    {
        public IndicatorService(IRepository<Indicator> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
        public IEnumerable<Indicator> GetAllWithObjectTypeAndScore()
        {
            var repo = new IndicatorRepository(new DatabaseFactory());
            return repo.GetAllWithObjectTypeAndScore();
        }

    }
}