using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Configuration
{
    public class ProfessorConfiguration : EntityTypeConfiguration<Professor>
    {
        public ProfessorConfiguration()
        {
            base.HasKey(p => p.Id);
            //base.HasIndex(p => p.ProfessorCode);

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).HasMaxLength(100);
            Property(p => p.Family).HasMaxLength(100);
            Property(p => p.FatherName).HasMaxLength(100);
            Property(p => p.NationalCode).HasMaxLength(20);
            Property(p => p.Mobile).HasMaxLength(20);
            Property(p => p.Email).HasMaxLength(40);
            Property(p => p.Address).HasMaxLength(500);

            Property(p => p.ProfessorCode).IsRequired();

        }
    }
}