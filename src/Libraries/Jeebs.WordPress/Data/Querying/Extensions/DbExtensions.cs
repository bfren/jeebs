// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Querying
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
