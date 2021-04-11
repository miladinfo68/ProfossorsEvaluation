using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IAUECProfessorsEvaluation.Data.Infrastructure;

namespace IAUECProfessorsEvaluation.Service.Infrastructure
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private IRepository<T> _repository;
        private IUnitOfWork _unitOfWork;

        public BaseService(IRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;

        }


        public bool IsExist(Expression<Func<T, bool>> expression)
        {
            return _repository.IsExist(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository?.GetAll();
        }
        public virtual IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions)
        {
            return _repository.GetAll(includeExpressions);
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> expression)
        {
            return _repository?.GetMany(expression);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return _repository?.GetMany(expression)?.FirstOrDefault();
        }

        public int Add(T entity)
        {
           var typeId= _repository.Add(entity);
            SaveChange();
            _unitOfWork.Commit();
            return typeId;
        }

        public T Insert(T entity)
        {
            var model = _repository.Insert(entity);
            SaveChange();
            _unitOfWork.Commit();
            return model;
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
            SaveChange();
        }

        public void Delete(int id)
        {
            _repository.Delete(_repository.GetById(id));
            SaveChange();
        }

        public void DeleteMany(Expression<Func<T, bool>> expression)
        {
            _repository.Delete(expression);
            SaveChange();
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
            SaveChange();
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
    }
}