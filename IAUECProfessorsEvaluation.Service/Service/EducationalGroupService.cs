using System.Collections.Generic;
using System.Linq;
using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Data.Repository;
using IAUECProfessorsEvaluation.Model.Models;
using IAUECProfessorsEvaluation.Model.SyncModel;
using IAUECProfessorsEvaluation.Service.Infrastructure;
using IAUECProfessorsEvaluation.Service.IService;

namespace IAUECProfessorsEvaluation.Service.Service
{
    public class EducationalGroupService : BaseService<EducationalGroup>, IEducationalGroupService
    {
        public EducationalGroupService(IRepository
            <EducationalGroup> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
        public IEnumerable<EducationalGroup> GetAllWithCollege()
        {
            var repo = new EducationalGroupRepository(new DatabaseFactory());
            return repo.GetAllWithCollege();

        }

        public int AddOrUpdate(EducationalGroup educationalGroup)
        {
            throw new System.NotImplementedException();
        }

        public int AddOrUpdate(GroupSyncModel educationalGroup)
        {
            var rep = new EducationalGroupRepository(new DatabaseFactory());
            return rep.AddOrUpdate(educationalGroup);
        }

        public int RemoveGroupScore(int? educationalGroupCode, string term)
        {
            var grpRepo = new EducationalGroupRepository(new DatabaseFactory());
            var scorRepo = new EducationalGroupScoreRepository(new DatabaseFactory());
            var grp = grpRepo.GetMany(x => x.EducationalGroupCode == educationalGroupCode && x.Term.TermCode == term)
                .FirstOrDefault();
            return scorRepo.Delete(x => x.EducationalGroup.Id == grp.Id 
            && !x.Score.Indicator.CountOfType.Contains("g2")
            && !x.Score.Indicator.CountOfType.Contains("g9")
            && !x.Score.Indicator.CountOfType.Contains("g12")
            && !x.Score.Indicator.CountOfType.Contains("g16")
            && !x.Score.Indicator.CountOfType.Contains("g17")
            && !x.Score.Indicator.CountOfType.Contains("g18")
            , false);
        }

        public int Remove(EducationalGroup educationalGroup)
        {
            var rep = new EducationalGroupRepository(new DatabaseFactory());
            return rep.Remove(educationalGroup);
        }
    }
}