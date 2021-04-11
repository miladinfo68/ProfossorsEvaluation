using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class Mapping : BaseClass
    {
        public MappingType MappingType { get; set; }
        public int? TypeId { get; set; }
        public string TypeName { get; set; }

        public int CurrentScore
        {
            get
            {
                return 0;
            }
        }

    }
}