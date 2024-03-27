// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Jeebs.Reflection;

/// <summary>
/// Extension methods for <see cref="Expression"/> objects.
/// </summary>
public static class LinqExpressionExtensions
{
	/// <summary>
	/// Prepare a Linq MemberExpression for use as property setter / getter
	/// </summary>
	/// <typeparam name="TObject">Object type</typeparam>
	/// <typeparam name="TProperty">Property type</typeparam>
	/// <param name="this">Expression to get property</param>
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

	/// <summary>
	/// If <paramref name="expression"/> is a <see cref="MemberExpression"/>,
	/// return the <see cref="MemberInfo"/>;
	/// If <paramref name="expression"/> is a <see cref="UnaryExpression"/>,
	/// return the <see cref="UnaryExpression.Operand"/> member as <see cref="MemberInfo"/>
	/// </summary>
	/// <param name="expression">Expression body</param>
	private static Maybe<MemberInfo> GetMemberInfo(Expression expression) =>
		expression switch
		{
			MemberExpression memberExpression =>
				memberExpression.Member,

			UnaryExpression unaryExpression when unaryExpression.Operand is MemberExpression memberExpression =>
				memberExpression.Member,

			_ =>
				M.None
		};
}
