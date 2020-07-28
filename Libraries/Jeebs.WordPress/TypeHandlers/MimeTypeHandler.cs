using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Jeebs.WordPress.TypeHandlers
{
	/// <summary>
	/// Mime TypeHandler
	/// </summary>
	public sealed class MimeTypeHandler : Dapper.SqlMapper.TypeHandler<MimeType>
	{
		/// <summary>
		/// Parse the MimeType value
		/// </summary>
		/// <param name="value">Database table value</param>
		/// <returns>MimeType object</returns>
		public override MimeType Parse(object value)
			=> MimeType.Parse(value.ToString());

		/// <summary>
		/// Set the MimeType table value
		/// </summary>
		/// <param name="parameter">IDbDataParameter object</param>
		/// <param name="value">MimeType value</param>
		public override void SetValue(IDbDataParameter parameter, MimeType value)
			=> parameter.Value = value.ToString();
	}
}
