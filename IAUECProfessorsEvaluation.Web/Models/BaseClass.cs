using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class BaseClass
    {
        public BaseClass()
        {
            IsActive = false;
        }
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}