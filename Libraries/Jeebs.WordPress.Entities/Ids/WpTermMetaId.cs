using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// WordPress Term Meta ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public sealed record WpTermMetaId(long Value) : LongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public WpTermMetaId() : this(Default) { }
	}
}
