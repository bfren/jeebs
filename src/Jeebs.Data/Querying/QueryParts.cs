// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryParts"/>
	public sealed record QueryParts(ITable From) : IQueryParts
	{
		/// <inheritdoc/>
		public IColumnList Select { get; init; } =
			new ColumnList();

		/// <inheritdoc/>
		public IImmutableList<(IColumn from, IColumn to)> InnerJoin { get; init; } =
			new ImmutableList<(IColumn from, IColumn to)>();

		/// <inheritdoc/>
		public IImmutableList<(IColumn from, IColumn to)> LeftJoin { get; init; } =
			new ImmutableList<(IColumn from, IColumn to)>();

		/// <inheritdoc/>
		public IImmutableList<(IColumn from, IColumn to)> RightJoin { get; init; } =
			new ImmutableList<(IColumn from, IColumn to)>();

		/// <inheritdoc/>
		public IImmutableList<(IColumn column, SearchOperator op, object value)> Where { get; init; } =
			new ImmutableList<(IColumn column, SearchOperator op, object value)>();

		/// <inheritdoc/>
		public IImmutableList<(string clause, IQueryParameters parameters)> WhereCustom { get; init; } =
			new ImmutableList<(string clause, IQueryParameters parameters)>();

		/// <inheritdoc/>
		public IImmutableList<(IColumn column, SortOrder order)> Sort { get; init; } =
			new ImmutableList<(IColumn column, SortOrder order)>();

		/// <inheritdoc/>
		public bool SortRandom { get; init; }

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
			WhereCustom = parts.WhereCustom;
			Sort = parts.Sort;
			SortRandom = parts.SortRandom;
			Maximum = parts.Maximum;
			Skip = parts.Skip;
		}
	}
}
