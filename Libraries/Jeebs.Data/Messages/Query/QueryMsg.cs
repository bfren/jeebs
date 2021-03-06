// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Data;
using Jeebs;
using Microsoft.Extensions.Logging;

namespace Jm.Data
{
	/// <summary>
	/// Used in <see cref="Jeebs.Data.UnitOfWork"/>
	/// </summary>
	public sealed class QueryMsg : LoggableMsg
	{
		/// <summary>
		/// Name of executing method
		/// </summary>
		public string Method { get; set; }

		/// <summary>
		/// Query SQL
		/// </summary>
		public string Sql { get; set; }

		/// <summary>
		/// Query parameters
		/// </summary>
		public object? Parameters { get; set; }

		/// <summary>
		/// Query command type
		/// </summary>
		public CommandType CommandType { get; set; }

		/// <inheritdoc/>
		public override string Format =>
			"{Method}() - Query [{CommandType}]: {Sql} - Parameters: {Parameters}";

		/// <inheritdoc/>
		public override object[] ParamArray =>
			new[] { Method, CommandType, Sql, Parameters ?? new object() };

		/// <inheritdoc/>
		public override LogLevel Level =>
			LogLevel.Trace;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="method">Name of executing method</param>
		/// <param name="sql">Query SQL</param>
		/// <param name="parameters">Query parameters</param>
		/// <param name="commandType">Query command type</param>
		public QueryMsg(string method, string sql, object? parameters, CommandType commandType = CommandType.Text) =>
			(Method, Sql, Parameters, CommandType) = (method, sql, parameters, commandType);
	}
}
