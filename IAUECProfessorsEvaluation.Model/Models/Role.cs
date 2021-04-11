
using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public int UserCount { get; set; }
        public ICollection<User> Users { get; set; }
    }
}