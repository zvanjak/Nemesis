using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Nemesis.Domain;
using Nemesis.DAL;

namespace Nemesis.Services
{
    public interface IActivityService
    {
        ICollection<WorkActivity> All();
        ICollection<WorkActivity> TodayActivities();
        ICollection<WorkActivity> CurrentWeekActivities();
        void Create(WorkActivity model);
    }
}
