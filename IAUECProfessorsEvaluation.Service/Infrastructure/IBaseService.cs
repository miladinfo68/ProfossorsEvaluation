using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Service.Infrastructure
{
    public interface IBaseService<T> where T : class
    {
        bool IsExist(Expression<Func<T, bool>> expression);

        //*****************************************************
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> expression);
        T Get(Expression<Func<T, bool>> expression);


        //*****************************************************
        int Add(T entity);
        void Update(T entity);
        T Insert(T entity);

        //*****************************************************

        void Delete(int id);
        void DeleteMany(Expression<Func<T, bool>> expression);

        void Delete(T entity);

        //*****************************************************

        void SaveChange();

    }
}
