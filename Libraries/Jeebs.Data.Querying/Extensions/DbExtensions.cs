using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// IDb Extensions
	/// </summary>
	public static class DbExtensions
	{
		/// <summary>
		/// Start a new query
		/// </summary>
		/// <param name="db">IDb</param>
		public static IQueryWrapper GetQueryWrapper(this IDb db) 
			=> new QueryWrapper(db);
	}
}
