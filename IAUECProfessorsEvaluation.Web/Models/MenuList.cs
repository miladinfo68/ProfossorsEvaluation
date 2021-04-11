using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class MenuList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public virtual ICollection<Access> Accesses { get; set; }
        public MenuSection MenuSection { get; set; }
    }
}