// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Exceptions;

namespace Jeebs.Data.Clients.SqlServer
{
	/// <summary>
	/// Extension methods for SearchOperator: ToOperator
	/// </summary>
	public static class SearchOperatorExtensions
	{
		/// <summary>
		/// Convert a <see cref="SearchOperator"/> type to the actual MS SQL operator<br/>
		/// Default value is "="
		/// </summary>
		/// <param name="this">SearchOperator</param>
		public static string ToOperator(this SearchOperator @this) =>
			@this switch
			{
				SearchOperator.Equal =>
					"=",

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

				SearchOperator.In =>
					"IN",

				SearchOperator.NotIn =>
					"NOT IN",

				_ =>
					throw new UnrecognisedSearchOperatorException(@this)
			};
	}
}
