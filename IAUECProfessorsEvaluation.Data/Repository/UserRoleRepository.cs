using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        private IDbSet<UserRole> _dbSet;
        public UserRoleRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<UserRole>();
        }
        public override IEnumerable<UserRole> GetMany(Expression<Func<UserRole, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition)
                .Include(i=> i.Role)
                .Include(i=> i.User)
                .AsEnumerable();
        }
    }
}
