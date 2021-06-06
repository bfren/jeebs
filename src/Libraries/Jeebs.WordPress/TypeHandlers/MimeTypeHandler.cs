// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;

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
		public override MimeType Parse(object value) =>
			value.ToString() switch
			{
				string mimeType =>
					MimeType.Parse(mimeType),

				_ =>
					MimeType.Blank
			};

		/// <summary>
		/// Set the MimeType table value
		/// </summary>
		/// <param name="parameter">IDbDataParameter object</param>
		/// <param name="value">MimeType value</param>
		public override void SetValue(IDbDataParameter parameter, MimeType value) =>
			parameter.Value = value.ToString();
	}
}
