using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.IRepository;
using IAUECProfessorsEvaluation.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class ServiceUsersMappingRepository : RepositoryBase<ServiceUsersMapping>, IServiceUsersMappingRepository
    {
        private IDbSet<ServiceUsersMapping> _dbSet;
        public ServiceUsersMappingRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<ServiceUsersMapping>();
        }
    }
}
