using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Service.Infrastructure;

namespace IAUECProfessorsEvaluation.Service.IService
{
    public interface IProfessorService : IBaseService<Professor>
    {
        int AddOrUpdate(ProfessorSyncModel model);
        int RemoveProfessorScore(int? objProfessoreCode, string term);
        int Remove(Professor professor);
    }
}