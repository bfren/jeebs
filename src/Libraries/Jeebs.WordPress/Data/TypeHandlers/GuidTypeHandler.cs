// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Data;
using Dapper;

namespace Jeebs.WordPress.Data.TypeHandlers
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
		public override Guid Parse(object value) =>
			value.ToString() switch
			{
				string guid =>
					Guid.Parse(guid),

				_ =>
					Guid.Empty
			};

		/// <summary>
		/// Set Guid value
		/// </summary>
		/// <param name="parameter">IDbDataParameter</param>
		/// <param name="value">Guid</param>
		public override void SetValue(IDbDataParameter parameter, Guid value) =>
			parameter.Value = value.ToString();
	}
}
