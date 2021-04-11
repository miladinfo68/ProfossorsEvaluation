using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Web.Models.ViewModel
{
    public class ConditionsViewModel
    {
        public ConditionsViewModel()
        {
            GroupCondition=new List<Condition>();
            ProfessoreCondition=new List<Condition>();
        }
        public List<Condition> GroupCondition { get; set; }
        public List<Condition> ProfessoreCondition { get; set; }
    }
}