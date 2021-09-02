// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using System.Reflection;
using Jeebs.Reflection;
using static F.OptionF;

namespace Jeebs.Linq
{
	/// <summary>
	/// LinqExpression Extensions: GetPropertyInfo
	/// </summary>
	public static class LinqExpressionExtensions
	{
		/// <summary>
		/// Prepare a Linq MemberExpression for use as property setter / getter
		/// </summary>
		/// <typeparam name="TObject">Object type</typeparam>
		/// <typeparam name="TProperty">Property type</typeparam>
		/// <param name="this">Expression to get property</param>
		public static Option<PropertyInfo<TObject, TProperty>> GetPropertyInfo<TObject, TProperty>(
			this Expression<Func<TObject, TProperty>> @this
		) =>
			GetMemberInfo(
				@this.Body
			)
			.Bind(
				x => typeof(TObject).HasProperty(x.Name) switch
				{
					true =>
						Some(new PropertyInfo<TObject, TProperty>((PropertyInfo)x)),

					false =>
						None<PropertyInfo<TObject, TProperty>>(new Msg.PropertyDoesNotExistOnTypeMsg<TObject>(x.Name))
				}
			);

		/// <summary>
		/// If <paramref name="expression"/> is a <see cref="MemberExpression"/>,
		/// return the <see cref="MemberInfo"/>;
		/// If <paramref name="expression"/> is a <see cref="UnaryExpression"/>,
		/// return the <see cref="UnaryExpression.Operand"/> member as <see cref="MemberInfo"/>
		/// </summary>
		/// <param name="expression">Expression body</param>
		private static Option<MemberInfo> GetMemberInfo(Expression expression) =>
			expression switch
			{
				MemberExpression memberExpression =>
					memberExpression.Member,

				UnaryExpression unaryExpression when unaryExpression.Operand is MemberExpression memberExpression =>
					memberExpression.Member,

				_ =>
					None<MemberInfo, Msg.ExpressionIsNotAMemberExpressionMsg>()
			};

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Only MemberExpressions can be used for PropertyInfo purposes</summary>
			public sealed record class ExpressionIsNotAMemberExpressionMsg : IMsg { }

			/// <summary>The specified property does not exist on the type</summary>
			public sealed record class PropertyDoesNotExistOnTypeMsg<T>(string Value) : WithValueMsg<string> { }
		}
	}
}
