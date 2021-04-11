namespace IAUECProfessorsEvaluation.Web.Models
{
    public class RoleAccess
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public Access Access { get; set; }
    }
}