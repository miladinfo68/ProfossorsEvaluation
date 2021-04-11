namespace IAUECProfessorsEvaluation.Model.Models
{
    public class UniversityLevelMapping : BaseClass
    {
        public Score Score { get; set; }
        public int? UniversityId { get; set; }
        public string UniversityName { get; set; }
    }
}