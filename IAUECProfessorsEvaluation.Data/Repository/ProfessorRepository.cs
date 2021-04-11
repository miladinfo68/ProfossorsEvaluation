using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IAUECProfessorsEvaluation.Model.SyncModel;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Linq.Expressions;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class ProfessorRepository : RepositoryBase<Professor>, IProfessorRepository
    {
        private IDbSet<Professor> _dbSet;
        public ProfessorRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<Professor>();
        }
        public override IEnumerable<Professor> GetAll()
        {
            return _dbSet.Include(i => i.ProfessorScores)
            .Include(i => i.Term).AsEnumerable();
        }
        public override IEnumerable<Professor> GetMany(Expression<Func<Professor, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition)
                .Include(i=> i.EducationalClasses)
                .Include(i=> i.EducationalClasses.Select(s=> s.EducationalGroup))
                .Include(i=>i.Term)
                .AsEnumerable();
        }

        public int AddOrUpdate(ProfessorSyncModel model)
        {
            var term = new TermRepository(DatabaseFactory);

            if (term.IsExist(x => x.TermCode == model.Term))
                if (IsExist(x => x.ProfessorCode == model.ProfessoreCode
                && x.Term.TermCode == model.Term))
                {
                    //ToDO Update
                    var r = Update(model);
                    if (r != 0) return 2;
                    return 3;
                }
                else
                {
                    //ToDo Add
                    var r = Add(model, term);
                    if (r != 0) return 1;
                    return 3;
                }
            else
                return 5;
        }

        private int Add(ProfessorSyncModel model, ITermRepository termRepo)
        {
           

            var p = new Professor
            {
                Term = termRepo.GetMany(x => x.TermCode == model.Term).FirstOrDefault(),
                IsActive = model.IsActive,
                UniversityWorkPlace = model.UniversityWorkPlace,
                UniversityStudyPlace = model.UniversityStudyPlace,
                AcademicDegree = model.AcademicDegree,
                TeachingExperience = model.TeachingExperience,
                ScientificRank = model.ScientificRank,
                Email = model.Email,
                Mobile = model.Mobile,
                NationalCode = model.NationalCode,
                Gender = model.Gender,
                Family = model.Family,
                Name = model.Name,
                Status = model.Status,
                ProfessorCode = model.ProfessoreCode,
                CreationDate = DateTime.Now
            };
            DataContext.Professors.Add(p);

            return DataContext.SaveChanges();
        }

        private int Update(ProfessorSyncModel model)
        {
            var pRepo = new ProfessorRepository(DatabaseFactory);
            var professor = pRepo.GetMany(x => x.ProfessorCode == model.ProfessoreCode && x.Term.TermCode == model.Term)
                .FirstOrDefault();

            professor.IsActive = model.IsActive;
            professor.UniversityWorkPlace = model.UniversityWorkPlace;
            professor.UniversityStudyPlace = model.UniversityStudyPlace;
            professor.AcademicDegree = model.AcademicDegree;
            professor.TeachingExperience = model.TeachingExperience;
            professor.ScientificRank = model.ScientificRank;
            professor.Email = model.Email;
            professor.Mobile = model.Mobile;
            professor.NationalCode = model.NationalCode;
            professor.Gender = model.Gender;
            professor.Family = model.Family;
            professor.Name = model.Name;
            professor.Status = model.Status;
            professor.LastModifiedDate = DateTime.Now;

            return DataContext.SaveChanges();

        }

        public int Remove(Professor professor)
        {
            DataContext.Professors.Remove(professor);
            return DataContext.SaveChanges();
        }
    }
}
