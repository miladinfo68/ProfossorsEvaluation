using System;
using System.Linq;
using System.Runtime.InteropServices;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class ProfessorService : BaseService<Professor>, IProfessorService
    {
        public ProfessorService(IRepository<Professor> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public int AddOrUpdate(ProfessorSyncModel model)
        {
            var rep = new ProfessorRepository(new DatabaseFactory());
            return rep.AddOrUpdate(model);

        }

        public int RemoveProfessorScore(int? professoreCode, string term)
        {
            var scoreRepository = new ProfessorScoreRepository(new DatabaseFactory());
            var prof = new ProfessorRepository(new DatabaseFactory());
            var professor = prof.GetMany(x => x.ProfessorCode == professoreCode && x.Term.TermCode == term).FirstOrDefault();

            return scoreRepository.Delete(x => x.Professor.Id == professor.Id && 
            !x.Score.Indicator.CountOfType.Contains("p7") && 
            !x.Score.Indicator.CountOfType.Contains("p11") && 
            !x.Score.Indicator.CountOfType.Contains("p12") &&
            !x.Score.Indicator.CountOfType.Contains("p16"), true);
            //else return -3000;
        }

        public int Remove(Professor professor)
        {
            var rep = new ProfessorRepository(new DatabaseFactory());
            return rep.Remove(professor);
        }
    }
}