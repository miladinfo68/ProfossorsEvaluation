using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Model.SyncModel
{
    public class ProfessorSyncModel
    {
        public int? ProfessoreCode { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        public string Family { get; set; }
        public bool Gender { get; set; }
        public string NationalCode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int? ScientificRank { get; set; }
        public int? TeachingExperience { get; set; }
        public int? AcademicDegree { get; set; }
        public int? UniversityStudyPlace { get; set; }
        public int? UniversityWorkPlace { get; set; }
        public decimal? UniversityWorkPlaceId { get; set; }
        public string Term { get; set; }
        public bool IsActive { get; set; }
        public bool? HasContractSigned { get; set; }
        public DateTime? ContractSignDate { get; set; }
        public int? DeputyAssessment { get; set; }
        public int? GroupManagerAssessment { get; set; }


    }
}
