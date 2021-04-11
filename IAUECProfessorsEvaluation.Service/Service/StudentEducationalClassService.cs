using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class StudentEducationalClassService : BaseService<StudentEducationalClass>, IStudentEducationalClassService
    {
        public StudentEducationalClassService
            (IRepository<StudentEducationalClass> repository, IUnitOfWork unitOfWork) 
            : base(repository, unitOfWork){}



        public int AddOrUpdate(StudentEducationalClassSyncModel model)
        {
            var repo = new StudentEducationalClassRepository(new DatabaseFactory());
           return repo.AddOrUpdate(model);
        }

        public int Remove(StudentEducationalClassSyncModel model)
        {
            var repo = new StudentEducationalClassRepository(new DatabaseFactory());
            return repo.Remove(model,true);
        }

    }
}