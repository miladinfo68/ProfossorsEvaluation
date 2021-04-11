using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Core.Helper
{
    public static class StaticValue
    {
        public static string SyncApiDomain => "http://192.168.2.101:1369";
        //public static string SyncApiDomain => "http://localhost:3129";
        public static string SyncToken => "/token";
        public static string CollegeRelativeAddress => "/api/college";
        public static string GroupRelativeAddress => "/api/EducationalGroup";
        public static string MappingRelativeAddress => "/api/mapping";
        public static string EducationalClassRelativeAddress => "/api/educationalClass";
        public static string StudentEducationalClass => "/api/studenteducationalclass";
        public static string ProfessorRelativeAddress => "/api/professor";
        public static string TermRelativeAddress => "/api/term";
        public static string CurrentTerm => "/api/Term/CurrentTerm";
        public static string GetStudentCurrentUnits => "/api/StudentEducationalClass/GetStudentCurrentUnits";
        public static string SyncUserName => "iauec@evaluationservice";
        public static string SyncPassword => "$client@1029%";

        public static List<int> IneligibleProfessorCodes => new List<int> { 99999 };
        public static List<int> IneligibleEducationalGroupCodes => new List<int> { 1, 3, 4, 88, 301, 900, 901, 902, 903 };
    }
}
