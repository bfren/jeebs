// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Querying
{
	/// <summary>
	/// Builds a Query in a fluent manner
	/// </summary>
	public interface IQueryBuilder
	{
		/// <summary>
		/// Query Stage 1: Set the model for this query
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		IQueryWithModel<T> WithModel<T>();
	}
}
