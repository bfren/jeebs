// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Data.Map;
using Jeebs.Reflection;

namespace Jeebs.Data.Repository;

public sealed partial record class FluentQuery<TEntity, TId>
{
	/// <inheritdoc/>
	public Task<Result<TValue>> ExecuteAsync<TValue>(string columnAlias) =>
		DataF.GetColumnFromAlias(Table, columnAlias)
			.BindAsync(x =>
				Update(parts => parts with
				{
					SelectColumns = new ColumnList([x])
				})
				.QuerySingleAsync<TValue>()
			);

	/// <inheritdoc/>
	public Task<Result<TValue>> ExecuteAsync<TValue>(Expression<Func<TEntity, TValue>> aliasSelector) =>
		aliasSelector.GetPropertyInfo()
			.ToResult(
				() => R.Fail("Unable to get PropertyInfo for alias selector.")
					.Ctx(nameof(FluentQuery<,>), nameof(ExecuteAsync))
			)
			.BindAsync(
				x => ExecuteAsync<TValue>(x.Name)
			);
}
