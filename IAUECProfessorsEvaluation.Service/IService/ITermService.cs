using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;

namespace IAUECProfessorsEvaluation.Service.IService
{
    public interface ITermService : IBaseService<Term>
    {
        int AddOrUpdate(Term term);
    }
}