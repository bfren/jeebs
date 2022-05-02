// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;

namespace Jeebs.Data.Query;

public sealed partial record class FluentQuery<TEntity, TId>
{
	#region Count

	/// <inheritdoc/>
	public async Task<Maybe<long>> CountAsync()
	{
		using var w = Db.UnitOfWork;
		return await CountAsync(w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<long>> CountAsync(IDbTransaction transaction)
	{
		var (query, param) = Db.Client.GetCountQuery(Parts);
		return Db.QuerySingleAsync<long>(query, param, CommandType.Text, transaction);
	}

	#endregion Count
}
