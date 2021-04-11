using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Web.Models.ViewModel
{
    public class IndicatorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ObjectTypeId { get; set; }
        public List<ScoreViewModel> Scores { get; set; }
        public List<int?> SelectedScore { get; set; }
        public bool? IsParticipation { get; set; }
        public string CountOfType { get; set; }
    }
}