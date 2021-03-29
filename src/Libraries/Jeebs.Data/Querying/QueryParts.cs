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
		public List<(IColumn from, IColumn to)> InnerJoin { get; init; } = new();

		/// <inheritdoc/>
		public List<(IColumn from, IColumn to)> LeftJoin { get; init; } = new();

		/// <inheritdoc/>
		public List<(IColumn from, IColumn to)> RightJoin { get; init; } = new();

		/// <inheritdoc/>
		public List<(IColumn column, SearchOperator op, object value)> Where { get; init; } = new();

		/// <inheritdoc/>
		public List<(IColumn column, SortOrder order)> Sort { get; init; } = new();

		/// <inheritdoc/>
		public long? Maximum { get; init; }

		/// <inheritdoc/>
		public long Skip { get; init; }

		/// <summary>
		/// Create from another object by copying values
		/// </summary>
		/// <param name="parts">IQueryParts</param>
		public QueryParts(IQueryParts parts) : this(parts.From)
		{
			Select = parts.Select;
			InnerJoin = parts.InnerJoin;
			LeftJoin = parts.LeftJoin;
			RightJoin = parts.RightJoin;
			Where = parts.Where;
			Sort = parts.Sort;
			Maximum = parts.Maximum;
			Skip = parts.Skip;
		}
	}
}
