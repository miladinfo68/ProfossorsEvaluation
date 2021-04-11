using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Configuration
{
    public class RatioConfiguration : EntityTypeConfiguration<Ratio>
    {
        public RatioConfiguration()
        {
            base.HasKey(p => p.Id);
            //base.HasIndex(p => p.Name);

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).IsRequired().HasMaxLength(200);
        }
    }
}