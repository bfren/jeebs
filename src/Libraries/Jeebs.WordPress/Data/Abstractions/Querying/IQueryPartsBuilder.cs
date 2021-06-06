// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Querying
{
	/// <summary>
	/// Build the parts required to create a query
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	/// <typeparam name="TOptions">QueryOptions</typeparam>
	public interface IQueryPartsBuilder<TModel, TOptions>
		where TOptions : IQueryOptions
	{
		/// <summary>
		/// IAdapter
		/// </summary>
		IAdapter Adapter { get; }

		/// <summary>
		/// Build the query
		/// </summary>
		/// <param name="opt">TOptions</param>
		IQueryParts Build(TOptions opt);
	}
}
