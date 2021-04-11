namespace IAUECProfessorsEvaluation.Model.Models
{
    public class Person : BaseClass
    {
        public string Name { get; set; }

        public string Family { get; set; }
        public bool? Gender { get; set; }
        public string NationalCode { get; set; }
        public string FatherName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}