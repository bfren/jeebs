// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Data.QueryBuilder;

namespace Jeebs.Data.Common.FluentQuery;

public sealed partial record class FluentQuery<TEntity, TId>
{
	/// <inheritdoc/>
	public override async Task<Result<IEnumerable<TModel>>> QueryAsync<TModel>()
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
			return R.Fail("Query errors.", Errors).Ctx(nameof(FluentQuery), nameof(QueryAsync));
		}

		// Return if there are no where clauses
		if (Parts.Where.Count == 0 && Parts.WhereCustom.Count == 0)
		{
			return R.Fail("No predicates defined for WHERE clause.").Ctx(nameof(FluentQuery), nameof(QueryAsync));
		}

		// Add select columns to query
		var parts = Parts.SelectColumns.Count switch
		{
			0 =>
				new QueryParts(Parts) with
				{
					SelectColumns = DataF.GetColumnsFromTable<TModel>(Table)
				},

			_ =>
				Parts
		};

		// Get and execute query
		return await (
			from q in Db.Client.GetQuery(parts)
			from r in Db.QueryAsync<TModel>(q.query, q.param, CommandType.Text, transaction)
			select r
		);
	}

	/// <inheritdoc/>
	public override async Task<Result<TModel>> QuerySingleAsync<TModel>()
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
}
