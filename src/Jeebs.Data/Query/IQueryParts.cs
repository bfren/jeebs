// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Enums;

namespace Jeebs.Data.Query;

/// <summary>
/// Contains all the parts necessary to build a query.
/// </summary>
public interface IQueryParts
{
	/// <summary>
	/// From table.
	/// </summary>
	ITable From { get; init; }

	/// <summary>
	/// If true, tells the query to select a count of rows instead of columns.
	/// </summary>
	bool SelectCount { get; init; }

	/// <summary>
	/// Select columns (if empty will select all columns).
	/// </summary>
	IColumnList SelectColumns { get; init; }

	/// <summary>
	/// Inner Joins.
	/// </summary>
	IImmutableList<(IColumn from, IColumn to)> InnerJoin { get; init; }

	/// <summary>
	/// Left Joins.
	/// </summary>
	IImmutableList<(IColumn from, IColumn to)> LeftJoin { get; init; }

	/// <summary>
	/// Right Joins.
	/// </summary>
	IImmutableList<(IColumn from, IColumn to)> RightJoin { get; init; }

	/// <summary>
	/// Where predicates.
	/// </summary>
	IImmutableList<(IColumn column, Compare compare, dynamic value)> Where { get; init; }

	/// <summary>
	/// Additional Where predicates, allowing advanced custom queries (e.g. with OR) -
	/// will be wrapped in brackets to be compatible with AND
	/// </summary>
	/// <remarks>
	/// NB: you are responsible for ensuring that the parameter names don't clash
	/// (avoid using P0/P1/P2 etc, as these are used automatically for clauses added using
	/// <see cref="Where"/>)
	/// </remarks>
	IImmutableList<(string clause, IQueryParametersDictionary parameters)> WhereCustom { get; init; }

	/// <summary>
	/// Sort columns.
	/// </summary>
	IImmutableList<(IColumn column, SortOrder order)> Sort { get; init; }

	/// <summary>
	/// Sort randomly - if true, will take precedence over <see cref="Sort"/>.
	/// </summary>
	bool SortRandom { get; init; }

	/// <summary>
	/// Maximum number of results to return (if null will select all rows) - default is 10.
	/// </summary>
	ulong? Maximum { get; init; }

	/// <summary>
	/// The number of results to skip<br/>
	/// Note: setting this will do nothing if <see cref="Maximum"/> is not also set
	/// </summary>
	ulong Skip { get; init; }
}
