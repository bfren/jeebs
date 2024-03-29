// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Data.Query.Functions;
using Jeebs.Messages;

namespace Jeebs.Data.Query;

public sealed partial record class FluentQuery<TEntity, TId>
{
	/// <inheritdoc/>
	public async Task<Maybe<IEnumerable<TModel>>> QueryAsync<TModel>()
	{
		using var w = await Db.StartWorkAsync();
		return await QueryAsync<TModel>(w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<IEnumerable<TModel>>> QueryAsync<TModel>(IDbTransaction transaction)
	{
		// Return if there were any errors (usually when converting a column alias to a column object)
		if (Errors.Count > 0)
		{
			return F.None<IEnumerable<TModel>>(new ListMsg(Errors)).AsTask();
		}

		// Return if there are no where clauses
		if (Parts.Where.Count == 0 && Parts.WhereCustom.Count == 0)
		{
			return F.None<IEnumerable<TModel>, M.NoPredicatesMsg>().AsTask();
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
		return Db.QueryAsync<TModel>(query, param, CommandType.Text, transaction);
	}

	/// <inheritdoc/>
	public async Task<Maybe<TModel>> QuerySingleAsync<TModel>()
	{
		using var w = await Db.StartWorkAsync();
		return await QuerySingleAsync<TModel>(w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<TModel>> QuerySingleAsync<TModel>(IDbTransaction transaction) =>
		Maximum(
			1
		)
		.QueryAsync<TModel>(
			transaction
		)
		.UnwrapAsync(
			x => x.SingleValue<TModel>()
		);
}
