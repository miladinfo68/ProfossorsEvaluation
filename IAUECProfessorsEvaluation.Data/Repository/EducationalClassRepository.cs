using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq.Expressions;
using System.Linq;
using IAUECProfessorsEvaluation.Data.IRepository;
using IAUECProfessorsEvaluation.Model.SyncModel;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class EducationalClassRepository : RepositoryBase<EducationalClass>, IEducationalClassRepository
    {
        private IDbSet<EducationalClass> _dbSet;
        public EducationalClassRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<EducationalClass>();
        }
        public override IEnumerable<EducationalClass> GetMany(Expression<Func<EducationalClass, bool>> whereCondition)
        {
            return _dbSet
                .Include(i => i.Professor)
                .Include(i => i.EducationalGroup)
                .Include(i => i.StudentEducationalClasses)
                .Where(whereCondition)
                .AsEnumerable();
        }
        public IEnumerable<EducationalClass> GetAllWithProfessoreAndCollege()
        {
            return DataContext.EducationalClasses
                .Include(p => p.Professor)
                .Include(g => g.EducationalGroup)
                .Include(c => c.EducationalGroup.College)
                .AsEnumerable();
        }

        public int AddOrUpdate(EducationalClassSyncModel educationalClass)
        {
            var groupRepo = new EducationalGroupRepository(DatabaseFactory);
            var professorRepo = new ProfessorRepository(DatabaseFactory);
            var termRepo = new TermRepository(DatabaseFactory);
            if (IsExist(x => x.CodeClass == educationalClass.CodeClass && x.Term.TermCode == educationalClass.Term))
            {
                //ToDO Update
                var r = Update(educationalClass, groupRepo, professorRepo, termRepo);
                if (r != 0) return 2;
                return 3;
            }
            else
            {
                //ToDo Add
                var r = Add(educationalClass, groupRepo, professorRepo, termRepo);
                if (r != 0) return 1;
                return 3;
            }
        }

        public int Update(EducationalClassSyncModel educationalClass, EducationalGroupRepository groupRepo, ProfessorRepository professorRepo, TermRepository termRepo)
        {


            if (!groupRepo.IsExist(y =>
                    y.EducationalGroupCode == educationalClass.GroupId
                    && y.Term.TermCode == educationalClass.Term) ||
                !professorRepo.IsExist(y =>
                    y.ProfessorCode == educationalClass.ProfessorId
                    && y.Term.TermCode == educationalClass.Term) ||
                !termRepo.IsExist(x => x.TermCode == educationalClass.Term))
            {
                return 0;
            }

            var item = DataContext.EducationalClasses
                .FirstOrDefault(f => f.CodeClass == educationalClass.CodeClass && f.Term.TermCode == educationalClass.Term);


            if (item != null)
            {

                item.CodeClass = educationalClass.CodeClass;

                item.LastModifiedDate = DateTime.Now;

                item.EducationalGroup = groupRepo
                    .GetMany(
                        g => g.EducationalGroupCode == educationalClass.GroupId
                             && g.Term.TermCode == educationalClass.Term)
                    .FirstOrDefault();

                item.IsActive = educationalClass.IsActive;

                item.Name = educationalClass.Name;

                item.OnlineHeldingCount = educationalClass.OnlineHeldingCount;

                item.LessonPlanSendDate = educationalClass.LessonPlanSendDate;
                item.AggregationExamPaperDate = educationalClass.AggregationExamPaperDate;
                item.ReceiveExamPaperDate = educationalClass.ReceiveExamPaperDate;


                //item.OthersHeldingCount = educationalClass.OthersHeldingCount;

                //item.PersentHeldingCount = educationalClass.PresentHeldingCount;

                item.Professor = professorRepo.GetMany(
                    g => g.ProfessorCode == educationalClass.ProfessorId
                         && g.Term.TermCode == educationalClass.Term).FirstOrDefault();

                if (educationalClass.ProfessorDelayAndEarlier != null)
                    item.ProfessorDelayAndEarlier = educationalClass.ProfessorDelayAndEarlier.Value;

                item.Term = termRepo.GetMany(g => g.TermCode == educationalClass.Term).FirstOrDefault();

                int datePartYear = 0;
                int datePartMonth = 0;
                int datePartDay = 0;
                if (educationalClass.DeclaringScoreDate != null 
                    && int.TryParse(educationalClass.DeclaringScoreDate.Split('/')[0], out datePartYear)
                    && int.TryParse(educationalClass.DeclaringScoreDate.Split('/')[1], out datePartMonth)
                    && int.TryParse(educationalClass.DeclaringScoreDate.Split('/')[2], out datePartDay)
                    )
                {
                    item.DeclaringScoreDate = new DateTime(datePartYear, datePartMonth, datePartDay, new PersianCalendar());
                }
                else if(educationalClass.DeclaringScoreDate != null && educationalClass.DeclaringScoreDate.Split('/').Length > 1 && educationalClass.DeclaringScoreDate.Split('/')[2].Contains(' '))
                {
                    item.DeclaringScoreDate = DateTime.Parse(educationalClass.DeclaringScoreDate);
                }
                item.HoldingExamDate = educationalClass.HoldingExamDate;
                item.LoadingQuestionDate = educationalClass.LoadingQuestionDate;
                //if (educationalClass.HoldingExamDate != null)
                //{
                //    var datePartYear = Convert.ToInt32(educationalClass.HoldingExamDate.Split('/')[0]);
                //    var datePartMonth = Convert.ToInt32(educationalClass.HoldingExamDate.Split('/')[1]);
                //    var datePartDay = Convert.ToInt32(educationalClass.HoldingExamDate.Split('/')[2]);
                //    item.HoldingExamDate = new DateTime(datePartYear, datePartMonth, datePartDay);
                //}
                //if (educationalClass.LoadingQuestionDate != null)
                //{
                //    var datePartYear = Convert.ToInt32(educationalClass.LoadingQuestionDate.Split('/')[0]);
                //    var datePartMonth = Convert.ToInt32(educationalClass.LoadingQuestionDate.Split('/')[1]);
                //    var datePartDay = Convert.ToInt32(educationalClass.LoadingQuestionDate.Split('/')[2]);
                //    item.LoadingQuestionDate = new DateTime(datePartYear, datePartMonth, datePartDay);
                //}
            }
            return DataContext.SaveChanges();
        }

        public int Add(EducationalClassSyncModel educationalClass, EducationalGroupRepository groupRepo, ProfessorRepository professorRepo, TermRepository termRepo)
        {



            if (!groupRepo.IsExist(y =>
                    y.EducationalGroupCode == educationalClass.GroupId && y.Term.TermCode == educationalClass.Term) ||
                !professorRepo.IsExist(y =>
                    y.ProfessorCode == educationalClass.ProfessorId && y.Term.TermCode == educationalClass.Term) ||
               !termRepo.IsExist(x => x.TermCode == educationalClass.Term))
            {
                return 0;
            }

            var item = new EducationalClass
            {
                CodeClass = educationalClass.CodeClass,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,

                EducationalGroup = groupRepo
                    .GetMany(
                        g => g.EducationalGroupCode == educationalClass.GroupId
                             && g.Term.TermCode == educationalClass.Term)
                    .FirstOrDefault(),

                IsActive = educationalClass.IsActive,

                Name = educationalClass.Name,

                OnlineHeldingCount = educationalClass.OnlineHeldingCount,

                OthersHeldingCount = educationalClass.OthersHeldingCount,

                PersentHeldingCount = educationalClass.PresentHeldingCount,

                Professor = professorRepo.GetMany(
                    g => g.ProfessorCode == educationalClass.ProfessorId
                         && g.Term.TermCode == educationalClass.Term).FirstOrDefault(),

                ReceiveExamPaperDate = educationalClass.ReceiveExamPaperDate,
                AggregationExamPaperDate = educationalClass.AggregationExamPaperDate,
                LessonPlanSendDate = educationalClass.LessonPlanSendDate,

                Term = termRepo.GetMany(g => g.TermCode == educationalClass.Term).FirstOrDefault()
            };

            if (educationalClass.ProfessorDelayAndEarlier != null)
                item.ProfessorDelayAndEarlier = educationalClass.ProfessorDelayAndEarlier.Value;

            if (educationalClass.DeclaringScoreDate != null)
            {
                var datePartYear = Convert.ToInt32(educationalClass.DeclaringScoreDate.Split('/')[0]);
                var datePartMonth = Convert.ToInt32(educationalClass.DeclaringScoreDate.Split('/')[1]);
                var datePartDay = Convert.ToInt32(educationalClass.DeclaringScoreDate.Split('/')[2]);
                item.DeclaringScoreDate = new DateTime(datePartYear, datePartMonth, datePartDay, new PersianCalendar());
            }
            item.HoldingExamDate = educationalClass.HoldingExamDate;
            item.LoadingQuestionDate = educationalClass.LoadingQuestionDate;
            //if (educationalClass.HoldingExamDate != null)
            //{
            //    var datePartYear = Convert.ToInt32(educationalClass.HoldingExamDate.Split('/')[0]);
            //    var datePartMonth = Convert.ToInt32(educationalClass.HoldingExamDate.Split('/')[1]);
            //    var datePartDay = Convert.ToInt32(educationalClass.HoldingExamDate.Split('/')[2]);
            //    item.HoldingExamDate = new DateTime(datePartYear, datePartMonth, datePartDay, new PersianCalendar());
            //}
            //if (educationalClass.LoadingQuestionDate != null)
            //{
            //    var datePartYear = Convert.ToInt32(educationalClass.LoadingQuestionDate.Split('/')[0]);
            //    var datePartMonth = Convert.ToInt32(educationalClass.LoadingQuestionDate.Split('/')[1]);
            //    var datePartDay = Convert.ToInt32(educationalClass.LoadingQuestionDate.Split('/')[2]);
            //    item.LoadingQuestionDate = new DateTime(datePartYear, datePartMonth, datePartDay, new PersianCalendar());
            //}

            DataContext.EducationalClasses.Add(item);
            try
            {
                return DataContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public int Remove(EducationalClassSyncModel model, bool notUsed)
        {
            var selectedClass = DataContext.EducationalClasses.SingleOrDefault(y =>
                y.CodeClass == model.CodeClass && y.Term.TermCode == model.Term);

            if (selectedClass != null) DataContext.EducationalClasses.Remove(selectedClass);

            var t = DataContext.SaveChanges();
            return t;
        }
    }
}
