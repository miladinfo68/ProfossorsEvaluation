using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class MenuSection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MenuList> MenuLists { get; set; }
    }
}