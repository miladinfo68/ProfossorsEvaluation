using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class Professor : Person
    {
        public int? ProfessorCode { get; set; }
        public int? Status { get; set; }


        public virtual ICollection<EducationalClass> EducationalClasses { get; set; }
        public virtual ICollection<ProfessorScore> ProfessorScores { get; set; }

        public int? ScientificRank { get; set; }
        public int? TeachingExperience { get; set; }
        public int? AcademicDegree { get; set; }
        public int? EducationPlaceUniversityRank { get; set; }
        public int? UniversityStudyPlace { get; set; }
        public int? UniversityWorkPlace { get; set; }
        public int? InPersonSession { get; set; }
        public int?  OnlineSession { get; set; }
        public int? OthersSession { get; set; }
        public string Description { get; set; }
        public int? EntryAndExitStatus { get; set; }
        public int? ScoresAnnounceTimely { get; set; }
        public int? ExamQuestionsProvideTimely { get; set; }
        public int? ProfessorAccessStatus { get; set; }
        public int? GroupMangerComments { get; set; }
        public int? DeputyComments { get; set; }
        public int? AverageGradeEvaluation { get; set; }
        public Term Term { get; set; }

        public Professor ShallowCopy()
        {
            //return (Professor)this.MemberwiseClone();

            var sourceProperties = typeof(Professor)
                                .GetProperties();
                                //.Where(p => p.CanRead && p.CanWrite &&
                                //            p.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.NotMappedAttribute), true).Length == 0);
            var newObj = new Professor();
            foreach (var property in sourceProperties)
            {
                property.SetValue(newObj, property.GetValue(this, null), null);
            }
            return newObj;
        }
        //}
    }
}
