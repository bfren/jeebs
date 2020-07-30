using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using Jeebs.Util;

namespace Jm.Data
{
	/// <summary>
	/// Message about an error that has occurred during a data query
	/// </summary>
	public sealed class QueryErrorMsg : WithStringMsg
	{
		internal QueryErrorMsg(string error) : base(error) { }
	}

	/// <summary>
	/// Message about an exception that has occurred during a data query
	/// </summary>
	public sealed class QueryExceptionMsg : ExceptionMsg
	{
		/// <summary>
		/// Sql query text
		/// </summary>
		public string Sql { get; }

		/// <summary>
		/// Sql query parameters (JSON-encoded)
		/// </summary>
		public string Parameters { get; }

		internal QueryExceptionMsg(Exception ex, string query, object? parameters = null) : base(ex)
			=> (Sql, Parameters) = (query, Json.Serialise(parameters));

		/// <summary>
		/// Output exception details with query and parameters
		/// </summary>
		public override string ToString()
			=> $"{ExceptionType}: {ExceptionText} | Query: {Sql} | Parameters: {Parameters}";
	}
}
