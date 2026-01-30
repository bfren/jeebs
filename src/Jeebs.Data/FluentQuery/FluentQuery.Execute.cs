// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jeebs.Data.FluentQuery;

public partial record class FluentQuery<TEntity, TId>
{
	/// <inheritdoc/>
	public abstract Task<Result<TValue>> ExecuteAsync<TValue>(string columnAlias);

	/// <inheritdoc/>
	public abstract Task<Result<TValue>> ExecuteAsync<TValue>(Expression<Func<TEntity, TValue>> aliasSelector);
}
