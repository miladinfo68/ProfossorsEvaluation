using System.Collections.Generic;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;

namespace IAUECProfessorsEvaluation.Service.IService
{
    public interface IIndicatorService : IBaseService<Indicator>
    {
        IEnumerable<Indicator> GetAllWithObjectTypeAndScore();
    }
}