// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;

namespace Jeebs.Data.Query;

/// <inheritdoc cref="IQueryParts"/>
public sealed record class QueryParts(ITable From) : IQueryParts
{
	/// <inheritdoc/>
	public bool SelectCount { get; init; }

	/// <inheritdoc/>
	public IColumnList SelectColumns { get; init; } =
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
	public IImmutableList<(IColumn column, Compare compare, dynamic value)> Where { get; init; } =
		new ImmutableList<(IColumn column, Compare compare, dynamic value)>();

	/// <inheritdoc/>
	public IImmutableList<(string clause, IQueryParametersDictionary parameters)> WhereCustom { get; init; } =
		new ImmutableList<(string clause, IQueryParametersDictionary parameters)>();

	/// <inheritdoc/>
	public IImmutableList<(IColumn column, SortOrder order)> Sort { get; init; } =
		new ImmutableList<(IColumn column, SortOrder order)>();

	/// <inheritdoc/>
	public bool SortRandom { get; init; }

	/// <inheritdoc/>
	public ulong? Maximum { get; init; }

	/// <inheritdoc/>
	public ulong Skip { get; init; }

	/// <summary>
	/// Create from another object by copying values
	/// </summary>
	/// <param name="parts">IQueryParts</param>
	public QueryParts(IQueryParts parts) : this(parts.From)
	{
		SelectCount = parts.SelectCount;
		SelectColumns = parts.SelectColumns;
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
