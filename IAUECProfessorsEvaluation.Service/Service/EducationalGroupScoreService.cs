using System;
using System.Linq;
using System.Linq.Expressions;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class EducationalGroupScoreService : BaseService<EducationalGroupScore>,IEducationalGroupScoreService
    {
        public EducationalGroupScoreService(IRepository<EducationalGroupScore> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
        public IQueryable<EducationalGroupScore> GetManyWithScoreAndIndicator(Expression<Func<EducationalGroupScore, bool>> whereCondition)
        {
            var repo = new EducationalGroupScoreRepository(new DatabaseFactory());
            return repo.GetManyWithScoreAndIndicator(whereCondition);
        }

    }
}