using IAUECProfessorsEvaluation.Model.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using IAUECProfessorsEvaluation.Data.Configuration;

namespace IAUECProfessorsEvaluation.Data.ContextEntities
{
    public class ProfessorsEvaluationEntities : DbContext
    {
        //public ProfessorsEvaluationEntities()
        //    : base("name=ProfessorsEvaluationEntities") { }
        public ProfessorsEvaluationEntities()
        {
            //Configuration.ProxyCreationEnabled = false;
            //Configuration.LazyLoadingEnabled = false;

        }


        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Access> Accesses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleAccess> RoleAccesses { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<CollegeScore> CollegeScores { get; set; }
        public DbSet<EducationalClass> EducationalClasses { get; set; }
        public DbSet<EducationalGroup> EducationalGroups { get; set; }
        public DbSet<EducationalGroupScore> EducationalGroupScores { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<ObjectType> ObjectTypes { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<ProfessorScore> ProfessorScores { get; set; }
        public DbSet<Ratio> Ratios { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Mapping> Mappings { get; set; }
        public DbSet<MappingType> MappingTypes { get; set; }
        // public DbSet<UniversityLevel> UniversityLevels { get; set; }
        public DbSet<UniversityLevelMapping> UniversityLevelMappings { get; set; }
        public DbSet<StudentEducationalClass> StudentEducationalClasses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<LogType> LogTypes { get; set; }
        public DbSet<ServiceUsersMapping> ServiceUsersMapping { get; set; }

        #region Evaluation
        public DbSet<EvaluationQuestion> EvaluationQuestion { get; set; }
        public DbSet<EvaluationAnswer> EvaluationAnswer { get; set; }
        public DbSet<EvaluationComment> EvaluationComment { get; set; }
        public DbSet<EvaluationQuestionAnswer> EvaluationQuestionAnswer { get; set; }
        public DbSet<EvaluationType> EvaluationType { get; set; }
        public DbSet<EvaluationChartType> EvaluationChartType { get; set; }
        public DbSet<EvaluationQuestionAnswerComment> EvaluationQuestionAnswerComment { get; set; }
        #endregion

        public virtual void Commit()
        {
            var fff = base.SaveChanges();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Conventions.Remove<IncludeMetadataConvention>();


            modelBuilder.Configurations.Add(new CollegeConfiguration());
            modelBuilder.Configurations.Add(new CollegeScoreConfiguration());
            modelBuilder.Configurations.Add(new EducationalClassConfiguration());
            modelBuilder.Configurations.Add(new EducationalGroupConfiguration());
            modelBuilder.Configurations.Add(new EducationalGroupScoreConfiguration());
            modelBuilder.Configurations.Add(new IndicatorConfiguration());
            modelBuilder.Configurations.Add(new ObjectTypeConfiguration());
            modelBuilder.Configurations.Add(new ProfessorConfiguration());
            modelBuilder.Configurations.Add(new ProfessorScoreConfiguration());
            modelBuilder.Configurations.Add(new RatioConfiguration());
            modelBuilder.Configurations.Add(new ScoreConfiguration());
            modelBuilder.Configurations.Add(new TermConfiguration());
            modelBuilder.Configurations.Add(new ScheduleConfiguration());
            modelBuilder.Configurations.Add(new LogTypeConfiguration());
            modelBuilder.Configurations.Add(new LogConfiguration());
            modelBuilder.Configurations.Add(new ServiceUsersMappingConfiguration());

            #region Evaluation
            modelBuilder.Entity<EvaluationQuestion>().ToTable("Question", "Evaluation");
            modelBuilder.Entity<EvaluationAnswer>().ToTable("Answer", "Evaluation");
            modelBuilder.Entity<EvaluationComment>().ToTable("Comment", "Evaluation");
            modelBuilder.Entity<EvaluationQuestionAnswer>().ToTable("QuestionAnswer", "Evaluation");
            modelBuilder.Entity<EvaluationType>().ToTable("Type", "Evaluation");
            modelBuilder.Entity<EvaluationChartType>().ToTable("ChartType", "Evaluation");
            modelBuilder.Entity<EvaluationQuestionAnswerComment>().ToTable("QuestionAnswerComment", "Evaluation");
            #endregion
        }
    }
}
