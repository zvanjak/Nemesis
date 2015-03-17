using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Nemesis.Domain;
using Nemesis.DAL;

namespace Nemesis.Web.Models {
    public class TeamCreateModel {

        public TeamCreateModel()
        {
            Subteams = new List<String>();
            Members = new List<String>();


            using (var repo = new GenericRepository<TeamMember>(new NemesisContext())) {
                List<TeamMember> availableMembers = repo.Get().ToList();

                fillMemberItems(availableMembers);
            }

            using (var repo = new GenericRepository<Team>(new NemesisContext())) {
                List<Team> teams = repo.Get().ToList();

                fillTeamItems(teams);
            }

            
        }

        #region SUBMIT VALUES
        [Display(Name = "Team name")]
        public String Name { get; set; }

        [Display(Name = "Team leader")]
        public String TeamLeader { get; set; }

        [Display(Name = "Team members")]
        public List<String> Members { get; set; }

        [Display(Name = "Team members")]
        public String CurrentMember { get; set; }
        
        [Display(Name = "Team type")]
        public String TeamType { get; set; }

        [Display(Name = "Quality manager")]
        public String QualityManager { get; set; }

        [Display(Name = "Parent team")]
        public String ParentTeam { get; set; }

        [Display(Name = "Subteams")]
        public List<String> Subteams { get; set; }

        [Display(Name = "Subteams")]
        public String CurrentSubteam { get; set; }
        #endregion

        ///////////////////////
        #region DATA SOURCE PROPERTIES
        public IEnumerable<SelectListItem> TeamItems;

        public IEnumerable<SelectListItem> MemberItems;

        public IEnumerable<SelectListItem> TeamTypes {
            get 
            {
                List<SelectListItem> types = new List<SelectListItem>();
                foreach (TeamTypes t in Enum.GetValues(typeof(TeamTypes))) {
                    types.Add(new SelectListItem() { Value = t.ToString(), Text = t.ToString() });
                }

                return types;
            }
        }

        private void fillTeamItems(IEnumerable<Team> teams)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (Team t in teams) {
                items.Add(new SelectListItem() { Text = t.Display, Value = t.Id.ToString()});
            }

            TeamItems = items;
        }

        private void fillMemberItems(IEnumerable<TeamMember> members)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (TeamMember t in members) {
                items.Add(new SelectListItem() { Text = t.Display, Value = t.Id.ToString() });
            }

            MemberItems = items;
        }

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