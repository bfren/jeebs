// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jeebs.Data.Querying
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
			where TOptions : QueryOptions;

		/// <summary>
		/// Query Stage 2: Set the options for this query
		/// </summary>
		/// <typeparam name="TOptions">QueryOptions</typeparam>
		/// <param name="modify">[Optional] Action to modify default options</param>
		IQueryWithOptions<TModel, TOptions> WithOptions<TOptions>(Action<TOptions>? modify = null)
			where TOptions : QueryOptions, new();
	}
}
