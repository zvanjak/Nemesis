using Nemesis.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.DAL.Configurations
{
    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.Description);
            this.Property(t => t.Name);
        }
    }
}
