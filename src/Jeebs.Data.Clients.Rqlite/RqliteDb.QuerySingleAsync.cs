// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Data.Functions;
using Jeebs.Data.Query;

namespace Jeebs.Data.Clients.Rqlite;

public abstract partial class RqliteDb : Db
{
	/// <inheritdoc/>
	public override async Task<Result<T>> QuerySingleAsync<T>(string query, object? param)
	{
		using var w = StartWork();
		return await w.QueryAsync<T>(query, param ?? new())
			.GetSingleAsync(x => x.Value<T>());
	}

	/// <inheritdoc/>
	public override Task<Result<T>> QuerySingleAsync<T>(IQueryParts parts) =>
		from q in Client.GetQuery(parts)
		from r in QuerySingleAsync<T>(q.query, q.param)
		select r;

	/// <inheritdoc/>
	public override Task<Result<T>> QuerySingleAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
		DataF.BuildQuery<T>(builder)
			.BindAsync(QuerySingleAsync<T>);
}
