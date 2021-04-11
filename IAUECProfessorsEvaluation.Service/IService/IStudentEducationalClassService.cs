using System.Collections.Generic;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Service.Infrastructure;

namespace IAUECProfessorsEvaluation.Service.IService
{
    public interface IStudentEducationalClassService : IBaseService<StudentEducationalClass>
    {
        int AddOrUpdate(StudentEducationalClassSyncModel model);
        int Remove(StudentEducationalClassSyncModel model);
    }
}