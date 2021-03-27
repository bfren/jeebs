// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Jeebs.Linq
{
	/// <summary>
	/// LinqExpression Extensions
	/// </summary>
	public static class LinqExpressionExtensions
	{
		/// <summary>
		/// Prepare a Linq Expression for use as property setter / getter
		/// </summary>
		/// <typeparam name="TObject">Object type</typeparam>
		/// <typeparam name="TProperty">Property type</typeparam>
		/// <param name="this">Expression to get property</param>
		public static PropertyInfo<TObject, TProperty>? GetPropertyInfo<TObject, TProperty>(this Expression<Func<TObject, TProperty>> @this)
		{
			// Sometimes the body may be a UnaryExpression
			var body = @this.Body switch
			{
				MemberExpression member =>
					member,

				UnaryExpression unary =>
					(MemberExpression)unary.Operand,

				_ =>
					null
			};

			// Create if not null
			if (body is not null)
			{
				var info = (PropertyInfo)body.Member;
				return new PropertyInfo<TObject, TProperty>(info);
			}

			return null;
		}
	}
}
