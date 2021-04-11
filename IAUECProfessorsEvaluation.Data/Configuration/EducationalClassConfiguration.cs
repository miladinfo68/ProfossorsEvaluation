using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Configuration
{
    public class EducationalClassConfiguration : EntityTypeConfiguration<EducationalClass>
    {
        public EducationalClassConfiguration()
        {
            base.HasKey(p => new { p.Id });
           // base.HasIndex(p => new { p.CodeClass, p.Name });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).IsRequired().HasMaxLength(300);
            Property(p => p.CodeClass).IsRequired();
        }
    }
}