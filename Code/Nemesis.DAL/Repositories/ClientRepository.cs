using Nemesis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.DAL.Repositories
{
    public class ClientRepository : GenericRepository<Client>
    {
        public ClientRepository(NemesisContext inContext)
            : base(inContext)
        { }
    }
}
