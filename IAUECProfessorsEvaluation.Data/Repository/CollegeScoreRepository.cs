using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.IRepository;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class CollegeScoreRepository : RepositoryBase<CollegeScore>, ICollegeScoreRepository
    {
        public CollegeScoreRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
