using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Jeebs
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
		/// <param name="expression">Expression to get property</param>
		/// <returns>PropertyInfo object</returns>
		public static PropertyInfo<TObject, TProperty> GetPropertyInfo<TObject, TProperty>(this Expression<Func<TObject, TProperty>> expression)
		{
			var body = (MemberExpression)expression.Body;
			return new PropertyInfo<TObject, TProperty>((PropertyInfo)body.Member);
		}
	}
}
