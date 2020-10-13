using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Enums;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// Query options
	/// </summary>
	public interface IQueryOptions
	{
		/// <summary>
		/// Query Id
		/// </summary>
		long? Id { get; set; }

		/// <summary>
		/// Query ORDER BY
		/// </summary>
		(string selectColumn, SortOrder order)[]? Sort { get; set; }

		/// <summary>
		/// Set ORDER BY to random (will override Sort)
		/// </summary>
		bool SortRandom { get; set; }

		/// <summary>
		/// Query LIMIT
		/// </summary>
		long? Limit { get; set; }

		/// <summary>
		/// Query OFFSET
		/// </summary>
		long? Offset { get; set; }
	}
}