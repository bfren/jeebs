// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data.Functions;
using Jeebs.Data.Query;

namespace Jeebs.Data.Repository;

public sealed partial record class FluentQuery<TEntity, TId>
{
	/// <inheritdoc/>
	public async Task<Result<IEnumerable<TModel>>> QueryAsync<TModel>()
	{
		if (CheckParts(nameof(QueryAsync)) is Failure f)
		{
			return f;
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
			from r in Db.QueryAsync<TModel>(q.query, q.param)
			select r
		);
	}

	/// <inheritdoc/>
	public Task<Result<TModel>> QuerySingleAsync<TModel>() =>
		Maximum(1).QueryAsync<TModel>().GetSingleAsync(x => x.Value<TModel>());
}
