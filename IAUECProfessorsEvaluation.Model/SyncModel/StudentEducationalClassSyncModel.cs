using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Model.SyncModel
{
    public class StudentEducationalClassSyncModel
    {
        public int? EducationalClassId { get; set; }
        public decimal? Grade { get; set; }
        public decimal? ProfessorEvaluationScore { get; set; }
        public string Term { get; set; }
        public int? StudentId { get; set; }
        public bool IsActive { get; set; }
    }
}
