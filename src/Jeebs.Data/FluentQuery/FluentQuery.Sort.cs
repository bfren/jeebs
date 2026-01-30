// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Reflection;

namespace Jeebs.Data.FluentQuery;

public abstract partial record class FluentQuery<TFluentQuery, TEntity, TId>
{
	/// <inheritdoc/>
	public TFluentQuery Sort(string columnAlias, SortOrder order)
	{
		if (Errors.Count > 0)
		{
			return Unchanged();
		}

		return DataF.GetColumnFromAlias(Table, columnAlias).Match(
			ok: x => Update(parts => parts with { Sort = parts.Sort.WithItem((x, order)) }),
			fail: f => { Errors.Add(f); return Unchanged(); }
	   );
	}

	/// <inheritdoc/>
	public TFluentQuery Sort<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, SortOrder order) =>
		aliasSelector.GetPropertyInfo()
			.Match(
				some: x => Sort(x.Name, order),
				none: Unchanged
			);
}
