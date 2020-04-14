using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	/// <summary>
	/// Query options
	/// </summary>
	public abstract class QueryOptions
	{
		/// <summary>
		/// Query Id
		/// </summary>
		public long? Id { get; set; }

		/// <summary>
		/// Query ORDER BY
		/// </summary>
		public (string selectColumn, SortOrder order)[]? Sort { get; set; }

		/// <summary>
		/// Set ORDER BY to random (will override Sort)
		/// </summary>
		public bool SortRandom { get; set; }

		/// <summary>
		/// Query LIMIT
		/// </summary>
		public long? Limit { get; set; } = 10;

		/// <summary>
		/// Query OFFSET
		/// </summary>
		public long? Offset { get; set; }
	}
}
