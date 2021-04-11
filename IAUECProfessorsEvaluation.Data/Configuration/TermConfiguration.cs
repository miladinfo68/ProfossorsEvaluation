using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Configuration
{
    public class TermConfiguration : EntityTypeConfiguration<Term>
    {
        public TermConfiguration()
        {
            base.HasKey(p => p.Id);
            //base.HasIndex(p =>  p.Name );

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).HasMaxLength(200);
            Property(p => p.TermCode).IsRequired();
        }
    }
}