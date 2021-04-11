using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class MappingTypeRepository : RepositoryBase<MappingType>, IMappingTypeRepository
    {
        public MappingTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}