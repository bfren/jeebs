// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// The various parts required to build a query
	/// </summary>
	public interface IQueryParts
	{
		/// <summary>
		/// From table
		/// </summary>
		string From { get; }

		/// <summary>
		/// Select columns
		/// </summary>
		string? Select { get; set; }

		/// <summary>
		/// Inner Joins
		/// </summary>
		IList<(string table, string on, string equals)>? InnerJoin { get; set; }

		/// <summary>
		/// Left Joins
		/// </summary>
		IList<(string table, string on, string equals)>? LeftJoin { get; set; }

		/// <summary>
		/// Right Joins
		/// </summary>
		IList<(string table, string on, string equals)>? RightJoin { get; set; }

		/// <summary>
		/// Where statements
		/// </summary>
		IList<string>? Where { get; set; }

		/// <summary>
		/// Query Parameters
		/// </summary>
		IQueryParameters Parameters { get; set; }

		/// <summary>
		/// Order By columns
		/// </summary>
		IList<string>? OrderBy { get; set; }

		/// <summary>
		/// Limit number
		/// </summary>
		long? Limit { get; set; }

		/// <summary>
		/// Offset number
		/// </summary>
		long? Offset { get; set; }
	}
}
