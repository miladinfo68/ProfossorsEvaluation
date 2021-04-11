using System;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class BaseClass
    {
        public BaseClass()
        {
            IsActive = true;
            CreationDate = new DateTime();
        }
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool? IsActive { get; set; }

    }
}