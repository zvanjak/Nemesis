using System.Collections.Generic;
using System.Text;
using Nemesis.Domain;
using Nemesis.Domain.Security;

namespace Nemesis.Domain.Assets
{
    public class AssetAssignments
    {
        public ICollection<User> Responsible { get; set; }
        public ICollection<User> Accountable { get; set; }
        public ICollection<User> Consulted { get; set; }
        public ICollection<User> Informed { get; set; }

        public bool IsUsersAsset(User user)
        {
            return Accountable.Contains(user) || Responsible.Contains(user);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("<b>Responsible</b><br/>");
            builder.Append(ToHtmlList(Responsible));
            builder.Append("<br/><b>Accountable</b><br/>");
            builder.Append(ToHtmlList(Accountable));
            builder.Append("<br/><b>Consulted</b><br/>");
            builder.Append(ToHtmlList(Consulted));
            builder.Append("<br/><b>Informed</b><br/>");
            builder.Append(ToHtmlList(Informed));

            return builder.ToString();
        }
        private static string ToHtmlList(ICollection<User> collection)
        {
            var builder = new StringBuilder();
            if (collection.Count > 0)
            {
                builder.Append("<ul>");
                foreach (var teamMember in collection)
                {
                    builder.Append("<li>" + teamMember.Username + "</li>");
                }
                builder.Append("</ul>");
            }

            return builder.ToString();
        }
    }
}
