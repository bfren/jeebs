// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Threading.Tasks;

namespace Jeebs.Data.Clients.Rqlite;

public sealed partial class RqliteDb : Db
{
	/// <inheritdoc/>
	public override async Task<Result<bool>> ExecuteAsync(string query, object? param)
	{
		using var c = Factory.CreateClient(Config.ConnectionString);
		return await c.ExecuteAsync(query, param ?? new())
			.MapAsync(x => x.Any(r => r.RowsAffected > 0));
	}

	/// <inheritdoc/>
	public override async Task<Result<TReturn>> ExecuteAsync<TReturn>(string query, object? param)
	{
		using var c = Factory.CreateClient(Config.ConnectionString);
		return await c.GetScalarAsync<TReturn>(query, param ?? new());
	}
}
