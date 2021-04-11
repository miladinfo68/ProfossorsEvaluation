using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class RoleAccessRepository : RepositoryBase<RoleAccess>, IRoleAccessRepository
    {
        private IDbSet<RoleAccess> _dbSet;
        public RoleAccessRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<RoleAccess>();
        }
        public override IEnumerable<RoleAccess> GetAll()
        {
            return _dbSet
                .Include(i=> i.Access)
                .Include(i=> i.Role)
                .AsEnumerable();
        }
        public override IEnumerable<RoleAccess> GetMany(Expression<Func<RoleAccess, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition)
                .Include(i=> i.Access)
                .Include(i=> i.Access.MenuList)
                .Include(i=> i.Access.MenuList.MenuSection)
                .Include(i=> i.Role)
                .AsEnumerable();
        }
    }
}
