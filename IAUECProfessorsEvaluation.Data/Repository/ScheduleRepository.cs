using IAUECProfessorsEvaluation.Data.Infrastructure;
using IAUECProfessorsEvaluation.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace IAUECProfessorsEvaluation.Data.Repository
{
    public class ScheduleRepository : RepositoryBase<Schedule>, IScheduleRepository
    {
        private IDbSet<Schedule> _dbSet;
        public ScheduleRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _dbSet = DataContext.Set<Schedule>();
        }

        public void AddOrUpdate(Schedule model)
        {
            if (IsExist(x => x.Id == model.Id))
                Update(model);
            else
                Add(model);
        }

        public int Remove(Schedule schedule)
        {
            DataContext.Schedules.Remove(schedule);
            return DataContext.SaveChanges();
        }
    }
}
