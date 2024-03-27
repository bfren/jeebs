// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Data.Map;
using Jeebs.Data.Query.Functions;
using Jeebs.Reflection;

namespace Jeebs.Data.Query;

public sealed partial record class FluentQuery<TEntity, TId>
{
	/// <inheritdoc/>
	public async Task<Maybe<TValue>> ExecuteAsync<TValue>(string columnAlias)
	{
		using var w = await Db.StartWorkAsync();
		return await ExecuteAsync<TValue>(columnAlias, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<TValue>> ExecuteAsync<TValue>(string columnAlias, IDbTransaction transaction) =>
		QueryF.GetColumnFromAlias(Table, columnAlias)
			.BindAsync(x =>
				Update(parts => parts with
				{
					SelectColumns = new ColumnList([x])
				})
				.QuerySingleAsync<TValue>(transaction)
			);

	/// <inheritdoc/>
	public async Task<Maybe<TValue>> ExecuteAsync<TValue>(Expression<Func<TEntity, TValue>> aliasSelector)
	{
		using var w = await Db.StartWorkAsync();
		return await ExecuteAsync(aliasSelector, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<TValue>> ExecuteAsync<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, IDbTransaction transaction) =>
		aliasSelector.GetPropertyInfo()
			.BindAsync(
				x => ExecuteAsync<TValue>(x.Name, transaction)
			);
}
