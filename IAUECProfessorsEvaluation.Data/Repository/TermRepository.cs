using System;
using System.Linq;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class TermRepository : RepositoryBase<Term>, ITermRepository
    {
        public TermRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public int AddOrUpdate(Term term, bool notUsed)
        {
            if (IsExist(x => x.TermCode == term.TermCode))
            {
                //ToDO Update
                var r = Update(term);
                if (r != 0) return 2;
                return 3;
            }
            else
            {
                //ToDo Add
                var r = Add(term);
                if (r != 0) return 1;
                return 3;
            }
        }

        public new int Update(Term term)
        {
            var c = DataContext.Terms.FirstOrDefault(x => x.TermCode == term.TermCode);
            if (c != null)
            {
                c.Name = term.Name;
                c.LastModifiedDate = DateTime.Now;
                c.IsActive = term.IsActive;
                c.IsCurrentTerm = term.IsCurrentTerm;
                c.ExamEndDate = term.ExamEndDate;
                c.ExamStartDate = term.ExamStartDate;
            }

            return DataContext.SaveChanges();
        }
        public new int Add(Term term)
        {
            var c = new Term()
            {
                TermCode = term.TermCode,
                Name = term.Name,
                CreationDate = DateTime.Now,
                IsActive = term.IsActive,
                IsCurrentTerm = term.IsCurrentTerm,
                ExamEndDate = term.ExamEndDate,
                ExamStartDate = term.ExamStartDate
            };
            DataContext.Terms.Add(c);
            return DataContext.SaveChanges();
        }
    }
}
