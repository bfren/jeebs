// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

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
		public static IQueryWrapper GetQueryWrapper(this IDb @this) =>
			new QueryWrapper(@this);
	}
}
