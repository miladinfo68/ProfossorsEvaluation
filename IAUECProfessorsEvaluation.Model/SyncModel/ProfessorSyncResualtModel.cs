using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Model.SyncModel
{
    public class ProfessorSyncResualtModel
    {
        public Dictionary<string, string> AddOrUpdateResualt { get; set; }
        public Dictionary<string, string> RemoveResualt { get; set; }
        public Dictionary<string, List<KeyValuePair<string, string>>> AddScoreResualt { get; set; }


    }
}