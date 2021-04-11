using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class Mapping:BaseClass
    {
        public MappingType MappingType { get; set; }
        public int? TypeId { get; set; }
        public string TypeName { get; set; }

    }
}
