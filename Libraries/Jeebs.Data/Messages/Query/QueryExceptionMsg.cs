using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using static F.JsonF;
using Jeebs;
using Microsoft.Extensions.Logging;

namespace Jm.Data
{
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

		/// <inheritdoc/>
		public override string Format
			=> "{ExceptionType}: {ExceptionText} | Query: {Sql} | Parameters: {Parameters}";

		/// <inheritdoc/>
		public override object[] ParamArray
			=> new[] { ExceptionType, ExceptionText, Sql, Parameters };

		internal QueryExceptionMsg(Exception ex, string query, object? parameters = null) : base(ex)
			=> (Sql, Parameters) = (query, Serialise(parameters));
	}
}
