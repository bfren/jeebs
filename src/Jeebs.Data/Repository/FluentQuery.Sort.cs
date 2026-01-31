// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Functions;
using Jeebs.Reflection;

namespace Jeebs.Data.Repository;

public sealed partial record class FluentQuery<TEntity, TId>
{
	/// <inheritdoc/>
	public IFluentQuery<TEntity, TId> Sort(string columnAlias, SortOrder order)
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
	public IFluentQuery<TEntity, TId> Sort<TValue>(Expression<Func<TEntity, TValue>> aliasSelector, SortOrder order) =>
		aliasSelector.GetPropertyInfo()
			.Match(
				some: x => Sort(x.Name, order),
				none: Unchanged
			);
}
