using AutoMapper;
using IAUECProfessorsEvaluation.Service.IService;
using IAUECProfessorsEvaluation.Service.Service;
using IAUECProfessorsEvaluation.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Helpers
{
    public class ScheduleHelper
    {
        public static void TestAction()
        {
            var log = new List<string>();
            log.Add("Test Action at: " + DateTime.Now);
           // System.IO.File.AppendAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "log.txt", log);
        }
    }
}