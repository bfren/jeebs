// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;

namespace Jeebs.Data.Clients.MySql
{
	/// <summary>
	/// Extension methods for SearchOperator: ToOperator
	/// </summary>
	public static class SearchOperatorExtensions
	{
		/// <summary>
		/// Convert a <see cref="SearchOperator"/> type to the actual MySQL operator<br/>
		/// Default value is "="
		/// </summary>
		/// <param name="this">SearchOperator</param>
		public static string ToOperator(this SearchOperator @this) =>
			@this switch
			{
				SearchOperator.NotEqual =>
					"!=",

				SearchOperator.Like =>
					"LIKE",

				SearchOperator.LessThan =>
					"<",

				SearchOperator.LessThanOrEqual =>
					"<=",

				SearchOperator.MoreThan =>
					">",

				SearchOperator.MoreThanOrEqual =>
					">=",

				_ =>
					"="
			};
	}
}
