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
        private static string INCLUDE_ALL = "Objectives, AssignedToTeamMembers, WorkActivities, AssignedToTeam, CreatedBy, Parent";

        public static ICollection<Objective> GetObjectives<T>(Expression<Func<T, bool>> filter = null) where T : Objective
        {
            using (var context = new NemesisContext())
            {
                using (var repository = new GenericRepository<T>(context))
                {
                    ICollection<Objective> objs = repository.Get(filter, null, INCLUDE_ALL).ToList<Objective>();

                    foreach (Objective obj in objs)
                    {
                        foreach (Objective child in obj.Objectives)
                        {
                            child.AssignedToTeamMembers = child.AssignedToTeamMembers;
                        }
                    }

                    return objs;
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

        public static void UpdateObjective(Objective objective)
        {
            using (var context = new NemesisContext())
            {
                using (var repository = new GenericRepository<Objective>(context))
                {
                    repository.Update(objective);
                }
            }
        }

        public static void SetRealization(int id, int percentComplete)
        {
            using (var context = new NemesisContext())
            {
                using (var repository = new GenericRepository<Objective>(context))
                {
                    Objective objective = repository.GetByID(id);
                    objective.PercentComplete = percentComplete;
                    repository.Save();
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

        

        #region Navigation

        #region Week

        #region Get Current

        public static int GetCurrentWeekOrdNum()
        {
            DateTime now = DateTime.Now;
            return GetIso8601WeekOfYear(now);
        }

        public static DateTime GetCurrentWeekStart()
        {
            return GetWeekStart(DateTime.Now);
        }

        public static DateTime GetCurrentWeekEnd()
        {
            return GetWeekStart(DateTime.Now).AddDays(6);
        }

        #endregion

        #region Previous

        public static int PreviousWeekOrdNum(int weekOrdNum, int year)
        {
            if (weekOrdNum == 1)
            {
                weekOrdNum = GetNumberOfWeeksInYear(year - 1);
                return weekOrdNum;
            }
            else
            {
                return weekOrdNum - 1;
            }
        }

        public static DateTime PreviousWeekStart(DateTime weekStart)
        {
            return weekStart.AddDays(-7);
        }

        public static DateTime PreviousWeekEnd(DateTime weekEnd)
        {
            return weekEnd.AddDays(-7);
        }

        #endregion

        #region Next

        public static int NextWeekOrdNum(int weekOrdNum, int year)
        {
            if (weekOrdNum == GetNumberOfWeeksInYear(year))
            {
                return 1;
            }
            else
            {
                return weekOrdNum + 1;
            }
        }

        public static DateTime NextWeekStart(DateTime weekStart)
        {
            return weekStart.AddDays(7);
        }

        public static DateTime NextWeekEnd(DateTime weekEnd)
        {
            return weekEnd.AddDays(7);
        }

        #endregion

        #region Helpers

        private static DateTime GetWeekStart(DateTime date)
        {
            DayOfWeek day = date.DayOfWeek;
            switch (day)
            {
                case DayOfWeek.Monday:
                    date = date.AddDays(0);
                    break;
                case DayOfWeek.Tuesday:
                    date = date.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    date = date.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    date = date.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    date = date.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    date = date.AddDays(-5);
                    break;
                case DayOfWeek.Sunday:
                    date = date.AddDays(-6);
                    break;
            }

            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
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

        private static int GetNumberOfWeeksInYear(int year)
        {
            DateTime lastDayOfYear = new DateTime(year, 12, 28);
            return GetIso8601WeekOfYear(lastDayOfYear);
        }

        #endregion

        #endregion

        #region Month

        #region Get Current

        public static int GetCurrentMonthOrdNum()
        {
            return DateTime.Now.Month;
        }

        public static DateTime GetCurrentMonthStart()
        {
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, 1, 0, 0, 0);
        }

        public static DateTime GetCurrentMonthEnd()
        {
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 0, 0, 0);
        }

        #endregion

        #region Previous

        public static int PreviousMonthOrdNum(int monthOrdNum)
        {
            if (monthOrdNum == 1)
            {
                return 12;
            }
            else
            {
                return monthOrdNum - 1;
            }
        }

        public static DateTime PreviousMonthStart(DateTime monthStart)
        {
            DateTime previous = monthStart.AddMonths(-1);
            return new DateTime(previous.Year, previous.Month, 1);
        }

        public static DateTime PreviousMonthEnd(DateTime monthEnd)
        {
            DateTime previous = monthEnd.AddMonths(-1);
            return new DateTime(previous.Year, previous.Month, DateTime.DaysInMonth(previous.Year, previous.Month));
        }

        #endregion

        #region Next

        public static int NextMonthOrdNum(int monthOrdNum)
        {
            if (monthOrdNum == 12)
            {
                return 1;
            }
            else
            {
                return monthOrdNum + 1;
            }
        }

        public static DateTime NextMonthStart(DateTime monthStart)
        {
            DateTime next = monthStart.AddMonths(1);
            return new DateTime(next.Year, next.Month, 1);
        }

        public static DateTime NextMonthEnd(DateTime monthEnd)
        {
            DateTime next = monthEnd.AddMonths(1);
            return new DateTime(next.Year, next.Month, DateTime.DaysInMonth(next.Year, next.Month));
        }

        #endregion

        #endregion

        #region Quartal

        #region Get Current

        public static int GetCurrentQuartalOrdNum()
        {
            int currentMonth = DateTime.Now.Month;
            if (currentMonth < 4) { return 1; }
            if (currentMonth < 7) { return 2; }
            if (currentMonth < 10) { return 3; }
            return 4;
        }

        public static DateTime GetCurrentQuartalStart()
        {
            DateTime now = DateTime.Now;
            DateTime firstQuartalStart = new DateTime(now.Year, 1, 1, 0, 0, 0);
            DateTime secondQuartalStart = new DateTime(now.Year, 4, 1, 0, 0, 0);
            DateTime thirdQuartalStart = new DateTime(now.Year, 7, 1, 0, 0, 0);
            DateTime forthQuartalStart = new DateTime(now.Year, 10, 1, 0, 0, 0);
            if (now < secondQuartalStart) { return firstQuartalStart; }
            if (now < thirdQuartalStart) { return secondQuartalStart; }
            if (now < forthQuartalStart) { return thirdQuartalStart; }
            return forthQuartalStart;
        }

        public static DateTime GetCurrentQuartalEnd()
        {
            return GetCurrentQuartalStart().AddMonths(3).AddDays(-1);
        }

        #endregion

        #region Previous

        public static int PreviousQuartalOrdNum(int quartalOrdNum)
        {
            if (quartalOrdNum == 1)
            {
                return 4;
            }
            else
            {
                return quartalOrdNum - 1;
            }
        }

        public static DateTime PreviousQuartalStart(DateTime quartalStart)
        {
            DateTime previous = quartalStart.AddMonths(-3);
            return new DateTime(previous.Year, previous.Month, 1);
        }

        public static DateTime PreviousQuartalEnd(DateTime quartalEnd)
        {
            DateTime previous = quartalEnd.AddMonths(-3);
            return new DateTime(previous.Year, previous.Month, DateTime.DaysInMonth(previous.Year, previous.Month));
        }

        #endregion

        #region Next

        public static int NextQuartalOrdNum(int quartalOrdNum)
        {
            if (quartalOrdNum == 4)
            {
                return 1;
            }
            else
            {
                return quartalOrdNum + 1;
            }
        }

        public static DateTime NextQuartalStart(DateTime quartalStart)
        {
            DateTime next = quartalStart.AddMonths(3);
            return new DateTime(next.Year, next.Month, 1);
        }

        public static DateTime NextQuartalEnd(DateTime quartalEnd)
        {
            DateTime next = quartalEnd.AddMonths(3);
            return new DateTime(next.Year, next.Month, DateTime.DaysInMonth(next.Year, next.Month));
        }

        #endregion

        #endregion

        #endregion


    }
}