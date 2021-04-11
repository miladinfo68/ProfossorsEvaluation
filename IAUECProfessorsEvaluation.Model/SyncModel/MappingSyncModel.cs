using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Model.SyncModel
{
    public class MappingSyncModel
    {
        public int MappingTypeId { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
