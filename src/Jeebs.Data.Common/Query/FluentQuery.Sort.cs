// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Reflection;

namespace Jeebs.Data.Query;

public sealed partial record class FluentQuery<TEntity, TId>
{
	/// <inheritdoc/>
	public IFluentQuery<TEntity, TId> Sort(string columnAlias, SortOrder order)
	{
		if (Errors.Count > 0)
		{
			return this;
		}

		return DataF.GetColumnFromAlias(Table, columnAlias).Match(
			ok: x => Update(parts => parts with { Sort = parts.Sort.WithItem((x, order)) }),
			fail: f => { Errors.Add(f); return this; }
	   );
	}

	/// <inheritdoc/>
	public IFluentQuery<TEntity, TId> Sort<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, SortOrder order) =>
		aliasSelector.GetPropertyInfo()
			.Match(
				some: x => Sort(x.Name, order),
				none: () => this
			);
}
