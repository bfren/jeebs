// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs.WordPress.Data.Querying
{
	/// <summary>
	/// Saves query model (stage 1) and enables stage 2: save query options
	/// </summary>
	/// <typeparam name="TModel"></typeparam>
	public interface IQueryWithModel<TModel>
	{
		/// <summary>
		/// Query Stage 2: Set the options for this query
		/// </summary>
		/// <typeparam name="TOptions">QueryOptions</typeparam>
		/// <param name="options">Options to use</param>
		IQueryWithOptions<TModel, TOptions> WithOptions<TOptions>(TOptions options)
			where TOptions : IQueryOptions;

		/// <summary>
		/// Query Stage 2: Set the options for this query
		/// </summary>
		/// <typeparam name="TOptions">QueryOptions</typeparam>
		/// <param name="modify">[Optional] Action to modify default options</param>
		IQueryWithOptions<TModel, TOptions> WithOptions<TOptions>(Action<TOptions>? modify = null)
			where TOptions : IQueryOptions, new();
	}
}
