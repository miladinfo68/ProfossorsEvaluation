using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        int Add(T entity);
        T Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> whereCondition);
        T GetById(int id);
        T GetById(Guid id);

        IEnumerable<T> GetAll();
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> whereCondition);

        bool IsExist(Expression<Func<T, bool>> expression);


    }
}
