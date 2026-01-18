// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using System.Reflection;

namespace Jeebs.Reflection;

public static partial class LinqExpressionExtensions
{
	/// <summary>
	/// If <paramref name="expression"/> is a <see cref="MemberExpression"/>,
	/// return the <see cref="MemberInfo"/>;
	/// If <paramref name="expression"/> is a <see cref="UnaryExpression"/>,
	/// return the <see cref="UnaryExpression.Operand"/> member as <see cref="MemberInfo"/>
	/// </summary>
	/// <param name="expression">Expression body.</param>
	/// <returns>MemberInfo.</returns>
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
