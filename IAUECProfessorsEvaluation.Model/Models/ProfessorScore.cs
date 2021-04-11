namespace IAUECProfessorsEvaluation.Model.Models
{
    public class ProfessorScore
    {
        public long Id { get; set; }
        public int? CurrentScore { get; set; }
        public virtual Term Term { get; set; }
        public virtual Score Score { get; set; }
        public virtual Professor Professor { get; set; }
        public EducationalGroup EducationalGroup { get; set; }
    }
}