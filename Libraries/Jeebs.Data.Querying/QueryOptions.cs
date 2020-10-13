using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Enums;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc/>
	public abstract class QueryOptions : IQueryOptions
	{
		/// <inheritdoc/>
		public long? Id { get; set; }

		/// <inheritdoc/>
		public (string selectColumn, SortOrder order)[]? Sort { get; set; }

		/// <inheritdoc/>
		public bool SortRandom { get; set; }

		/// <inheritdoc/>
		public long? Limit { get; set; } = 10;

		/// <inheritdoc/>
		public long? Offset { get; set; }
	}
}
