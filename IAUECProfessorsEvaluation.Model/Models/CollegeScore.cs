namespace IAUECProfessorsEvaluation.Model.Models
{
    public class CollegeScore
    {
        public long Id { get; set; }
        public int CurrentScore { get; set; }


        public virtual Term Term { get; set; }
        public virtual Score Score { get; set; }


        public virtual College College { get; set; }


    }
}