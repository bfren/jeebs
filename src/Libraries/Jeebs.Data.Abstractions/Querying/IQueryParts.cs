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
		ITable From { get; }

		/// <summary>
		/// Select columns (if null will select all columns)
		/// </summary>
		List<IColumn> Select { get; }

		/// <summary>
		/// Inner Joins
		/// </summary>
		List<(IColumn on, IColumn equals)> InnerJoin { get; }

		/// <summary>
		/// Left Joins
		/// </summary>
		List<(IColumn on, IColumn equals)> LeftJoin { get; }

		/// <summary>
		/// Right Joins
		/// </summary>
		List<(IColumn on, IColumn equals)> RightJoin { get; }

		/// <summary>
		/// Where Predicates
		/// </summary>
		List<(IColumn column, SearchOperator op, object value)> Where { get; }

		/// <summary>
		/// Query Parameters
		/// </summary>
		IQueryParameters Parameters { get; }

		/// <summary>
		/// Order By columns
		/// </summary>
		List<(IColumn column, SortOrder order)> OrderBy { get; }

		/// <summary>
		/// Maximum number of results to return
		/// </summary>
		long Maximum { get; }

		/// <summary>
		/// The number of results to skip
		/// </summary>
		long Skip { get; }
	}
}
