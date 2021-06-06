// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.TypeHandlers
{
	/// <summary>
	/// PostType TypeHandler
	/// </summary>
	public sealed class PostTypeTypeHandler : Dapper.SqlMapper.TypeHandler<PostType>
	{
		/// <summary>
		/// Parse the PostType value
		/// </summary>
		/// <param name="value">Database table value</param>
		/// <returns>PostType object</returns>
		public override PostType Parse(object value) =>
			value.ToString() switch
			{
				string postType =>
					PostType.Parse(postType),

				_ =>
					PostType.Post
			};

		/// <summary>
		/// Set the PostType table value
		/// </summary>
		/// <param name="parameter">IDbDataParameter object</param>
		/// <param name="value">PostType value</param>
		public override void SetValue(IDbDataParameter parameter, PostType value) =>
			parameter.Value = value.ToString();
	}
}
