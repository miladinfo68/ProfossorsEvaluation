using System;
using System.Linq;
using System.Linq.Expressions;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Service.Infrastructure;

namespace IAUECProfessorsEvaluation.Service.IService
{
    public interface IEducationalGroupScoreService : IBaseService<EducationalGroupScore>
    {
        IQueryable<EducationalGroupScore> GetManyWithScoreAndIndicator(
            Expression<Func<EducationalGroupScore, bool>> whereCondition);

    }
}