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
		using var w = StartWork();
		return await w.QueryAsync<T>(query, param ?? new())
			.MapAsync(x => x.AsEnumerable());
	}

	/// <inheritdoc/>
	public override Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts) =>
		Client.GetQuery(parts)
			.BindAsync(x => QueryAsync<T>(x.query, x.param));

	/// <inheritdoc/>
	public override Task<Result<IEnumerable<T>>> QueryAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
		DataF.BuildQuery<T>(builder)
			.BindAsync(QueryAsync<T>);

	/// <inheritdoc/>
	public override Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
		DataF.BuildQuery<T>(builder)
			.BindAsync(x => QueryAsync<T>(page, x));
}
