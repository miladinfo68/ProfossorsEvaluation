namespace IAUECProfessorsEvaluation.Model.SyncModel
{
    public class GroupSyncModel
    {
        public int? EducationalGroupCode { get; set; }
        public string Name { get; set; }
        public int? CollegeId { get; set; }
        public string Term { get; set; }
        public int? GroupMangerId { get; set; }
        public bool IsActive { get; set; }
        public int? OnlinePresenceTime { get; set; }
        public int? PhysicalPresenceTime { get; set; }
        public int? TotalStudentsCount { get; set; }
        public int? BachelorStudentCount { get; set; }
        public int? MaStudentCount { get; set; }
        public int? DoctoralStudentCount { get; set; }
        public int? TotalProfessorsCount { get; set; }
        public int? DoctoralProfessorsCount { get; set; }
        public int? MaProfessorsCount { get; set; }
        public int? BachelorProfessorsCount { get; set; }

        public int? CancellationStudentsCount { get; set; }
        public int? DismissedstudentsCount { get; set; }
        public decimal? TotlalStudentAverageScores { get; set; }
        public decimal? BachelorStudentAverageScores { get; set; }
        public decimal? MaStudentAverageScores { get; set; }
        public decimal? DoctoralStudentAverageScores { get; set; }
        public int? TotalProposals { get; set; }
        public int? ApprovedProposals { get; set; }

        public int? ActiveResearchProfessorCount { get; set; }
        public int? AverageProposalWaitingTime { get; set; }
        public string LessonPlanSendDate { get; set; }
    }
}
