// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	/// <summary>
	/// Contains all the parts necessary to build a query
	/// </summary>
	public interface IQueryParts
	{
		/// <summary>
		/// From table
		/// </summary>
		ITable From { get; init; }

		/// <summary>
		/// Select columns (if empty will select all columns)
		/// </summary>
		List<IColumn> Select { get; init; }

		/// <summary>
		/// Inner Joins
		/// </summary>
		List<(IColumn from, IColumn to)> InnerJoin { get; init; }

		/// <summary>
		/// Left Joins
		/// </summary>
		List<(IColumn from, IColumn to)> LeftJoin { get; init; }

		/// <summary>
		/// Right Joins
		/// </summary>
		List<(IColumn from, IColumn to)> RightJoin { get; init; }

		/// <summary>
		/// Where Predicates
		/// </summary>
		List<(IColumn column, SearchOperator op, object value)> Where { get; init; }

		/// <summary>
		/// Sort columns
		/// </summary>
		List<(IColumn column, SortOrder order)> Sort { get; init; }

		/// <summary>
		/// Maximum number of results to return (if null will select all rows)
		/// </summary>
		long? Maximum { get; init; }

		/// <summary>
		/// The number of results to skip<br/>
		/// Note: setting this will do nothing if <see cref="Maximum"/> is not also set
		/// </summary>
		long Skip { get; init; }
	}
}
