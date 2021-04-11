using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Configuration
{
    public class EducationalGroupConfiguration : EntityTypeConfiguration<EducationalGroup>
    {
        public EducationalGroupConfiguration()
        {
            base.HasKey(p => p.Id);
            //base.HasIndex(p => new { p.EducationalGroupCode, p.Name });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).IsRequired().HasMaxLength(300);
            Property(p => p.EducationalGroupCode).IsRequired();
        }
    }
}