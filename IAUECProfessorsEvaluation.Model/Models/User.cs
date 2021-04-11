using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAUECProfessorsEvaluation.Model.Models;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsPowerUser { get; set; }
        public College College { get; set; }
        public int? EducationalGroupCode { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
