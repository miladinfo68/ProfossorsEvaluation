using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class ScheduleModel : BaseClass
    {
        public string Name { get; set; }
        public decimal TimeLapse { get; set; }
        public string TimeLapseMeasurement { get; set; }
        public string ActionMethod { get; set; }
        public string Description { get; set; }
        public DateTime LastRunDate { get; set; }
        public DateTime NextRunDate { get; set; }
    }
}