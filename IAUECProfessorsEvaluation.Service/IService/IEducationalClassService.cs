using System.Collections.Generic;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Model.SyncModel;

namespace IAUECProfessorsEvaluation.Service.IService
{
    public interface IEducationalClassService : IBaseService<EducationalClass>
    {
        IEnumerable<EducationalClass> GetAllWithProfessoreAndCollege();
        int AddOrUpdate(EducationalClassSyncModel educationalClass);
        int Remove(EducationalClassSyncModel educationalClass);
    }
}