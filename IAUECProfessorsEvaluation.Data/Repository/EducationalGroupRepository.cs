using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using System.Linq.Expressions;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Core.Helper;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class EducationalGroupRepository : RepositoryBase<EducationalGroup>, IEducationalGroupRepository
    {


        private IDbSet<EducationalGroup> _dbSet;
        public EducationalGroupRepository(
            IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<EducationalGroup>();
        }



        public override IEnumerable<EducationalGroup> GetAll()
        {
            return _dbSet
                //.Include(i => i.College)
                .Include(i => i.Term)

                //.Include(i => i.GroupManger)
                //.Include(i => i.EducationalClasses.Select(s => s.Term))
                //.Include(i => i.EducationalClasses.Select(s => s.Professor.ProfessorScores))
                //.Include(i => i.EducationalClasses.Select(s => s.Professor.ProfessorScores.Select(ss => ss.Term)))
                .AsEnumerable();
        }
        public override IEnumerable<EducationalGroup> GetMany(Expression<Func<EducationalGroup, bool>> whereCondition)
        {
            return _dbSet.Where(whereCondition)
                .Include(i => i.Term)
                .Include(i => i.GroupManger)
                .Include(i => i.EducationalClasses.Select(s => s.Term))

                .Include(i => i.EducationalClasses.Select(s => s.Professor.ProfessorScores))
                .Include(i => i.EducationalClasses.Select(s => s.Professor.ProfessorScores.Select(ss => ss.Term)))
                .Include(i => i.EducationalClasses.Select(s => s.Professor.ProfessorScores.Select(ss => ss.EducationalGroup)))
                .AsEnumerable();
        }
        public IEnumerable<EducationalGroup> GetAllWithCollege()
        {
            return DataContext.EducationalGroups
                .Include(c => c.College)
                .AsEnumerable();
        }

        public int AddOrUpdate(GroupSyncModel educationalGroup)
        {
            var termRepo = new TermRepository(DatabaseFactory);
            if (!termRepo.IsExist(y => y.TermCode == educationalGroup.Term))
                return (int)Enums.AddOrUpdateRenurnValue.ترم_موجود_نمی_باشد;

            var collegeRepo = new CollegeRepository(DatabaseFactory);
            if (!collegeRepo.IsExist(y => y.CollegeCode == educationalGroup.CollegeId))
                return (int)Enums.AddOrUpdateRenurnValue.دانشکده_موجود_نمی_باشد;

            if (IsExist(x => x.EducationalGroupCode == educationalGroup.EducationalGroupCode && x.Term.TermCode == educationalGroup.Term))
            {
                //ToDO Update
                var r = Update(educationalGroup);
                if (r != 0) return (int)Enums.AddOrUpdateRenurnValue.آپدیت_گردید;
                return (int)Enums.AddOrUpdateRenurnValue.عملیات_ناموفق;
            }
            else
            {
                //ToDo Add
                var r = Add(educationalGroup);
                if (r != 0) return (int)Enums.AddOrUpdateRenurnValue.اضافه_گردید; ;

                return (int)Enums.AddOrUpdateRenurnValue.عملیات_ناموفق;
            }
        }

        public int Update(GroupSyncModel ge)
        {
            var profRepo = new ProfessorRepository(DatabaseFactory);
            var collegeRepo = new CollegeRepository(DatabaseFactory);
            var g = DataContext.EducationalGroups
                .FirstOrDefault(x => x.EducationalGroupCode == ge.EducationalGroupCode
                && x.Term.TermCode == ge.Term);

            if (g != null)
            {
                g.Name = ge.Name;
                g.IsActive = ge.IsActive;
                g.GroupManger = profRepo.GetMany(x => x.ProfessorCode == ge.GroupMangerId && x.Term.TermCode == ge.Term).FirstOrDefault();
                g.OnlinePresenceTime = ge.OnlinePresenceTime != 0 ? ge.OnlinePresenceTime : null;
               // g.PhysicalPresenceTime = ge.PhysicalPresenceTime != 0 ? ge.PhysicalPresenceTime : null;
                g.TotalStudentsCount = ge.TotalStudentsCount != 0 ? ge.TotalStudentsCount : null;
                g.CancellationStudentsCount = ge.CancellationStudentsCount != 0 ? ge.CancellationStudentsCount : null;
                g.DismissedstudentsCount = ge.DismissedstudentsCount != 0 ? ge.DismissedstudentsCount : null;
                g.TotalStudentScoresAverage = ge.TotlalStudentAverageScores != 0 ? ge.TotlalStudentAverageScores : null;
                g.TotalProfessorsCount = ge.TotalProfessorsCount != 0 ? ge.TotalProfessorsCount : null;
                g.DoctoralProfessorsCount = ge.DoctoralProfessorsCount != 0 ? ge.DoctoralProfessorsCount : null;
                g.MaProfessorsCount = ge.MaProfessorsCount != 0 ? ge.MaProfessorsCount : null;
                g.BachelorProfessorsCount = ge.BachelorProfessorsCount != 0 ? ge.BachelorProfessorsCount : null;
                g.TotalProposals = ge.TotalProposals != 0 ? ge.TotalProposals : null;
                g.ApprovedProposals = ge.ApprovedProposals != 0 ? ge.ApprovedProposals : null;
                g.College = collegeRepo.GetMany(x => x.CollegeCode == ge.CollegeId).FirstOrDefault();
                g.AverageBachelorStudentGrades = ge.BachelorStudentAverageScores != 0 ? ge.BachelorStudentAverageScores : null;
                g.AverageMaStudentGrades = ge.MaStudentAverageScores != 0 ? ge.MaStudentAverageScores : null;
                g.AverageDoctoralStudentGrades = ge.DoctoralStudentAverageScores != 0 ? ge.DoctoralStudentAverageScores : null;
                g.LastModifiedDate = DateTime.Now;
                //Calculatepr
                g.ExpelledStudentsPercentage = ge.TotalStudentsCount != 0
                    ? (ge.DismissedstudentsCount * 100) / ge.TotalStudentsCount
                    : null;
                g.StudentCancellationPercentage =  ge.TotalStudentsCount != 0
                    ? (ge.CancellationStudentsCount * 100) / ge.TotalStudentsCount
                    : null;
                g.TeacherToBachelorStudentRatio =  ge.BachelorProfessorsCount != 0
                    ? (ge.BachelorStudentCount / ge.BachelorProfessorsCount)
                    : null;
                g.TeacherToMaStudentRatio =  ge.MaProfessorsCount != 0
                    ? (ge.MaStudentCount / ge.MaProfessorsCount)
                    : null;
                g.TeacherToDoctoralStudentRatio = ge.DoctoralProfessorsCount != 0
                    ? (ge.DoctoralStudentCount / ge.DoctoralProfessorsCount)
                    : null;
                g.ApproveProposalsPercentage =  ge.TotalProposals != 0
                    ? (ge.ApprovedProposals * 100) / ge.TotalProposals
                    : null;
            }


            return DataContext.SaveChanges();
        }
        public int Add(GroupSyncModel ge)
        {
            var profRepo = new ProfessorRepository(DatabaseFactory);
            var termRepo = new TermRepository(DatabaseFactory);
            var collegeRepo = new CollegeRepository(DatabaseFactory);

            var c = new EducationalGroup
            {

                Name = ge.Name,
                IsActive = ge.IsActive,
                GroupManger = profRepo
                .GetMany(x => x.ProfessorCode == ge.GroupMangerId && x.Term.TermCode == ge.Term).FirstOrDefault(),
                OnlinePresenceTime = ge.OnlinePresenceTime != 0 ? ge.OnlinePresenceTime : null,
                PhysicalPresenceTime = ge.PhysicalPresenceTime != 0 ? ge.PhysicalPresenceTime : null,
                TotalStudentsCount = ge.TotalStudentsCount != 0 ? ge.TotalStudentsCount : null,
                CancellationStudentsCount = ge.CancellationStudentsCount != 0 ? ge.CancellationStudentsCount : null,
                DismissedstudentsCount = ge.DismissedstudentsCount != 0 ? ge.DismissedstudentsCount : null,
                TotalStudentScoresAverage = ge.TotlalStudentAverageScores != 0 ? ge.TotlalStudentAverageScores : null,
                TotalProfessorsCount = ge.TotalProfessorsCount != 0 ? ge.TotalProfessorsCount : null,
                DoctoralProfessorsCount = ge.DoctoralProfessorsCount != 0 ? ge.DoctoralProfessorsCount : null,
                MaProfessorsCount = ge.MaProfessorsCount != 0 ? ge.MaProfessorsCount : null,
                BachelorProfessorsCount = ge.BachelorProfessorsCount != 0 ? ge.BachelorProfessorsCount : null,
                TotalProposals = ge.TotalProposals != 0 ? ge.TotalProposals : null,
                ApprovedProposals = ge.ApprovedProposals != 0 ? ge.ApprovedProposals : null,
                College = collegeRepo.GetMany(x => x.CollegeCode == ge.CollegeId).FirstOrDefault(),
                AverageBachelorStudentGrades = ge.BachelorStudentAverageScores != 0 ? ge.BachelorStudentAverageScores : null,
                AverageMaStudentGrades = ge.MaStudentAverageScores != 0 ? ge.MaStudentAverageScores : null,
                AverageDoctoralStudentGrades = ge.DoctoralStudentAverageScores != 0 ? ge.DoctoralStudentAverageScores : null,
                Term = termRepo.GetMany(x => x.TermCode == ge.Term).FirstOrDefault(),
                EducationalGroupCode = ge.EducationalGroupCode != 0 ? ge.EducationalGroupCode : null,
                CreationDate = DateTime.Now,
                //Calculatepr
                ExpelledStudentsPercentage =  ge.TotalStudentsCount != 0 
                ? (ge.DismissedstudentsCount * 100) / ge.TotalStudentsCount : null,
                StudentCancellationPercentage =  ge.TotalStudentsCount != 0 
                ? (ge.CancellationStudentsCount * 100) / ge.TotalStudentsCount : null,
                TeacherToBachelorStudentRatio =  ge.BachelorProfessorsCount != 0 
                ? (ge.BachelorStudentCount / ge.BachelorProfessorsCount) : null,
                TeacherToMaStudentRatio =  ge.MaProfessorsCount != 0 
                ? (ge.MaStudentCount / ge.MaProfessorsCount) : null,
                TeacherToDoctoralStudentRatio =  ge.DoctoralProfessorsCount != 0 
                ? (ge.DoctoralStudentCount / ge.DoctoralProfessorsCount) : null,

                ApproveProposalsPercentage =  ge.TotalProposals != 0 
                ? (ge.ApprovedProposals * 100) / ge.TotalProposals : null,

            };
            DataContext.EducationalGroups.Add(c);
            return DataContext.SaveChanges();
        }

        public int Remove(EducationalGroup educationalGroup)
        {
            DataContext.EducationalGroups.Remove(educationalGroup);
            return DataContext.SaveChanges();
        }
    }
}
