// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Data.Query;

namespace Jeebs.Data.Clients.Rqlite;

public sealed partial class RqliteDb : Db
{
	/// <inheritdoc/>
	public override Task<Result<T>> QuerySingleAsync<T>(string query, object? param)
	{
		using var c = Factory.CreateClient(Config.ConnectionString);
		return c.QueryAsync<T>(query, param ?? new()).GetSingleAsync(x => x.Value<T>());
	}

	/// <inheritdoc/>
	public override Task<Result<T>> QuerySingleAsync<T>(IQueryParts parts) => throw new NotImplementedException();

	/// <inheritdoc/>
	public override Task<Result<T>> QuerySingleAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder) => throw new NotImplementedException();
}
