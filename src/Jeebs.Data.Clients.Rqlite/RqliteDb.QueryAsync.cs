// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data.Functions;
using Jeebs.Data.Query;

namespace Jeebs.Data.Clients.Rqlite;

public abstract partial class RqliteDb : Db
{
	/// <inheritdoc/>
	public override async Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param)
	{
		using var c = Connect();
		return await c.QueryAsync<T>(query, param ?? new())
			.MapAsync(x => x.AsEnumerable());
	}

	/// <inheritdoc/>
	public override Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts) =>
		from q in Client.GetQuery(parts)
		from r in QueryAsync<T>(q.query, q.param)
		select r;

	/// <inheritdoc/>
	public override Task<Result<IEnumerable<T>>> QueryAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
		from p in DataF.BuildQuery<T>(builder)
		from r in QueryAsync<T>(p)
		select r;

	/// <inheritdoc/>
	public override Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
		from p in DataF.BuildQuery<T>(builder)
		from r in QueryAsync<T>(page, p)
		select r;
}
