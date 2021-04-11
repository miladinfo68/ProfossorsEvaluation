﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class Log
    {
        public decimal Id { get; set; }
        public User User { get; set; }
        public LogType LogType { get; set; }
        public string Desacription { get; set; }
        public DateTime Date { get; set; }
    }
}