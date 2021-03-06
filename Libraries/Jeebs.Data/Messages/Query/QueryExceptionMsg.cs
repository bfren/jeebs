// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

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
		public object? Parameters { get; }

		/// <inheritdoc/>
		public override string Format =>
			"{ExceptionType}: {ExceptionText} | Query: {Sql} | Parameters: {Parameters}";

		/// <inheritdoc/>
		public override object[] ParamArray =>
			new[] { ExceptionType, ExceptionText, Sql, Parameters ?? new object() };

		internal QueryExceptionMsg(Exception ex, string query, object? parameters = null) : base(ex) =>
			(Sql, Parameters) = (query, parameters);
	}
}
