// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;

namespace Jeebs.Data.Repository;

public sealed partial record class FluentQuery<TEntity, TId>
{
	/// <inheritdoc/>
	public Task<Result<long>> CountAsync() =>
		from q in Db.Client.GetCountQuery(Parts)
		from r in Db.QuerySingleAsync<long>(q.query, q.param)
		select r;
}
