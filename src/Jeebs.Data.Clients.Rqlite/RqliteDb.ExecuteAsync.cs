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
			.MapAsync(x => x.All(r => r.RowsAffected > 0 || r.LastInsertId > 0));
	}
}
