using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace Jeebs.Data.TypeHandlers
{
	/// <summary>
	/// Guid TypeHandler
	/// </summary>
	public sealed class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
	{
		/// <summary>
		/// Parse Guid
		/// </summary>
		/// <param name="value">Guid value</param>
		/// <returns>Guid</returns>
		public override Guid Parse(object value) => new Guid(value.ToString());

		/// <summary>
		/// Set Guid value
		/// </summary>
		/// <param name="parameter">IDbDataParameter</param>
		/// <param name="value">Guid</param>
		public override void SetValue(IDbDataParameter parameter, Guid value) => parameter.Value = value.ToString();
	}
}
