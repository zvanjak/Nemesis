using System;
using System.Collections.Generic;
using System.Linq;
using Nemesis.DAL;
using Nemesis.Domain;
using System.Linq.Expressions;
using System.Globalization;

namespace Nemesis.Services
{
    public class ObjectiveService
    {
        private static string INCLUDE_ALL = "Objectives, AssignedToTeamMembers, WorkActivities, AssignedToTeam, CreatedBy";

        public static ICollection<Objective> GetObjectives<T>(Expression<Func<T, bool>> filter = null) where T : Objective
        {
            using (var context = new NemesisContext())
            {
                using (var repository = new GenericRepository<T>(context))
                {
                    return repository.Get(filter, null, INCLUDE_ALL).ToList<Objective>();
                }
            }
        }

        public static ICollection<Objective> GetObjectivesFor<T>(string date) where T : Objective
        {
            using (var context = new NemesisContext())
            {
                using (var repository = new GenericRepository<T>(context))
                {
                    return
                        repository.Get(null, null, INCLUDE_ALL).Where(x => x.CreatedOn.ToString("yyyy-MM-dd").Equals(date)).ToList<Objective>();
                }
            }
        }

        public static Objective GetObjective(int id)
        {
            using (var context = new NemesisContext())
            {
                using (var repository = new GenericRepository<Objective>(context))
                {
                    return repository.GetByID(id);
                }
            }
        }

        public static void Create(Objective objective, int parentId, int[] teamMemberIds) 
        {
            using (var context = new NemesisContext())
            {
                using (var membersRepo = new GenericRepository<TeamMember>(context))
                using (var objectiveRepository = new GenericRepository<Objective>(context))
                {
                    objective.Parent = objectiveRepository.GetByID(parentId);
                    var members = teamMemberIds.Select(id => membersRepo.GetByID(id)).ToList();
                    objective.AssignedToTeamMembers = members;

                    objectiveRepository.Insert(objective);
                    objectiveRepository.Save();
                }
            }
        }

        public static int GetCurrentWeek()
        {
            DateTime now = DateTime.Now;
            return GetIso8601WeekOfYear(now);
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

        public static int GetCurrentMonth()
        {
            return DateTime.Now.Month;
        }

        public static int GetCurrentQuartal()
        {
            int currentMonth = DateTime.Now.Month;
            if (currentMonth < 4) { return 1; }
            if (currentMonth < 7) { return 2; }
            if (currentMonth < 10) { return 3; }
            return 4;
        }

        public static int GetNumberOfWeeksInYear()
        {
            DateTime lastDayOfYear = new DateTime(DateTime.Now.Year, 12, 31);
            return GetIso8601WeekOfYear(lastDayOfYear);
        }
    }
}