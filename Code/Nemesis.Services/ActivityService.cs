using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nemesis.DAL;
using Nemesis.Domain;

namespace Nemesis.Services
{
    public class ActivityService : IActivityService
    {
        private readonly GenericRepository<WorkActivity> _repository;

        public ActivityService(GenericRepository<WorkActivity> repo)
        {
            this._repository = repo;
        }
        
        public ICollection<Domain.WorkActivity> All()
        {
            return _repository.Get().ToList();
        }

        public ICollection<Domain.WorkActivity> TodayActivities()
        {
            return _repository.Get().Where(
                    item => item.Date.Date.Equals(DateTime.Today)
                ).ToList();
        }

        public ICollection<Domain.WorkActivity> CurrentWeekActivities()
        {
            return _repository.Get().Where(
                    item => (
                        (item.Date.Date - DateTime.Today.Date.AddDays(-7)).CompareTo(TimeSpan.Zero) > 0
                        )
                ).ToList();
        }

        public void Create(WorkActivity model)
        {
            //TODO Set auth user
            model.DoneBy = new TeamMember("Pero", DateTime.Now.TimeOfDay.ToString());
            _repository.Insert(model);
            _repository.Save();
        }
    }
}
