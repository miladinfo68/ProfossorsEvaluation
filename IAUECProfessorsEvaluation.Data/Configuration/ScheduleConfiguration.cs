using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Data.Configuration
{
    public class ScheduleConfiguration : EntityTypeConfiguration<Schedule>
    {
        public ScheduleConfiguration()
        {
            base.HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).HasMaxLength(200);
            Property(p => p.ActionMethod).IsRequired();
            Property(p => p.Name).IsRequired();
            Property(p => p.TimeLapse).IsRequired();
            Property(p => p.TimeLapseMeasurement).IsRequired();
        }
    }
}
