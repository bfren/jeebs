using Jeebs.WordPress.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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
		public override PostType Parse(object value) => PostType.Parse(value.ToString());

		/// <summary>
		/// Set the PostType table value
		/// </summary>
		/// <param name="parameter">IDbDataParameter object</param>
		/// <param name="value">PostType value</param>
		public override void SetValue(IDbDataParameter parameter, PostType value) => parameter.Value = value.ToString();
	}
}
