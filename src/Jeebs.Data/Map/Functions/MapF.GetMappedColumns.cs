// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Reflection;
using Jeebs.Data.Attributes;

namespace Jeebs.Data.Map.Functions;

/// <summary>
/// Mapping Functions.
/// </summary>
public static partial class MapF
{
	/// <summary>
	/// Get all columns as <see cref="Column"/> objects.
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <param name="table">Table object.</param>
	public static Result<ColumnList> GetColumns<TTable, TEntity>(TTable table)
		where TTable : ITable
		where TEntity : IWithId =>
		R.Try(
			() => from tableProperty in typeof(TTable).GetProperties()
				  join entityProperty in typeof(TEntity).GetProperties() on tableProperty.Name equals entityProperty.Name
				  let column = tableProperty.GetValue(table)?.ToString()
				  where tableProperty.GetCustomAttribute<IgnoreAttribute>() is null
				  select new Column
				  (
					  tblName: table.GetName(),
					  colName: column,
					  propertyInfo: tableProperty
				  ),
			e => R.Fail(nameof(MapF), nameof(GetColumns),
				e, "Error getting columns from table '{Table}'.", table
			)
		)
		.Map(
			x => new ColumnList(x)
		);
}
