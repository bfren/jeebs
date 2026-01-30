// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.QueryBuilder;
using Jeebs.Reflection;

namespace Jeebs.Data.FluentQuery;

public abstract partial record class FluentQuery<TFluentQuery, TEntity, TId>
{
	/// <inheritdoc/>
	public TFluentQuery Where(string clause, object parameters)
	{
		// If there are errors, return
		if (Errors.Count > 0)
		{
			return Unchanged();
		}

		// Check clause
		if (string.IsNullOrWhiteSpace(clause))
		{
			Errors.Add(new("Trying to add empty clause to WHERE."));
			return Unchanged();
		}

		// Get parameters
		var param = new QueryParametersDictionary();
		if (!param.TryAdd(parameters))
		{
			Errors.Add(new("Unable to add parameters to WHERE."));
			return Unchanged();
		}

		// Add clause and return
		return Update(parts => parts with
		{
			WhereCustom = parts.WhereCustom.WithItem((clause, param))
		});
	}

	/// <inheritdoc/>
	public TFluentQuery Where(string columnAlias, Compare compare, dynamic? value)
	{
		// If there are errors, return
		if (Errors.Count > 0)
		{
			return Unchanged();
		}

		// Get column and add to QueryParts
		return DataF.GetColumnFromAlias(Table, columnAlias).Match(
			ok: x => Update(parts => parts with
			{
				Where = parts.Where.WithItem((x, compare, value ?? DBNull.Value))
			}),
			fail: f =>
			{
				Errors.Add(f);
				return Unchanged();
			}
		);
	}

	/// <inheritdoc/>
	public TFluentQuery Where<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, Compare compare, TValue value) =>
		aliasSelector.GetPropertyInfo()
			.Match(
				some: x => Where(x.Name, compare, value),
				none: Unchanged
			);

	/// <inheritdoc/>
	public TFluentQuery WhereId(params TId[] ids) =>
		ids.Length switch
		{
			> 1 =>
				WhereIn(nameof(IWithId.Id), ids.Select(x => x.Value)),

			1 =>
				Where(nameof(IWithId.Id), Compare.Equal, ids[0].Value),

			_ =>
				Unchanged()
		};

	/// <inheritdoc/>
	public TFluentQuery WhereIn<TValue>(string columnAlias, IEnumerable<TValue> values) =>
		values.Any() switch
		{
			true =>
				Where(columnAlias, Compare.In, values),

			false =>
				Unchanged()
		};

	/// <inheritdoc/>
	public TFluentQuery WhereIn<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, IEnumerable<TValue> values) =>
		aliasSelector.GetPropertyInfo()
			.Match(
				some: x => WhereIn(x.Name, values),
				none: Unchanged
			);

	/// <inheritdoc/>
	public TFluentQuery WhereNotIn<TValue>(string columnAlias, IEnumerable<TValue> values) =>
		values.Any() switch
		{
			true =>
				Where(columnAlias, Compare.NotIn, values),

			false =>
				Unchanged()
		};

	/// <inheritdoc/>
	public TFluentQuery WhereNotIn<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, IEnumerable<TValue> values) =>
		aliasSelector.GetPropertyInfo()
			.Match(
				some: x => WhereNotIn(x.Name, values),
				none: Unchanged
			);
}
