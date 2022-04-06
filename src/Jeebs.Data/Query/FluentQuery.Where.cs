// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Query.Functions;
using Jeebs.Reflection;
using StrongId;

namespace Jeebs.Data.Query;

public sealed partial record class FluentQuery<TEntity, TId> : FluentQuery, IFluentQuery<TEntity, TId>
	where TEntity : IWithId<TId>
	where TId : class, IStrongId, new()
{
	/// <inheritdoc/>
	public IFluentQuery<TEntity, TId> Where(string clause, object parameters)
	{
		// If there are errors, return
		if (Errors.Count > 0)
		{
			return this;
		}

		// Check clause
		if (string.IsNullOrWhiteSpace(clause))
		{
			Errors.Add(new M.TryingToAddEmptyClauseToWhereMsg());
			return this;
		}

		// Get parameters
		var param = new QueryParametersDictionary();
		if (!param.TryAdd(parameters))
		{
			Errors.Add(new M.UnableToAddParametersToWhereMsg());
			return this;
		}

		// Add clause and return
		return Update(parts => parts with { WhereCustom = parts.WhereCustom.WithItem((clause, param)) });
	}

	/// <inheritdoc/>
	public IFluentQuery<TEntity, TId> Where(string columnAlias, Compare compare, dynamic? value)
	{
		// If there are errors, return
		if (Errors.Count > 0)
		{
			return this;
		}

		// Get column and add to QueryParts
		return QueryF.GetColumnFromAlias(Table, columnAlias).Switch(
			some: x => Update(parts => parts with { Where = parts.Where.WithItem((x, compare, value ?? DBNull.Value)) }),
			none: r => { Errors.Add(r); return this; }
		);
	}

	/// <inheritdoc/>
	public IFluentQuery<TEntity, TId> Where<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, Compare compare, TValue value) =>
		aliasSelector.GetPropertyInfo()
			.Switch(
				some: x => Where(x.Name, compare, value),
				none: this
			);

	/// <inheritdoc/>
	public IFluentQuery<TEntity, TId> WhereId(params TId[] ids) =>
		ids.Length switch
		{
			> 1 =>
				WhereIn(nameof(IWithId.Id), ids.Select(x => x.Value)),

			1 =>
				Where(nameof(IWithId.Id), Compare.Equal, ids[0].Value),

			_ =>
				this
		};

	/// <inheritdoc/>
	public IFluentQuery<TEntity, TId> WhereIn<TValue>(string columnAlias, IEnumerable<TValue> values) =>
		values.Any() switch
		{
			true =>
				Where(columnAlias, Compare.In, values),

			false =>
				this
		};

	/// <inheritdoc/>
	public IFluentQuery<TEntity, TId> WhereIn<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, IEnumerable<TValue> values) =>
		aliasSelector.GetPropertyInfo()
			.Switch(
				some: x => WhereIn(x.Name, values),
				none: this
			);

	/// <inheritdoc/>
	public IFluentQuery<TEntity, TId> WhereNotIn<TValue>(string columnAlias, IEnumerable<TValue> values) =>
		values.Any() switch
		{
			true =>
				Where(columnAlias, Compare.NotIn, values),

			false =>
				this
		};

	/// <inheritdoc/>
	public IFluentQuery<TEntity, TId> WhereNotIn<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, IEnumerable<TValue> values) =>
		aliasSelector.GetPropertyInfo()
			.Switch(
				some: x => WhereNotIn(x.Name, values),
				none: this
			);
}
