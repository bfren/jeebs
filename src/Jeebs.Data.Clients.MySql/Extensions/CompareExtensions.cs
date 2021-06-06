﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Exceptions;

namespace Jeebs.Data.Clients.MySql
{
	/// <summary>
	/// Extension methods for Compare: ToOperator
	/// </summary>
	public static class CompareExtensions
	{
		/// <summary>
		/// Convert a <see cref="Compare"/> type to the actual MySQL operator<br/>
		/// Default value is "="
		/// </summary>
		/// <param name="this">Compare</param>
		public static string ToOperator(this Compare @this) =>
			@this switch
			{
				Compare.Equal =>
					"=",

				Compare.NotEqual =>
					"!=",

				Compare.Like =>
					"LIKE",

				Compare.LessThan =>
					"<",

				Compare.LessThanOrEqual =>
					"<=",

				Compare.MoreThan =>
					">",

				Compare.MoreThanOrEqual =>
					">=",

				Compare.In =>
					"IN",

				Compare.NotIn =>
					"NOT IN",

				_ =>
					throw new UnrecognisedSearchOperatorException(@this)
			};
	}
}
