using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain.Assets
{
	[Serializable]
	public class InvalidAttributeTypeException : Exception
	{
		public override string Message
		{
			get { return "InvalidAttributeTypeExceptionMessage"; }
		}
	}
}
