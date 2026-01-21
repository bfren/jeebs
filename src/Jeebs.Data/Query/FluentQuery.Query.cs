// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Data.Query.Functions;

namespace Jeebs.Data.Query;

public sealed partial record class FluentQuery<TEntity, TId>
{
	/// <inheritdoc/>
	public async Task<Result<IEnumerable<TModel>>> QueryAsync<TModel>()
	{
		using var w = await Db.StartWorkAsync();
		return await QueryAsync<TModel>(w.Transaction);
	}

	/// <inheritdoc/>
	public async Task<Result<IEnumerable<TModel>>> QueryAsync<TModel>(IDbTransaction transaction)
	{
		// Return if there were any errors (usually when converting a column alias to a column object)
		if (Errors.Count > 0)
		{
			return R.Fail(nameof(FluentQuery), nameof(QueryAsync),
				string.Join(Environment.NewLine, Errors)
			);
		}

		// Return if there are no where clauses
		if (Parts.Where.Count == 0 && Parts.WhereCustom.Count == 0)
		{
			return R.Fail(nameof(FluentQuery), nameof(QueryAsync),
				"No predicates defined for WHERE clause."
			);
		}

		// Add select columns to query
		var parts = Parts.SelectColumns.Count switch
		{
			0 =>
				new QueryParts(Parts) with
				{
					SelectColumns = QueryF.GetColumnsFromTable<TModel>(Table)
				},

			_ =>
				Parts
		};

		// If there are
		var (query, param) = Db.Client.GetQuery(parts);
		return await Db.QueryAsync<TModel>(query, param, CommandType.Text, transaction);
	}

	/// <inheritdoc/>
	public async Task<Result<TModel>> QuerySingleAsync<TModel>()
	{
		using var w = await Db.StartWorkAsync();
		return await QuerySingleAsync<TModel>(w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<TModel>> QuerySingleAsync<TModel>(IDbTransaction transaction) =>
		Maximum(
			1
		)
		.QueryAsync<TModel>(
			transaction
		)
		.GetSingleAsync(
			x => x.Value<TModel>()
		);
	//.UnwrapAsync(
	//	x => x.SingleValue<TModel>()
	//);
}
