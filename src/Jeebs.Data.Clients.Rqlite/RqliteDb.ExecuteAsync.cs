// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Threading.Tasks;

namespace Jeebs.Data.Clients.Rqlite;

public abstract partial class RqliteDb : Db
{
	/// <inheritdoc/>
	public override Task<Result<bool>> ExecuteAsync(string query, object? param) =>
		ExecuteAsync(true, (query, param ?? new()));

	/// <inheritdoc/>
	public async Task<Result<bool>> ExecuteAsync(bool asSingleTransaction, params (string query, object param)[] commands)
	{
		using var w = StartWork();
		return await w.ExecuteAsync(asSingleTransaction, [.. commands])
			.MapAsync(x => x.Any(r => r.RowsAffected > 0));
	}

	/// <inheritdoc/>
	public override async Task<Result<TReturn>> ExecuteAsync<TReturn>(string query, object? param)
	{
		using var w = StartWork();
		return await w.GetScalarAsync<TReturn>(query, param ?? new());
	}
}
