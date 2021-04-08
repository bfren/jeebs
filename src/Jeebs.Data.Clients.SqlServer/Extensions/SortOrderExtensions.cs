// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;

namespace Jeebs.Data.Clients.SqlServer
{
	/// <summary>
	/// Extension methods for SortOrder: ToOperator
	/// </summary>
	public static class SortOrderExtensions
	{
		/// <summary>
		/// Convert a <see cref="SortOrder"/> type to the actual MySQL operator<br/>
		/// Default value is "ASC"
		/// </summary>
		/// <param name="this">SortOrder</param>
		public static string ToOperator(this SortOrder @this) =>
			@this switch
			{
				SortOrder.Descending =>
					"DESC",

				_ =>
					"ASC"
			};
	}
}
