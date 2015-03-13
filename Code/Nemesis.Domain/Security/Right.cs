using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain.Security
{
	public enum Right
	{
		UserAccountRead = 0,
		UserAccountReadWrite = 1,

		SecurityRoleRead = 10,
		SecurityRoleReadWrite = 11,

		TeamRead = 20,
		TeamReadWrite = 21,

		Accounting = 30,

		NZIRead = 40,
		NZIReadWrite = 41,

		MaintenanceOrderRead = 50,
		MaintenanceOrderReadWrite = 51,

		ClientRead = 60,
		ClientReadWrite = 61,

		// prava vezana uz Objectives 
		SummaryGoalRead = 1000,
		SummaryGoalReadWrite = 1001,
		SummaryGoalCreateObjectives = 1002,

		QuarterObjectiveRead = 1010,
		QuarterObjectiveReadWrite = 1011,
		QuarterObjectiveReadWritePersonal = 1012,
		QuarterObjectiveApprove = 1013,
		QuarterObjectiveReadWriteTeamObjectives = 1014,

		MonthObjectiveRead = 1020,
		MonthObjectiveReadWrite = 1021,
		MonthObjectiveReadWritePersonal = 1022,
		MonthObjectiveApprove = 1023,
		MonthObjectiveReadWriteTeamObjectives = 1024,
		MonthObjectiveMove = 1025,

		WeekObjectiveRead = 1030,
		WeekObjectiveReadWrite = 1031,
		WeekObjectiveReadWritePersonal = 1032,
		WeekObjectiveApprove = 1033,
		WeekObjectiveReadWriteTeamObjectives = 1034,
		WeekObjectiveMove = 1035,

		ObjectiveShowFromSubteams = 1040,

		WorkActivityRead = 1100,
		WorkActivityReadWrite = 1101,
		WorkActivityReadWriteTeam = 1102,
		WorkActivityGrade = 1105
	}
}
