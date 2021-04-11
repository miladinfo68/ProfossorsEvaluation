using IAUECProfessorsEvaluation.Model.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IAUECProfessorsEvaluation.Data.Configuration
{
    public class ServiceUsersMappingConfiguration : EntityTypeConfiguration<ServiceUsersMapping>
    {
        public ServiceUsersMappingConfiguration()
        {
            base.HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
