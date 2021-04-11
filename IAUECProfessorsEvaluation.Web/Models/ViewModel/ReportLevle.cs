using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Web.Models.ViewModel
{
    public class ReportLevle
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<ReportLevle> GetReportLeveles(int? collegeId, int? groupCode)
        {
            var resualt = new List<ReportLevle>();
            if ((collegeId == null || collegeId == 0) && (groupCode == null || groupCode == 0))
            {
                resualt.AddRange(new List<ReportLevle>
                {
                    new ReportLevle{Id = 1,Name = "دانشکده"},
                    new ReportLevle{Id = 2,Name = "گروه"},
                    new ReportLevle{Id = 3,Name = "استاد"}
                });
            }
            else
            {

                if (groupCode == null || groupCode == 0)
                {
                    resualt.AddRange(new List<ReportLevle>
                    {
                        new ReportLevle{Id = 2,Name = "گروه"},
                        new ReportLevle{Id = 3,Name = "استاد"}
                    });
                }
                else
                {
                    resualt.AddRange(new List<ReportLevle>
                    {
                        new ReportLevle{Id = 3,Name = "استاد"}
                    });

                }
            }
            return resualt;
        }

    }
}