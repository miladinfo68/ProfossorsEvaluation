using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class ObjectTypeRepository : RepositoryBase<ObjectType>, IObjectTypeRepository
    {
        public ObjectTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
