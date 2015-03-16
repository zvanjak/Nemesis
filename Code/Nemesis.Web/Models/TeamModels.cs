using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

using Nemesis.Domain;
using Nemesis.DAL;

namespace Nemesis.Web.Models {
    public class TeamCreateModel {

        public TeamCreateModel()
        {
            using (var repo = new GenericRepository<TeamMember>(new NemesisContext())) {
                AvailableMembers = repo.Get().Select(s => s.Display).ToList();
            }

            //using (var repo = new GenericRepository<TeamMember>(c)) {
            //    AvailableMembers = repo.Get().Select(s => s.Display).ToList();
            //}

            using (var repo = new GenericRepository<Team>(new NemesisContext())) {
                CurrentTeams = repo.Get().Select(s => s.Name).ToList();
            }

            AvailableTeamTypes = new List<string>();
            foreach (TeamTypes t in Enum.GetValues(typeof(TeamTypes))) {
                AvailableTeamTypes.Add(t.ToString());
            }
        }

        #region SUBMIT VALUES
        [Display(Name = "Team name")]
        [Required(ErrorMessage = "Team name is required!")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Team leader is required!")]
        [Display(Name = "Team leader")]
        public String TeamLeader { get; set; }

        [Display(Name = "Team members")]
        public List<String> Members { get; set; }
        
        [Display(Name = "Team type")]
        public String TeamType { get; set; }

        [Display(Name = "Quality manager")]
        public String QualityManager { get; set; }

        [Display(Name = "Parent team")]
        public String ParentTeam { get; set; }

        [Display(Name = "Subteams")]
        public List<String> Subteams { get; set; }
        #endregion

        ///////////////////////
        #region DATA SOURCE PROPERTIES
        public List<String> AvailableMembers { get; set; }
        //public List<String> AvailableQualityManagers { get; set; }
        public List<String> CurrentTeams { get; set; }

        public List<String> AvailableTeamTypes { get; set; }
        #endregion
    }

    public class TeamViewModel {

        public TeamViewModel(Team team)
        {
            Name = team.Name;

            LeaderFullName = team.Leader.Display;

            MemberNames = new List<string>();
            foreach (TeamMember m in team.Members) {
                MemberNames.Add(m.Display);
            }
        }

        public string Name { get; set; }

        public string LeaderFullName { get; set; }

        public List<String> MemberNames { get; set; }
    }
}