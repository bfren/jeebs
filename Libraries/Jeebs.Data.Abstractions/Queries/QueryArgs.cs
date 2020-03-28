﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Query args
	/// </summary>
	public class QueryArgs<T>
	{
		/// <summary>
		/// From table
		/// </summary>
		public string? From { get; set; }

		/// <summary>
		/// Select
		/// </summary>
		public string? Select { get; set; }

		/// <summary>
		/// Inner Join
		/// </summary>
		public List<(string table, string on, string equals)>? InnerJoin { get; set; }

		/// <summary>
		/// Left Join
		/// </summary>
		public List<(string table, string on, string equals)>? LeftJoin { get; set; }

		/// <summary>
		/// Right Join
		/// </summary>
		public List<(string table, string on, string equals)>? RightJoin { get; set; }

		/// <summary>
		/// Where
		/// </summary>
		public List<string>? Where { get; set; }

		/// <summary>
		/// Query Parameters
		/// </summary>
		public QueryParameters Parameters { get; set; }

		/// <summary>
		/// Order By
		/// </summary>
		public List<string>? OrderBy { get; set; }

		/// <summary>
		/// Limit
		/// </summary>
		public long? Limit { get; set; }

		/// <summary>
		/// Offset
		/// </summary>
		public long? Offset { get; set; }

		/// <summary>
		/// Create object
		/// </summary>
		public QueryArgs() => Parameters = new QueryParameters();
	}
}
