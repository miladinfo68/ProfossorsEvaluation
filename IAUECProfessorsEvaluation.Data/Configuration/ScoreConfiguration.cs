using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Configuration
{
    public class ScoreConfiguration : EntityTypeConfiguration<Score>
    {
        public ScoreConfiguration()
        {
            base.HasKey(p => p.Id);
            

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).HasMaxLength(300);
        }
    }
}