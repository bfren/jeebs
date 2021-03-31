// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using System.Reflection;
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
			@this.Body switch
			{
				MemberExpression memberExpression =>
					new PropertyInfo<TObject, TProperty>((PropertyInfo)memberExpression.Member),

				_ =>
					None<PropertyInfo<TObject, TProperty>, Msg.ExpressionIsNotAMemberExpressionMsg>()
			};

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Only MemberExpressions can be used for PropertyInfo purposes</summary>
			public sealed record ExpressionIsNotAMemberExpressionMsg : IMsg { }
		}
	}
}
