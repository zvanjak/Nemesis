using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nemesis.DAL;
using Nemesis.Domain;
using System.Linq.Expressions;
using System.Globalization;

namespace Nemesis.Services
{
    public class ActivityService
    {
        public static ICollection<WorkActivity> GetActivities(Expression<Func<WorkActivity, bool>> filter = null)
        {
            using (var context = new NemesisContext())
            {
                using (var repository = new GenericRepository<WorkActivity>(context))
                {
                    ICollection<WorkActivity> activities = repository.Get(filter, null, "RealizedForObjective, DoneBy, WorkOrder").ToList<WorkActivity>();
                    
                    return activities;
                }
            }
        }

        public static ICollection<WorkActivity> GetTodayActivities()
        {
            return GetActivities(a => a.Date.Day.Equals(DateTime.Now.Day));
        }

        public static ICollection<WorkActivity> GetCurrentWeekActivities()
        {
            DateTime startWeek = GetStartOfCurrentWeek();
            DateTime endWeek = startWeek.AddDays(7);
            return GetActivities(a => a.Date.CompareTo(startWeek)>0 && a.Date.CompareTo(endWeek)<0);
        }

        private static DateTime GetStartOfCurrentWeek()
        {
            DateTime now = DateTime.Now;
            DayOfWeek day = now.DayOfWeek;
            switch (day)
            {
                case DayOfWeek.Monday:
                    now = now.AddDays(0);
                    break;
                case DayOfWeek.Tuesday:
                    now = now.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    now = now.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    now = now.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    now = now.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    now = now.AddDays(-5);
                    break;
                case DayOfWeek.Sunday:
                    now = now.AddDays(-6);
                    break;
            }
            
            return new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
        }



        private static bool InCurrentWeek(DateTime date)
        {
            DateTime now = DateTime.Now;
            return GetIso8601WeekOfYear(now) == GetIso8601WeekOfYear(date);
        }

        private static int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static WorkActivity GetActivity(int id)
        {
            using (var context = new NemesisContext())
            {
                using (var repository = new GenericRepository<WorkActivity>(context))
                {
                    return repository.GetByID(id);
                }
            }
        }

        public static void Create(WorkActivity workActivity, int objectiveId, int workOrderId)
        {
            using (var context = new NemesisContext())
            {
                using (var objectiveRepo = new GenericRepository<Objective>(context))
                using (var workOrderRepo = new GenericRepository<WorkOrder>(context))
                using (var membersRepo = new GenericRepository<TeamMember>(context))
                using (var activityRepo = new GenericRepository<WorkActivity>(context))
                {
                    workActivity.RealizedForObjective = objectiveRepo.GetByID(objectiveId);
                    workActivity.WorkOrder = workOrderRepo.GetByID(workOrderId);
                    workActivity.DoneBy = membersRepo.GetByID(1);

                    activityRepo.Insert(workActivity);
                    activityRepo.Save();
                }
            }
        }
    }
}
