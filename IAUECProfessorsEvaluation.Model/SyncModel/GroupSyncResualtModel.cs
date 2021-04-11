using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Model.SyncModel
{
    public class GroupSyncResualtModel
    {
        public Dictionary<string, string> AddOrUpdateResualt { get; set; }
        public Dictionary<string, string> RemoveResualt { get; set; }
        public Dictionary<string, List<KeyValuePair<string, string>>> AddScoreResualt { get; set; }


    }
}
