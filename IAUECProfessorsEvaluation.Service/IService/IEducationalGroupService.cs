using System.Collections.Generic;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Service.Infrastructure;

namespace IAUECProfessorsEvaluation.Service.IService
{
    public interface IEducationalGroupService : IBaseService<EducationalGroup>
    {
        IEnumerable<EducationalGroup> GetAllWithCollege();
        int AddOrUpdate(EducationalGroup educationalGroup);
        int AddOrUpdate(GroupSyncModel educationalGroup);
        int RemoveGroupScore(int? educationalGroupCode, string term);
        int Remove(EducationalGroup educationalGroup);
    }
}