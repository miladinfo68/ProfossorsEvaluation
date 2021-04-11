using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Web.Models.ViewModel
{
    public class SortingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<SortingViewModel> GetSortingList()
        {
            return new List<SortingViewModel>
            {
                new SortingViewModel{Id = 1,Name = "شرکت در گزارش"},
                new SortingViewModel{Id = 2,Name = "عدم شرکت در گزارش"}
            };
        }
    }
}