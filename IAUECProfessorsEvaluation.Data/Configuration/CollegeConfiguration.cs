using System.ComponentModel.DataAnnotations.Schema;
using IAUECProfessorsEvaluation.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace IAUECProfessorsEvaluation.Data.Configuration
{
    public class CollegeConfiguration : EntityTypeConfiguration<College>
    {
        public CollegeConfiguration()
        {
            base.HasKey(p => p.Id);
            //base.HasIndex(p => new { p.Name, p.CollegeCode });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).HasMaxLength(300).IsRequired();
            Property(p => p.CollegeCode).IsRequired();


        }
    }
}
