// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data.Query;

namespace Jeebs.Data.Clients.Rqlite;

public sealed partial class RqliteDb : Db
{
	/// <inheritdoc/>
	public override async Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param)
	{
		using var c = Factory.CreateClient(Config.ConnectionString);
		return await c.QueryAsync<T>(query, param ?? new()).MapAsync(x => x.AsEnumerable());
	}

	/// <inheritdoc/>
	public override Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts) => throw new NotImplementedException();

	/// <inheritdoc/>
	public override Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts) => throw new NotImplementedException();

	/// <inheritdoc/>
	public override Task<Result<IEnumerable<T>>> QueryAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder) => throw new NotImplementedException();

	/// <inheritdoc/>
	public override Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, Func<IQueryBuilder, IQueryBuilderWithFrom> builder) => throw new NotImplementedException();
}
