using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// WordPress Link ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public sealed record WpLinkId(long Value) : LongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public WpLinkId() : this(Default) { }
	}
}
