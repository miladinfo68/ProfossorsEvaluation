using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class CollegeService : BaseService<College>, ICollegeService
    {
        public CollegeService(IRepository<College> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public int AddOrUpdate(College college)
        {
            var rep = new CollegeRepository(new DatabaseFactory());
            return rep.AddOrUpdate(college);
        }

        public int Remove(College college)
        {
            var rep = new CollegeRepository(new DatabaseFactory());
            return rep.Remove(college);
        }
    }
}