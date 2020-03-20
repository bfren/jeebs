using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Query args
	/// </summary>
	public class QueryArgs
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
		public List<string>? InnerJoin { get; set; }

		/// <summary>
		/// Left Join
		/// </summary>
		public List<string>? LeftJoin { get; set; }

		/// <summary>
		/// Right Join
		/// </summary>
		public List<string>? RightJoin { get; set; }

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
		public double? Limit { get; set; }

		/// <summary>
		/// Offset
		/// </summary>
		public double? Offset { get; set; }

		/// <summary>
		/// Create object
		/// </summary>
		public QueryArgs() => Parameters = new QueryParameters();
	}
}
