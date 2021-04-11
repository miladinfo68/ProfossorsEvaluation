using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "نام کاربری را وارد نمائید.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "کلمه عبور را وارد نمائید.")]
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsPowerUser { get; set; }
        public College College { get; set; }
        public int? EducationalGroupCode { get; set; }

        public string EducationalGroupName { get; set; }
        public string PersonalCode { get; set; }
        //public ICollection<Role> Roles { get; set; }

        public string ServiceUsername { get; set; }
    }
}
