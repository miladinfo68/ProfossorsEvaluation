using System.Collections.Generic;

namespace IAUECProfessorsEvaluation.Model.Models
{
    public class EducationalGroup : BaseClass
    {

        public int? EducationalGroupCode { get; set; } //1
        public string Name { get; set; } //1

        public virtual College College { get; set; } //1

        public virtual ICollection<EducationalGroupScore> EducationalGroupScores { get; set; }
        public Professor GroupManger { get; set; } //1
        public int? OnlinePresenceTime { get; set; } //1
        public int? PhysicalPresenceTime { get; set; } //1
        public int? OtherPresenceTime { get; set; }

        public int? OnlineHolidays { get; set; }
        public int? OfflineHolidays { get; set; }
        public int? EducationalAndResearchCouncil { get; set; }

        public decimal? AverageBachelorStudentGrades { get; set; } //1-
        public decimal? AverageMaStudentGrades { get; set; }//1-
        public decimal? AverageDoctoralStudentGrades { get; set; }//1-
        //درصد اخراجی
        public int? ExpelledStudentsPercentage { get; set; }//2
        //درصد انصرافی
        public int? StudentCancellationPercentage { get; set; }//2

        public int? TeacherToBachelorStudentRatio { get; set; }//2
        public int? TeacherToMaStudentRatio { get; set; }//2
        public int? TeacherToDoctoralStudentRatio { get; set; }//2
        public int? ApproveProposalsPercentage { get; set; }//2
        //ارائه به موقعه برنام درسی
        public string InTimePresentCurriculum { get; set; }//3
        public Term Term { get; set; }//1

        public virtual ICollection<EducationalClass> EducationalClasses { get; set; }
        public int? TotalStudentsCount { get; set; }//1
        public int? CancellationStudentsCount { get; set; }//1
        public int? DismissedstudentsCount { get; set; }//1
        public decimal? TotalStudentScoresAverage { get; set; }
        public int? TotalProfessorsCount { get; set; }//1
        public int? DoctoralProfessorsCount { get; set; }//1
        public int? MaProfessorsCount { get; set; }//1
        public int? BachelorProfessorsCount { get; set; }//1
        public int? TotalProposals { get; set; }//1
        public int? ApprovedProposals { get; set; }//1
    }
}