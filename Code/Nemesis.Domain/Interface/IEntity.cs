using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain
{
	/// <summary>
	/// Base interface for domain entities.
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// Gets the unique identifier of the entity.
		/// </summary>
		int Id { get; }
		/// <summary>
		/// Gets the text representation of the entity.
		/// </summary>
		string Display { get; }
		/// <summary>
		/// Gets a value that indicates whether the entity is archived.
		/// </summary>
		bool IsArchived { get; }
	}
}
