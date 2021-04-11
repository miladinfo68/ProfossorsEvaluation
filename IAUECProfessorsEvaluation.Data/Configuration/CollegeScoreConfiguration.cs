using System.ComponentModel.DataAnnotations.Schema;
using IAUECProfessorsEvaluation.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace IAUECProfessorsEvaluation.Data.Configuration
{
    public class CollegeScoreConfiguration : EntityTypeConfiguration<CollegeScore>
    {
        public CollegeScoreConfiguration()
        {
            base.HasKey(p => p.Id);
            

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        

        }
    }
}
