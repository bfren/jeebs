using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Query Parts interface
	/// </summary>
	public interface IQueryParts
	{
		/// <summary>
		/// From table
		/// </summary>
		string? From { get; set; }

		/// <summary>
		/// Select
		/// </summary>
		string? Select { get; set; }

		/// <summary>
		/// Inner Join
		/// </summary>
		IList<(string table, string on, string equals)>? InnerJoin { get; set; }

		/// <summary>
		/// Left Join
		/// </summary>
		IList<(string table, string on, string equals)>? LeftJoin { get; set; }

		/// <summary>
		/// Right Join
		/// </summary>
		IList<(string table, string on, string equals)>? RightJoin { get; set; }

		/// <summary>
		/// Where
		/// </summary>
		IList<string>? Where { get; set; }

		/// <summary>
		/// Order By
		/// </summary>
		IList<string>? OrderBy { get; set; }

		/// <summary>
		/// Limit
		/// </summary>
		long? Limit { get; set; }

		/// <summary>
		/// Offset
		/// </summary>
		long? Offset { get; set; }

		/// <summary>
		/// Query Parameters
		/// </summary>
		IQueryParameters Parameters { get; set; }
	}
}
