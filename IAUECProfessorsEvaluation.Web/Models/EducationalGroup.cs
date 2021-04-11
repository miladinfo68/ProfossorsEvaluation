using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Models
{
    public class EducationalGroup : BaseClass
    {
        public int? EducationalGroupCode { get; set; }
        public string Name { get; set; }

        public virtual College College { get; set; }

        public virtual ICollection<EducationalGroupScore> EducationalGroupScores { get; set; }
        public Professor GroupManger { get; set; }
        public int? OnlinePresenceTime { get; set; }
        public int? PhysicalPresenceTime { get; set; }
        public int? AverageBachelorStudentGrades { get; set; }
        public int? AverageMaStudentGrades { get; set; }
        public int? AverageDoctoralStudentGrades { get; set; }
        //درصد اخراجی
        public int? ExpelledStudentsPercentage { get; set; }
        //درصد انصرافی
        public int? StudentCancellationPercentage { get; set; }

        public int? TeacherToBachelorStudentRatio { get; set; }
        public int? TeacherToMaStudentRatio { get; set; }
        public int? TeacherToDoctoralStudentRatio { get; set; }
        public int? ApproveProposalsPercentage { get; set; }
        //ارائه به موقعه برنام درسی
        public string InTimePresentCurriculum { get; set; }
        public Term Term { get; set; }

        public virtual ICollection<EducationalClass> EducationalClasses { get; set; }
        public int? StudentsCount { get; set; }
        public int? CancellationStudentsCount { get; set; }
        public int? DismissedstudentsCount { get; set; }
        public int? StudentScoresAverage { get; set; }
        public int? ProfessorsCount { get; set; }
        public int? TotalProposals { get; set; }
        public int? ApprovedProposals { get; set; }

        public virtual int TotalScores
        {
            get
            {
                var total = 0;
                total += this.EducationalGroupScores.Sum(s => s.CurrentScore);
                total += (int)this.EducationalClasses.Select(s => s.Professor).SelectMany(s => s.ProfessorScores).Sum(s => s.CurrentScore);
                return total;
            }
        }
        public virtual int RankInUniversity { get; set; }
    }
}