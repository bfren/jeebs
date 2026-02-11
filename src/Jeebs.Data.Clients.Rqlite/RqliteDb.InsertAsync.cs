// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;
using System.Threading.Tasks;

namespace Jeebs.Data.Clients.Rqlite;

public abstract partial class RqliteDb : Db
{
	/// <inheritdoc/>
	public override async Task<Result<TId>> InsertAsync<TId>(string query, object? param)
	{
		using var w = StartWork();
		return await w.ExecuteAsync(
			query, param ?? new()
		)
		.BindAsync(
			x => JsonSerializer.Deserialize<TId>(x.LastInsertId, Factory.JsonOptions) switch
			{
				TId id =>
					R.Wrap(id),

				_ =>
					R.Fail("Failed to convert '{Id}' to '{IdType}'.", x.LastInsertId, typeof(TId).Name)
						.Ctx(nameof(RqliteDb), nameof(InsertAsync))
			}
		);
	}
}
