namespace IAUECProfessorsEvaluation.Model.Models
{
    public class Access
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public MenuList MenuList { get; set; }
    }
}