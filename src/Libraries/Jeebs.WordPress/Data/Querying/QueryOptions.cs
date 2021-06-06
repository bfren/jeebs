// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data.Querying
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
