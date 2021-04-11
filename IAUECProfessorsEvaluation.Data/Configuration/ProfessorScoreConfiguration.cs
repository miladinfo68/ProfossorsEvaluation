using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Configuration
{
    public class ProfessorScoreConfiguration : EntityTypeConfiguration<ProfessorScore>
    {
        public ProfessorScoreConfiguration()
        {
            base.HasKey(p => p.Id);

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}