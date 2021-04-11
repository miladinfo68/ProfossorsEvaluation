using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Configuration
{
    public class LogTypeConfiguration : EntityTypeConfiguration<LogType>
    {
        public LogTypeConfiguration()
        {
            base.HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).HasMaxLength(400);
            Property(x => x.Name).IsRequired();
            Property(x => x.LogTypeID).IsRequired();
        }
    }
}