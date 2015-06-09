using Nemesis.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain
{
	public abstract class EntityBase : ITeamSpace
	{
		/// <summary>
		/// Gets or sets the unique identifier of the entity.
		/// </summary>
		public virtual int Id { get; set; }
		/// <summary>
		/// Gets or sets the text representation of the entity.
		/// </summary>
		public abstract string Display { get; }
		/// <summary>
		/// Gets or sets a value that indicates whether the entity is archived.
		/// </summary>
		public virtual bool IsArchived { get; set; }

        public virtual int TeamSpaceId { get; set; }

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
		public override string ToString()
		{
			return Display;
		}
		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				var emptyHash = Guid.Empty.GetHashCode();
				var myHash = GetHashCode();
				var comparerHash = obj.GetHashCode();

				// Ako oba objekta imaju emptyHash (znači nemaju ispravan identifikator)
				// vrati false
				if (myHash.Equals(emptyHash) &&
						comparerHash.Equals(emptyHash))
					return false;

				return myHash.Equals(comparerHash);
			}

			return false;
		}
    }
}
