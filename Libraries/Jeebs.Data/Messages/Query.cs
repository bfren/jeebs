using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Util;

namespace Jm.Data
{
	/// <summary>
	/// Message about an error that has occurred during a data query
	/// </summary>
	public sealed class QueryError : WithString
	{
		internal QueryError(string error) : base(error) { }
	}

	/// <summary>
	/// Message about an exception that has occurred during a data query
	/// </summary>
	public sealed class QueryException : Exception
	{
		/// <summary>
		/// Sql query text
		/// </summary>
		public string Sql { get; }

		/// <summary>
		/// Sql query parameters (JSON-encoded)
		/// </summary>
		public string Parameters { get; }

		internal QueryException(System.Exception ex, string query, object? parameters = null) : base(ex)
		{
			Sql = query;
			Parameters = Json.Serialise(parameters);
		}

		/// <summary>
		/// Output exception details with query and parameters
		/// </summary>
		public override string ToString()
		{
			return $"{ExceptionType}: {ExceptionText} | Query: {Sql} | Parameters: {Parameters}";
		}
	}
}
