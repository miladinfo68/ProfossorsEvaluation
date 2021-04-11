using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private IDbSet<User> _dbSet;
        public UserRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<User>();
        }
        public override IEnumerable<User> GetAll()
        {
            return _dbSet
                .Include(i => i.College)
                //.Include(i => i.EducationalGroup)
                .AsEnumerable();
        }
        public override IEnumerable<User> GetMany(Expression<Func<User, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition)
                .Include(i => i.College)
                //.Include(i => i.EducationalGroup)
                .AsEnumerable();
        }
    }
}
