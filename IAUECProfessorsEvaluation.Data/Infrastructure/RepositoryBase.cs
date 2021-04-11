using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IAUECProfessorsEvaluation.Data.ContextEntities;
using IAUECProfessorsEvaluation.Model.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace IAUECProfessorsEvaluation.Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private ProfessorsEvaluationEntities _dataContext;
        private IDbSet<T> _dbSet;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbSet = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory { get; private set; }

        protected ProfessorsEvaluationEntities DataContext => _dataContext ?? (_dataContext = DatabaseFactory.Get());

        public int Add(T entity)
        {
            //_dbSet.Add(entity); 
            // _dataContext.SaveChanges();
            var type = typeof(T);
            return Add(entity, type);

        }
        public T Insert(T entity)
        {
            return _dbSet.Add(entity);
        }

        private int Add(T entity, Type type)
        {
            switch (type.Name)
            {
                case "Indicator":
                    var indicator = entity as Indicator;
                    if (indicator != null)
                    {
                        _dataContext.Entry(indicator.Ratio).State = EntityState.Unchanged;
                        _dataContext.Entry(indicator.ObjectType).State = EntityState.Unchanged;
                        _dataContext.Entry(indicator).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return indicator.Id;
                    }
                    break;
                case "Score":
                    var score = entity as Score;
                    if (score != null)
                    {
                        _dataContext.Entry(score.Indicator).State = EntityState.Unchanged;
                        _dataContext.Entry(score).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return score.Id;
                    }
                    break;
                case "ProfessorScore":
                    var professorScore = entity as ProfessorScore;
                    if (professorScore != null)
                    {
                        professorScore.Term = DataContext.Terms.Where(w => w.Id == professorScore.Term.Id).FirstOrDefault();
                        professorScore.Score = DataContext.Scores.Where(w => w.Id == professorScore.Score.Id).FirstOrDefault();
                        professorScore.Professor = DataContext.Professors.Where(w => w.Id == professorScore.Professor.Id).FirstOrDefault();
                        professorScore.EducationalGroup = DataContext.EducationalGroups
                            .Where(w => w.Id == professorScore.EducationalGroup.Id).FirstOrDefault();

                        _dataContext.Entry(professorScore.EducationalGroup).State = EntityState.Unchanged;
                        _dataContext.Entry(professorScore.Term).State = EntityState.Unchanged;
                        _dataContext.Entry(professorScore.Score).State = EntityState.Unchanged;
                        _dataContext.Entry(professorScore.Professor).State = EntityState.Unchanged;
                        _dataContext.Entry(professorScore).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return 0;
                    }
                    break;
                case "Professor":
                    var professor = entity as Professor;
                    if (professor != null)
                    {
                        // _dataContext.Entry(professor.Colleges).State = EntityState.Unchanged;
                        _dataContext.Entry(professor.EducationalClasses).State = EntityState.Unchanged;
                        // _dataContext.Entry(professor.EducationalGroups).State = EntityState.Unchanged;
                        _dataContext.Entry(professor.ProfessorScores).State = EntityState.Unchanged;
                        _dataContext.Entry(professor).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return professor.Id;
                    }
                    break;
                case "EducationalGroupScore":
                    var educationalGroupScore = entity as EducationalGroupScore;
                    if (educationalGroupScore != null)
                    {
                        educationalGroupScore.Term = DataContext.Terms.FirstOrDefault(w => w.Id == educationalGroupScore.Term.Id);
                        educationalGroupScore.Score = DataContext.Scores.FirstOrDefault(w => w.Id == educationalGroupScore.Score.Id);
                        educationalGroupScore.EducationalGroup = DataContext.EducationalGroups.FirstOrDefault(w => w.Id == educationalGroupScore.EducationalGroup.Id);
                        _dataContext.Entry(educationalGroupScore.Term).State = EntityState.Unchanged;
                        _dataContext.Entry(educationalGroupScore.Score).State = EntityState.Unchanged;
                        _dataContext.Entry(educationalGroupScore.EducationalGroup).State = EntityState.Unchanged;
                        _dataContext.Entry(educationalGroupScore).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return 0;
                    }
                    break;
                case "EducationalGroup":
                    var educationalGroup = entity as EducationalGroup;
                    if (educationalGroup != null)
                    {
                        //_dataContext.Entry(educationalGroup.Professors).State = EntityState.Unchanged;
                        _dataContext.Entry(educationalGroup.College).State = EntityState.Unchanged;
                        _dataContext.Entry(educationalGroup.EducationalGroupScores).State = EntityState.Unchanged;
                        _dataContext.Entry(educationalGroup).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return educationalGroup.Id;
                    }
                    break;
                case "EducationalClass":
                    var educationalClass = entity as EducationalClass;
                    if (educationalClass != null)
                    {
                        _dataContext.Entry(educationalClass.Professor).State = EntityState.Unchanged;
                        // _dataContext.Entry(educationalClass.EducationalGroups).State = EntityState.Unchanged;
                        // _dataContext.Entry(educationalClass.Colleges).State = EntityState.Unchanged;
                        _dataContext.Entry(educationalClass).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return educationalClass.Id;
                    }
                    break;
                case "CollegeScore":
                    var collegeScore = entity as CollegeScore;
                    if (collegeScore != null)
                    {
                        //_dataContext.Entry(collegeScore.Term).State = EntityState.Unchanged;
                        //_dataContext.Entry(collegeScore.Score).State = EntityState.Unchanged;
                        //_dataContext.Entry(collegeScore.College).State = EntityState.Unchanged;
                        _dataContext.Entry(collegeScore).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return 0;
                    }
                    break;
                case "Term":
                    var term = entity as Term;
                    if (term != null)
                    {
                        _dataContext.Entry(term).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return term.Id;
                    }
                    break;
                case "UniversityLevelMapping":
                    var universityLevelMapping = entity as UniversityLevelMapping;
                    if (universityLevelMapping != null)
                    {
                        _dataContext.Entry(universityLevelMapping.Score).State = EntityState.Unchanged;
                        _dataContext.Entry(universityLevelMapping).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return universityLevelMapping.Id;
                    }
                    break;
                case "User":
                    var user = entity as User;
                    if (user != null)
                    {
                        if (user.College != null)
                        {
                            user.College = DataContext.Colleges.FirstOrDefault(w => w.Id == user.College.Id);
                            _dataContext.Entry(user.College).State = EntityState.Unchanged;
                            _dataContext.SaveChanges();
                            return user.ID;
                        }
                        //if (user.EducationalGroup != null)
                        //{
                        //    user.EducationalGroup = DataContext.EducationalGroups.FirstOrDefault(w => w.Id == user.EducationalGroup.Id);
                        //    _dataContext.Entry(user.EducationalGroup).State = EntityState.Unchanged;
                        //}
                        _dataContext.Entry(user).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return user.ID;
                    }
                    break;
                case "Role":
                    var role = entity as Role;
                    if (role != null)
                    {
                        _dataContext.Entry(role).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return role.Id;
                    }
                    break;
                case "UserRole":
                    var userRole = entity as UserRole;
                    if (userRole != null)
                    {
                        userRole.Role = DataContext.Roles.FirstOrDefault(f => f.Id == userRole.Role.Id);
                        userRole.User = DataContext.Users.FirstOrDefault(f => f.ID == userRole.User.ID);
                        _dataContext.Entry(userRole.Role).State = EntityState.Unchanged;
                        _dataContext.Entry(userRole.User).State = EntityState.Unchanged;
                        _dataContext.Entry(userRole).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return userRole.Id;
                    }
                    break;
                case "RoleAccess":
                    var roleAccess = entity as RoleAccess;
                    if (roleAccess != null)
                    {
                        roleAccess.Access = DataContext.Accesses.FirstOrDefault(f => f.Id == roleAccess.Access.Id);
                        roleAccess.Role = DataContext.Roles.FirstOrDefault(f => f.Id == roleAccess.Role.Id);
                        _dataContext.Entry(roleAccess.Access).State = EntityState.Unchanged;
                        _dataContext.Entry(roleAccess.Role).State = EntityState.Unchanged;
                        _dataContext.Entry(roleAccess).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return roleAccess.Id;
                    }
                    break;
                case "Schedule":
                    var schedule = entity as Schedule;
                    if (schedule != null)
                    {
                        _dataContext.Entry(schedule).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return schedule.Id;
                    }
                    break;
                case "Log":
                    var log = entity as Log;
                    if (log != null)
                    {
                        log.User = DataContext.Users.FirstOrDefault(f => f.ID == log.User.ID);
                        log.LogType = DataContext.LogTypes.FirstOrDefault(f => f.Id == log.LogType.Id);
                        _dataContext.Entry(log.User).State = EntityState.Unchanged;
                        _dataContext.Entry(log.LogType).State = EntityState.Unchanged;
                        _dataContext.Entry(log).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return 0;

                    }
                    break;
                case "EvaluationQuestion":
                    var evaluationQuestion = entity as EvaluationQuestion;
                    if (evaluationQuestion != null)
                    {
                        evaluationQuestion.Term = DataContext.Terms.FirstOrDefault(x => x.Id == evaluationQuestion.TermId);
                        evaluationQuestion.EvaluationType = DataContext.EvaluationType.FirstOrDefault(x => x.Id == evaluationQuestion.EvaluationTypeId);
                        _dataContext.Entry(evaluationQuestion.Term).State = EntityState.Unchanged;
                        _dataContext.Entry(evaluationQuestion.EvaluationType).State = EntityState.Unchanged;
                        _dataContext.Entry(evaluationQuestion).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return evaluationQuestion.Id;
                    }
                    break;
                case "EvaluationAnswer":
                    var evaluationAnswer = entity as EvaluationAnswer;
                    if (evaluationAnswer != null)
                    {
                        evaluationAnswer.Question = DataContext.EvaluationQuestion.FirstOrDefault(x => x.Id == evaluationAnswer.Question.Id);
                        _dataContext.Entry(evaluationAnswer.Question).State = EntityState.Unchanged;
                        _dataContext.Entry(evaluationAnswer).State = EntityState.Added;
                        try
                        {
                            _dataContext.SaveChanges();
                            return evaluationAnswer.Id;
                        }
                        catch (Exception ex)
                        {
                            return 0;
                        }


                    }
                    break;
                case "ServiceUsersMapping":
                    var serviceUsersMapping = entity as ServiceUsersMapping;
                    if (serviceUsersMapping != null)
                    {
                        _dataContext.Entry(serviceUsersMapping).State = EntityState.Added;
                        _dataContext.SaveChanges();
                        return Convert.ToInt32(serviceUsersMapping.Id);
                    }
                    break;
            }
            _dataContext.SaveChanges();
            return 0;
        }

        public void Update(T entity)
        {
            var type = typeof(T);
            switch (type.Name)
            {
                case "Indicator":
                    var indicator = entity as Indicator;
                    if (indicator != null)
                    {
                        var item = DataContext.Indicators.FirstOrDefault(f => f.Id == indicator.Id);
                        item.Name = indicator.Name;
                        item.LastModifiedDate = DateTime.Now;
                        item.ObjectType = DataContext.ObjectTypes.FirstOrDefault(f => f.Id == indicator.ObjectType.Id);
                        item.Ratio = DataContext.Ratios.FirstOrDefault(f => f.Id == indicator.Ratio.Id);
                        item.Scores = indicator.Scores;
                        _dataContext.Entry(item.Ratio).State = EntityState.Unchanged;
                        _dataContext.Entry(item.ObjectType).State = EntityState.Unchanged;
                        DataContext.Entry(item).State = EntityState.Modified;
                        //_dbSet.Attach(indicator as T);
                    }
                    break;
                case "Score":
                    var score = entity as Score;
                    if (score != null)
                    {
                        _dataContext.Entry(score.Indicator).State = EntityState.Unchanged;
                        _dataContext.Entry(score).State = EntityState.Modified;
                        _dbSet.Attach(score as T);
                    }
                    break;
                case "ProfessorScore":
                    var professorScore = entity as ProfessorScore;
                    if (professorScore != null)
                    {
                        var item = DataContext.ProfessorScores.FirstOrDefault(f => f.Id == professorScore.Id);
                        item.Term = DataContext.Terms.Where(w => w.Id == professorScore.Term.Id).FirstOrDefault();
                        item.Score = DataContext.Scores.Where(w => w.Id == professorScore.Score.Id).FirstOrDefault();
                        item.Professor = DataContext.Professors.Where(w => w.Id == professorScore.Professor.Id).FirstOrDefault();
                        item.CurrentScore = professorScore.CurrentScore;
                        _dataContext.Entry(item.Term).State = EntityState.Unchanged;
                        _dataContext.Entry(item.Professor).State = EntityState.Unchanged;
                        _dataContext.Entry(item.Score).State = EntityState.Unchanged;
                        _dataContext.Entry(item).State = EntityState.Modified;
                        //_dbSet.Attach(item as T);
                    }
                    break;
                case "Professor":
                    var professor = entity as Professor;
                    if (professor != null)
                    {
                        var item = DataContext.Professors.FirstOrDefault(f => f.Id == professor.Id);
                        item.AcademicDegree = professor.AcademicDegree;
                        item.Address = professor.Address;
                        item.AverageGradeEvaluation = professor.AverageGradeEvaluation;
                        item.Description = professor.Description;
                        item.EducationPlaceUniversityRank = professor.EducationPlaceUniversityRank;
                        item.Email = professor.Email;
                        item.EntryAndExitStatus = professor.EntryAndExitStatus;
                        item.ExamQuestionsProvideTimely = professor.ExamQuestionsProvideTimely;
                        item.Family = professor.Family;
                        item.FatherName = professor.FatherName;
                        item.Gender = professor.Gender;
                        item.GroupMangerComments = professor.GroupMangerComments;
                        item.InPersonSession = professor.InPersonSession;
                        item.IsActive = professor.IsActive;
                        item.LastModifiedDate = DateTime.Now;
                        item.Mobile = professor.Mobile;
                        item.Name = professor.Name;
                        item.NationalCode = professor.NationalCode;
                        item.OnlineSession = professor.OnlineSession;
                        item.OthersSession = professor.OthersSession;
                        item.ProfessorAccessStatus = professor.ProfessorAccessStatus;
                        item.ScientificRank = professor.ScientificRank;
                        item.ScoresAnnounceTimely = professor.ScoresAnnounceTimely;
                        item.Status = professor.Status;
                        item.TeachingExperience = professor.TeachingExperience;
                        item.UniversityStudyPlace = professor.UniversityStudyPlace;
                        item.UniversityWorkPlace = professor.UniversityWorkPlace;

                        // //_dataContext.Entry(professor.Colleges).State = EntityState.Unchanged;
                        // _dataContext.Entry(professor.EducationalClasses).State = EntityState.Unchanged;
                        //// _dataContext.Entry(professor.EducationalGroups).State = EntityState.Unchanged;
                        // _dataContext.Entry(professor.ProfessorScores).State = EntityState.Unchanged;
                        _dataContext.Entry(item).State = EntityState.Modified;
                        //_dbSet.Attach(professor as T);
                    }
                    break;
                case "EducationalGroupScore":
                    var educationalGroupScore = entity as EducationalGroupScore;
                    if (educationalGroupScore != null)
                    {
                        var item = DataContext.EducationalGroupScores.FirstOrDefault(f => f.Id == educationalGroupScore.Id);
                        item.Term = DataContext.Terms.FirstOrDefault(w => w.Id == educationalGroupScore.Term.Id);
                        item.Score = DataContext.Scores.FirstOrDefault(w => w.Id == educationalGroupScore.Score.Id);
                        item.EducationalGroup = DataContext.EducationalGroups.FirstOrDefault(w => w.Id == educationalGroupScore.EducationalGroup.Id);
                        item.CurrentScore = educationalGroupScore.CurrentScore;
                        _dataContext.Entry(item.Term).State = EntityState.Unchanged;
                        _dataContext.Entry(item.Score).State = EntityState.Unchanged;
                        _dataContext.Entry(item.EducationalGroup).State = EntityState.Unchanged;
                        DataContext.Entry(item).State = EntityState.Modified;
                        //_dbSet.Attach(educationalGroupScore as T);
                    }
                    break;
                case "EducationalGroup":
                    var educationalGroup = entity as EducationalGroup;
                    if (educationalGroup != null)
                    {
                        var item = DataContext.EducationalGroups.FirstOrDefault(f => f.Id == educationalGroup.Id);
                        item.College = DataContext.Colleges.FirstOrDefault(f => f.Id == educationalGroup.College.Id);
                        item.EducationalGroupScores = DataContext.EducationalGroupScores.Where(w => w.EducationalGroup.Id == educationalGroup.Id).ToList();
                        item.InTimePresentCurriculum = educationalGroup.InTimePresentCurriculum;
                        _dataContext.Entry(item.College).State = EntityState.Unchanged;
                        //_dataContext.Entry(item.EducationalGroupScores).State = EntityState.Unchanged;
                        DataContext.Entry(item).State = EntityState.Modified;
                        //_dbSet.Attach(educationalGroup as T);
                    }
                    break;
                case "EducationalClass":
                    var educationalClass = entity as EducationalClass;
                    if (educationalClass != null)
                    {
                        _dataContext.Entry(educationalClass.Professor).State = EntityState.Unchanged;
                        // _dataContext.Entry(educationalClass.EducationalGroups).State = EntityState.Unchanged;
                        // _dataContext.Entry(educationalClass.Colleges).State = EntityState.Unchanged;
                        DataContext.Entry(educationalClass).State = EntityState.Modified;
                        //_dbSet.Attach(educationalClass as T);
                    }
                    break;
                case "CollegeScore":
                    var collegeScore = entity as CollegeScore;
                    if (collegeScore != null)
                    {
                        _dataContext.Entry(collegeScore.Term).State = EntityState.Unchanged;
                        _dataContext.Entry(collegeScore.Score).State = EntityState.Unchanged;
                        _dataContext.Entry(collegeScore.College).State = EntityState.Unchanged;
                        DataContext.Entry(collegeScore).State = EntityState.Modified;
                        _dbSet.Attach(collegeScore as T);
                    }
                    break;
                case "UniversityLevelMapping":
                    var universityLevelMapping = entity as UniversityLevelMapping;
                    if (universityLevelMapping != null)
                    {
                        var item = DataContext.UniversityLevelMappings.FirstOrDefault(f => f.Id == universityLevelMapping.Id);
                        item.Score = DataContext.Scores.FirstOrDefault(f => f.Id == universityLevelMapping.Score.Id);
                        item.IsActive = universityLevelMapping.IsActive;
                        item.LastModifiedDate = DateTime.Now;
                        item.UniversityName = universityLevelMapping.UniversityName;
                        _dataContext.Entry(item.Score).State = EntityState.Unchanged;
                        DataContext.Entry(item).State = EntityState.Modified;
                    }
                    break;
                case "User":
                    var user = entity as User;
                    if (user != null)
                    {
                        var item = DataContext.Users.FirstOrDefault(f => f.ID == user.ID);
                        if (user.College != null)
                        {
                            item.College = DataContext.Colleges.FirstOrDefault(f => f.Id == user.College.Id);
                            //_dataContext.Entry(item.College).State = EntityState.Unchanged;
                        }
                        //if (user.EducationalGroup != null)
                        //{
                        //    item.EducationalGroup = DataContext.EducationalGroups.FirstOrDefault(f => f.Id == user.EducationalGroup.Id);
                        //    //_dataContext.Entry(item.EducationalGroup).State = EntityState.Unchanged;
                        //}
                        item.FirstName = user.FirstName;
                        item.LastName = user.LastName;
                        item.Password = user.Password;
                        item.Username = user.Username;
                        _dataContext.Entry(item).State = EntityState.Modified;
                    }
                    break;
                case "Schedule":
                    var schedule = entity as Schedule;
                    if (schedule != null)
                    {
                        var item = DataContext.Schedules.FirstOrDefault(f => f.Id == schedule.Id);
                        item.ActionMethod = schedule.ActionMethod;
                        item.Description = schedule.Description;
                        item.IsActive = schedule.IsActive;
                        item.LastModifiedDate = DateTime.Now;
                        item.Name = schedule.Name;
                        item.TimeLapse = schedule.TimeLapse;
                        item.TimeLapseMeasurement = schedule.TimeLapseMeasurement;
                        item.LastRunDate = schedule.LastRunDate;
                        item.NextRunDate = schedule.NextRunDate;
                        _dataContext.Entry(item).State = EntityState.Modified;
                    }
                    break;
                case "EvaluationQuestion":
                    var evaluationQuestion = entity as EvaluationQuestion;
                    if (evaluationQuestion != null)
                    {
                        var item = DataContext.EvaluationQuestion.FirstOrDefault(x => x.Id == evaluationQuestion.Id);
                        item.Term = DataContext.Terms.FirstOrDefault(w => w.Id == evaluationQuestion.Term.Id);
                        item.EvaluationType = DataContext.EvaluationType.FirstOrDefault(w => w.Id == evaluationQuestion.EvaluationType.Id);
                        item.IsLastQuestion = evaluationQuestion.IsLastQuestion;
                        item.ParentId = evaluationQuestion.ParentId;
                        item.Text = evaluationQuestion.Text;
                        _dataContext.Entry(item).State = EntityState.Modified;

                    }
                    break;
                case "EvaluationAnswer":
                    var evaluationAnswer = entity as EvaluationAnswer;
                    if (evaluationAnswer != null)
                    {
                        evaluationAnswer.Question = DataContext.EvaluationQuestion.FirstOrDefault(x => x.Id == evaluationAnswer.Question.Id);
                        _dataContext.Entry(evaluationAnswer.Question).State = EntityState.Unchanged;
                        _dataContext.Entry(evaluationAnswer).State = EntityState.Added;
                        try
                        {
                            _dataContext.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                        }


                    }
                    break;
            }

            var ss = _dataContext.SaveChanges();

        }

        public void Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                _dataContext.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        public void Delete(Expression<Func<T, bool>> whereCondition)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(whereCondition).AsEnumerable();
            foreach (T obj in objects)
            {
                _dbSet.Remove(obj);
            }
            _dataContext.SaveChanges();
        }
        public int Delete(Expression<Func<T, bool>> whereCondition, bool returnResualt = true)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(whereCondition).AsEnumerable();
            foreach (T obj in objects)
            {
                _dbSet.Remove(obj);
            }
            return _dataContext.SaveChanges();
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }


        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public virtual IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions)
        {

            //return _dbset.AsQueryable<T>();
            return includeExpressions.Aggregate(_dbSet.AsQueryable<T>(), ((current, expression) => current.Include(expression)));
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition).AsEnumerable();
        }

        public bool IsExist(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Any(expression);
        }
    }
}
