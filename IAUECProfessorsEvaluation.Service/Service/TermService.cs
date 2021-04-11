using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class TermService : BaseService<Term>, ITermService
    {
        public TermService(IRepository<Term> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public int AddOrUpdate(Term term)
        {
            var repo = new TermRepository(new DatabaseFactory());
           return repo.AddOrUpdate(term, true);
        }
    }
}