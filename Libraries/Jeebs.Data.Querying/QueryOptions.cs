// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using Jeebs.Data.Enums;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc/>
	public abstract class QueryOptions : IQueryOptions
	{
		/// <inheritdoc/>
		public long? Id { get; set; }

		/// <inheritdoc/>
		public long[]? Ids { get; set; }

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
