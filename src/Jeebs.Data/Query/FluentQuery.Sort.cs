// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
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
	public IFluentQuery<TEntity, TId> OrderBy(string columnAlias, SortOrder order)
	{
		if (Errors.Count > 0)
		{
			return this;
		}

		return QueryF.GetColumnFromAlias(Table, columnAlias).Switch(
			some: x => this with
			{
				QueryParts = QueryParts with
				{
					Sort = QueryParts.Sort.WithItem((x, order))
				}
			},
			none: r =>
			{
				Errors.Add(r);
				return this;
			}
	   );
	}

	/// <inheritdoc/>
	public IFluentQuery<TEntity, TId> OrderBy<TValue>(Expression<Func<TEntity, TValue>> selector, SortOrder order) =>
		selector.GetPropertyInfo()
			.Switch(
				some: x => OrderBy(x.Name, order),
				none: this
			);
}