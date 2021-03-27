// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs.Data.Enums;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryParts"/>
	public sealed record QueryParts(ITable From) : IQueryParts
	{
		/// <inheritdoc/>
		public List<IColumn> Select { get; init; } = new();

		/// <inheritdoc/>
		public List<(IColumn on, IColumn equals)> InnerJoin { get; init; } = new();

		/// <inheritdoc/>
		public List<(IColumn on, IColumn equals)> LeftJoin { get; init; } = new();

		/// <inheritdoc/>
		public List<(IColumn on, IColumn equals)> RightJoin { get; init; } = new();

		/// <inheritdoc/>
		public List<(IColumn column, SearchOperator op, object value)> Where { get; init; } = new();

		/// <inheritdoc/>
		public IQueryParameters Parameters { get; init; } = new QueryParameters();

		/// <inheritdoc/>
		public List<(IColumn column, SortOrder order)> OrderBy { get; init; } = new();

		/// <inheritdoc/>
		public long? Maximum { get; init; }

		/// <inheritdoc/>
		public long Skip { get; init; }
	}
}
