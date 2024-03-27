// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Jeebs.Reflection;

public static partial class LinqExpressionExtensions
{
	/// <summary>
	/// Prepare a Linq MemberExpression for use as property setter / getter.
	/// </summary>
	/// <typeparam name="TObject">Object type.</typeparam>
	/// <typeparam name="TProperty">Property type<./typeparam>
	/// <param name="this">Expression to get property.</param>
	/// <returns>PropertyInfo.</returns>
	public static Maybe<PropertyInfo<TObject, TProperty>> GetPropertyInfo<TObject, TProperty>(
		this Expression<Func<TObject, TProperty>> @this
	) =>
		GetMemberInfo(
			@this.Body
		)
		.Bind(
			x => typeof(TObject).HasProperty(x.Name) switch
			{
				true =>
					M.Wrap(new PropertyInfo<TObject, TProperty>((PropertyInfo)x)),

				false =>
					M.None
			}
		);
}
