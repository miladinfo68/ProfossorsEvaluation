using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Configuration
{
    public class LogConfiguration : EntityTypeConfiguration<Log>
    {
        public LogConfiguration()
        {
            base.HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Desacription).IsMaxLength();
            Property(p => p.Date).IsRequired();
        }
    }
}