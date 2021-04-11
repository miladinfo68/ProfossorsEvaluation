using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using System;
using System.Linq;
using IAUECProfessorsEvaluation.Model.SyncModel;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class MappingRepository : RepositoryBase<Mapping>, IMappingRepository
    {
        public MappingRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
        public int AddOrUpdate(MappingSyncModel mapping)
        {
            var typeRepo = new MappingTypeRepository(DatabaseFactory);


            if (IsExist(x => x.TypeId == mapping.TypeId && x.MappingType.Id == mapping.MappingTypeId))
            {
                //ToDO Update
                var r = Update(mapping);
                if (r != 0) return 2;
                return 3;
            }
            else
            {
                //ToDo Add
                var r = Add(mapping);
                if (r != 0) return 1;
                return 3;
            };
        }

        public int Update(MappingSyncModel mapping)
        {
            var mappingRepo = new MappingRepository(DatabaseFactory);
            var item = DataContext.Mappings.FirstOrDefault(f => f.TypeId == mapping.TypeId && f.MappingType.Id == mapping.MappingTypeId);
            if (item != null)
            {
                item.LastModifiedDate = DateTime.Now;
                item.TypeName = mapping.TypeName;
                item.IsActive = mapping.IsActive;
            }
            return DataContext.SaveChanges();
        }
        public int Add(MappingSyncModel mapping)
        {
            var mappingRepo = new MappingRepository(DatabaseFactory);
            var mappintTypeRepo = new MappingTypeRepository(DatabaseFactory);
            var item = new Mapping
            {
                CreationDate = DateTime.Now,
                IsActive = mapping.IsActive,
                MappingType = mappintTypeRepo.GetMany(x => x.Id == mapping.MappingTypeId).FirstOrDefault(),
                TypeId = mapping.TypeId,
                TypeName = mapping.TypeName
            };
            DataContext.Mappings.Add(item);
            return DataContext.SaveChanges();
        }

        public int Remove(MappingSyncModel model, bool notUsed)
        {
            var item = DataContext.Mappings.FirstOrDefault(x =>
                x.TypeId == model.TypeId && x.MappingType.Id == model.MappingTypeId);
            if (item != null) DataContext.Mappings.Remove(item);

            return DataContext.SaveChanges();
        }
    }
}