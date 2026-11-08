// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;

namespace Jeebs.Data.Repository;

public sealed partial record class FluentQuery<TEntity, TId>
{
	/// <inheritdoc/>
	public async Task<Result<long>> CountAsync()
	{
		// Return if there were any errors (usually when converting a column alias to a column object)
		if (Errors.Count > 0)
		{
			return R.Fail("Query errors.", Errors).Ctx(nameof(FluentQuery<,>), nameof(CountAsync));
		}

		// Return if there are no where clauses
		if (Parts.Where.Count == 0 && Parts.WhereCustom.Count == 0)
		{
			return R.Fail("No predicates defined for WHERE clause.").Ctx(nameof(FluentQuery<,>), nameof(CountAsync));
		}

		// Get and execute query
		return await (
			from q in Db.Client.GetCountQuery(Parts)
			from r in Db.QuerySingleAsync<long>(q.query, q.param)
			select r
		);
	}
}
