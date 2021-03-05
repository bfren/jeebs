using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Authentication.Entities
{
	/// <summary>
	/// User Entity ID
	/// </summary>
	/// <param name="Value">Id Value</param>
	public sealed record UserEntityId(long Value) : LongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public UserEntityId() : this(Default) { }
	}
}
