// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jeebs.Data.FluentQuery;

public abstract partial record class FluentQuery<TEntity, TId>
{
	/// <inheritdoc/>
	public abstract Task<Result<IEnumerable<TModel>>> QueryAsync<TModel>();

	/// <inheritdoc/>
	public abstract Task<Result<TModel>> QuerySingleAsync<TModel>();
}
