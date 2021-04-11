using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.IRepository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Model.SyncModel;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class StudentEducationalClassRepository : RepositoryBase<StudentEducationalClass>, IStudentEducationalClassRepository
    {
        private IDbSet<StudentEducationalClass> _dbSet;
        public StudentEducationalClassRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<StudentEducationalClass>();
        }

        public int AddOrUpdate(StudentEducationalClassSyncModel model)
        {
            if (IsExist(x => x.EducationalClass.CodeClass == model.EducationalClassId))
            {
                if (IsExist(x => x.Term.TermCode == model.Term
                //&& x.EducationalClass.CodeClass == model.EducationalClassId
                && x.StudentId == model.StudentId))
                {
                    //ToDO Update
                    var r = Update(model);
                    if (r != 0) return 2;
                    return 3;
                }
                else
                {
                    //ToDo Add
                    var r = Add(model);
                    if (r != 0) return 1;
                    return 3;
                }
            }
            else
            {
                return 3;
            }
        }

        private int Add(StudentEducationalClassSyncModel model)
        {
            var educationalClass = new EducationalClassRepository(DatabaseFactory);
            var term = new TermRepository(DatabaseFactory);

            if (!educationalClass.IsExist(x =>
                    x.CodeClass == model.EducationalClassId && x.Term.TermCode == model.Term) ||
                !term.IsExist(x => x.TermCode == model.Term)) return 0;

            var sc = new StudentEducationalClass
            {
                Term = term.GetMany(x => x.TermCode == model.Term).FirstOrDefault(),
                CreationDate = DateTime.Now,
                EducationalClass = educationalClass
                .GetMany(x => x.CodeClass == model.EducationalClassId && x.Term.TermCode == model.Term)
                .FirstOrDefault(),
                IsActive = model.IsActive,
                Grade = model.Grade,
                ProfessorEvaluationScore = model.ProfessorEvaluationScore,
                StudentId = model.StudentId
            };
            DataContext.StudentEducationalClasses.Add(sc);
            return DataContext.SaveChanges();
        }

        private int Update(StudentEducationalClassSyncModel model)
        {
            var sc = DataContext.StudentEducationalClasses.FirstOrDefault
                (x => x.Term.TermCode == model.Term
                && x.EducationalClass.CodeClass == model.EducationalClassId
                && x.StudentId == model.StudentId);
            if (sc != null)
            {
                sc.Grade = model.Grade;
                sc.ProfessorEvaluationScore = model.ProfessorEvaluationScore;
                sc.IsActive = model.IsActive;
                sc.LastModifiedDate = DateTime.Now;
            }
            return DataContext.SaveChanges();
        }

        public int Remove(StudentEducationalClassSyncModel model, bool notUsed)
        {
            var studentClass = DataContext.StudentEducationalClasses.FirstOrDefault(y =>
                y.Term.TermCode == model.Term && y.EducationalClass.CodeClass == model.EducationalClassId &&
                y.StudentId == model.StudentId);

            if (studentClass != null) DataContext.StudentEducationalClasses.Remove(studentClass);

            return DataContext.SaveChanges();
        }

        public override IEnumerable<StudentEducationalClass> GetMany(Expression<Func<StudentEducationalClass, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition)
                .Include(i => i.EducationalClass)
                .Include(i => i.EducationalClass.Professor)
                .AsEnumerable();
        }
    }
}
