// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.TypeHandlers
{
	/// <summary>
	/// Comment TypeHandler
	/// </summary>
	public sealed class CommentTypeHandler : Dapper.SqlMapper.TypeHandler<CommentType>
	{
		/// <summary>
		/// Parse the CommentType value
		/// </summary>
		/// <param name="value">Database table value</param>
		/// <returns>CommentType object</returns>
		public override CommentType Parse(object value) =>
			value.ToString() switch
			{
				string commentType =>
					CommentType.Parse(commentType),

				_ =>
					CommentType.Blank
			};

		/// <summary>
		/// Set the CommentType table value
		/// </summary>
		/// <param name="parameter">IDbDataParameter object</param>
		/// <param name="value">CommentType value</param>
		public override void SetValue(IDbDataParameter parameter, CommentType value) =>
			parameter.Value = value.ToString();
	}
}
