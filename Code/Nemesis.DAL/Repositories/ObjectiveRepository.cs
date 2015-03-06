using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nemesis.Domain;

namespace Nemesis.DAL.Repositories
{
	public class ObjectiveRepository : GenericRepository<Objective>
	{
		public ObjectiveRepository(NemesisContext inContext) : base(inContext)
		{ }
	}
}
