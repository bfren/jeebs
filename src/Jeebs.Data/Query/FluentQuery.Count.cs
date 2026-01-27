// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;

namespace Jeebs.Data.Query;

public sealed partial record class FluentQuery<TEntity, TId>
{
	#region Count

	/// <inheritdoc/>
	public async Task<Result<long>> CountAsync()
	{
		using var w = await Db.StartWorkAsync();
		return await CountAsync(w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<long>> CountAsync(IDbTransaction transaction) =>
		from q in Db.Client.GetCountQuery(Parts)
		from r in Db.QuerySingleAsync<long>(q.query, q.param, CommandType.Text, transaction)
		select r;

	#endregion Count
}
