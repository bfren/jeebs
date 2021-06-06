// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.TypeHandlers
{
	/// <summary>
	/// PostStatus TypeHandler
	/// </summary>
	public sealed class PostStatusTypeHandler : Dapper.SqlMapper.TypeHandler<PostStatus>
	{
		/// <summary>
		/// Parse the PostStatus value
		/// </summary>
		/// <param name="value">Database table value</param>
		/// <returns>PostStatus object</returns>
		public override PostStatus Parse(object value) =>
			value.ToString() switch
			{
				string postStatus =>
					PostStatus.Parse(postStatus),

				_ =>
					PostStatus.Draft
			};

		/// <summary>
		/// Set the PostStatus table value
		/// </summary>
		/// <param name="parameter">IDbDataParameter object</param>
		/// <param name="value">PostStatus value</param>
		public override void SetValue(IDbDataParameter parameter, PostStatus value) =>
			parameter.Value = value.ToString();
	}
}
