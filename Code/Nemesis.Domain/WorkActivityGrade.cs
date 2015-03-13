using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain
{
	public enum WorkActivitiyGradeEnum
	{
		/// <summary>
		/// Predstavlja nezadovoljavajuću ocjenu.
		/// </summary>
//		[LocalizedDescription("WorkActivitiesGradeUnsatisfactory")]
		Unsatisfactory = 1,
		/// <summary>
		/// Predstavlja jedva zadovoljavajuću ocjenu.
		/// </summary>
		//[LocalizedDescription("WorkActivitiesGradeBarelySatisfactory")]
		BarelySatisfactory = 2,
		/// <summary>
		/// Predstavlja zadovoljavajuću ocjenu.
		/// </summary>
		//[LocalizedDescription("WorkActivitiesGradeSatisfactory")]
		Satisfactory = 3,
		/// <summary>
		/// Predstavlja vrlo dobru ocjenu.
		/// </summary>
		//[LocalizedDescription("WorkActivitiesGradeVeryGood")]
		VeryGood = 4,
		/// <summary>
		/// Predstavlja izvrsnu ocjenu.
		/// </summary>
		//[LocalizedDescription("WorkActivitiesGradeExcellent")]
		Excellent = 5
	}
	public class WorkActivityGrade
	{
        /// <summary>
        /// Dohvaća ili postavlja identifikator ocjene dnevnih aktivnosti.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Dohvaća ili postavlja člana tima na kojeg se ocjena odnosi.
        /// </summary>
        public TeamMember TeamMember { get; set; }
        /// <summary>
        /// Dohvaća ili postavlja tim kojem pripada član koji se ocjenjuje.
        /// </summary>
        public Team Team { get; set; }
        /// <summary>
        /// Dohvaća ili postavlja datum ocjene dnevnih aktivnosti.
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Dohvaća ili postavlja ocjenu dnevnih aktivnosti.
        /// </summary>
				public WorkActivitiyGradeEnum Grade { get; set; }
        /// <summary>
        /// Dohvaća ili postavlja podatak jesu li dnevne aktivnosti
        /// ažurirane nakon ocjenjivanja.
        /// </summary>
        public bool IsChangedAfterGrade { get; set; }

        /// <summary>
        /// Instancira novi <b>GradeWorkActivities</b> objekt.
        /// </summary>
				public WorkActivityGrade()
        {
            Id = -1;
            TeamMember = null;
            Team = null;
            Date = DateTime.MinValue;
            Grade = WorkActivitiyGradeEnum.Excellent;
            IsChangedAfterGrade = false;
        }
	}
}
