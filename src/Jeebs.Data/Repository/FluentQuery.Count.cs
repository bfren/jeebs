// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;

namespace Jeebs.Data.Repository;

public sealed partial record class FluentQuery<TEntity, TId>
{
	/// <inheritdoc/>
	public async Task<Result<long>> CountAsync()
	{
		if (CheckParts(nameof(CountAsync)) is Failure f)
		{
			return f;
		}

		// Get and execute query
		return await (
			from q in Db.Client.GetCountQuery(Parts)
			from r in Db.QuerySingleAsync<long>(q.query, q.param)
			select r
		);
	}
}
