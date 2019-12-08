using Jeebs.WordPress.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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
		public override CommentType Parse(object value) => CommentType.Parse(value.ToString());

		/// <summary>
		/// Set the CommentType table value
		/// </summary>
		/// <param name="parameter">IDbDataParameter object</param>
		/// <param name="value">CommentType value</param>
		public override void SetValue(IDbDataParameter parameter, CommentType value) => parameter.Value = value.ToString();
	}
}
