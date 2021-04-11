using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.IRepository;
using IAUECProfessorsEvaluation.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class CollegeRepository : RepositoryBase<College>, ICollegeRepository
    {
        private IDbSet<College> _dbSet;
        public CollegeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<College>();
        }
        public override IEnumerable<College> GetAll()
        {
            return _dbSet
                //.Include(i => i.EducationalGroups)
                //.Include(i => i.EducationalGroups.Select(s => s.EducationalClasses))
                //.Include(i => i.EducationalGroups.Select(s => s.EducationalClasses.Select(ss => ss.Term)))
                //.Include(i => i.EducationalGroups.Select(s => s.EducationalClasses.Select(ss => ss.Professor)))
                //.Include(i => i.EducationalGroups.Select(s => s.EducationalClasses.Select(ss => ss.Professor.ProfessorScores)))
                //.Include(i => i.EducationalGroups.Select(s => s.EducationalClasses.Select(ss => ss.Professor.ProfessorScores.Select(ssss => ssss.Term))))
                .AsEnumerable();
        }
        public override IEnumerable<College> GetMany(Expression<Func<College, bool>> whereCondition)
        {
            return _dbSet
                .Include(i => i.EducationalGroups)
                .Include(i => i.EducationalGroups.Select(s => s.EducationalClasses))
                .Include(i => i.EducationalGroups.Select(s => s.EducationalClasses.Select(ss => ss.Term)))
                .Include(i => i.EducationalGroups.Select(s => s.EducationalClasses.Select(ss => ss.Professor)))
                .Include(i => i.EducationalGroups.Select(s => s.EducationalClasses.Select(ss => ss.Professor.ProfessorScores)))
                .Include(i => i.EducationalGroups.Select(s => s.EducationalClasses.Select(ss => ss.Professor.ProfessorScores.Select(ssss => ssss.Term))))
                .Include(i => i.EducationalGroups.Select(s => s.EducationalClasses.Select(ss => ss.Professor.ProfessorScores.Select(ssss => ssss.EducationalGroup.College))))
                .Where(whereCondition).AsEnumerable();
        }

        public int AddOrUpdate(College college)
        {

            if (IsExist(x => x.CollegeCode == college.CollegeCode))
            {
                //ToDO Update
                try
                {
                    var r = Update(college);
                    if (r != 0) return 2;
                    return 3;
                }
                catch (Exception e)
                {
                    return 3;
                }
            }
            else
            {
                try
                {
                    //ToDo Add
                    var r = Add(college);
                    if (r != 0) return 1;
                    return 3;
                }
                catch (Exception e)
                {
                    return 3;
                }
            }

        }

        public new int Update(College collegeEntry)
        {
            var c = DataContext.Colleges.FirstOrDefault(x => x.CollegeCode == collegeEntry.CollegeCode);
            c.Name = collegeEntry.Name;
            c.LastModifiedDate = DateTime.Now;
            c.IsActive = collegeEntry.IsActive;
            return DataContext.SaveChanges();
        }
        public new int Add(College collegeEntry)
        {
            var c = new College
            {
                CollegeCode = collegeEntry.CollegeCode,
                Name = collegeEntry.Name,
                CreationDate = DateTime.Now,
                IsActive = collegeEntry.IsActive
            };
            DataContext.Colleges.Add(c);
            return DataContext.SaveChanges();
        }

        public int Remove(College college)
        {
            DataContext.Colleges.Remove(college);
            return DataContext.SaveChanges();
        }
    }
}
