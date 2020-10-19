using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// IDb Extensions
	/// </summary>
	public static class DbExtensions
	{
		/// <summary>
		/// Start a new query
		/// </summary>
		/// <param name="this">IDb</param>
		public static IQueryWrapper GetQueryWrapper(this IDb @this)
			=> new QueryWrapper(@this);
	}
}
