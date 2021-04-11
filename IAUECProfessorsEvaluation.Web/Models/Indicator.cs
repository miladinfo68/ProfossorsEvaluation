using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class Indicator : BaseClass
    {
        //[Required(ErrorMessage ="عنوان شاخص را وارد نمائید.")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "میزان اهمیت شاخص را وارد نمائید.")]
        public virtual Ratio Ratio { get; set; }
        public virtual ObjectType ObjectType { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
        public string CountOfType { get; set; }
    }
}