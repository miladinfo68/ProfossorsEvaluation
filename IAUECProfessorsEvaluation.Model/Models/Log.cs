using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class Log
    {
        public Log()
        {
            Date = new DateTime();
        }

        public decimal Id { get; set; }
        public User User { get; set; }
        public LogType LogType { get; set; }
        public string Desacription { get; set; }
        public DateTime Date { get; set; }
    }

}
