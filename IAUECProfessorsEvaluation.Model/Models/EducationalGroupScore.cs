namespace IAUECProfessorsEvaluation.Model.Models
{
    public class EducationalGroupScore
    {
        public long Id { get; set; }
        public int? CurrentScore { get; set; }


        public virtual Term Term { get; set; }
        public virtual Score Score { get; set; }


        public virtual EducationalGroup EducationalGroup { get; set; }


    }
}