namespace IAUECProfessorsEvaluation.Model.Models
{
    public class Score : BaseClass
    {
        public string Name { get; set; }

        public decimal? MaxValue { get; set; }
        public decimal? MinValue { get; set; }
        //public float? UpperBound { get; set; }
        //public float? LowerBound { get; set; }
        public float Point { get; set; }

        public virtual Indicator Indicator { get; set; }

    }
}