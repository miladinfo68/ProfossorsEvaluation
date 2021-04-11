using System;
using System.Collections.Generic;
using System.Linq;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;
using IAUECProfessorsEvaluation.Model.SyncModel;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class EducationalClassService : BaseService<EducationalClass>, IEducationalClassService
    {
        public EducationalClassService(IRepository<EducationalClass> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
        public IEnumerable<EducationalClass> GetAllWithProfessoreAndCollege()
        {
            var repo = new EducationalClassRepository(new DatabaseFactory());
            return repo.GetAllWithProfessoreAndCollege();
        }

        public int AddOrUpdate(EducationalClassSyncModel educationalClass)
        {
            var repo = new EducationalClassRepository(new DatabaseFactory());
            return repo.AddOrUpdate(educationalClass);
        }




        public int Remove(EducationalClassSyncModel educationalClass)
        {
            var rep = new EducationalClassRepository(new DatabaseFactory());
            return rep.Remove(educationalClass,true);
        }
    }
}