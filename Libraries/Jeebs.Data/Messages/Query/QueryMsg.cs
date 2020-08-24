using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Jeebs;
using Microsoft.Extensions.Logging;

namespace Jm.Data
{
	public sealed class QueryMsg : LoggableMsg
	{
		public string Method { get; set; }

		public CommandType CommandType { get; set; }

		public string Sql { get; set; }

		public object? Parameters { get; set; }

		public override string Format
			=> "{Method}() - Query [{CommandType}]: {Query} - Parameters: {Parameters}";

		public override object[] ParamArray
			=> new[] { Method, CommandType, Sql, Parameters ?? new object() };

		public override LogLevel Level
			=> LogLevel.Trace;

		public QueryMsg(string method, string sql, object? parameters, CommandType commandType = CommandType.Text)
			=> (Method, Sql, Parameters, CommandType) = (method, sql, parameters, commandType);
	}
}
