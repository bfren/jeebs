// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Reflection;
using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace Jeebs.Data;

public static partial class DataF
{
	/// <summary>
	/// Get all columns as <see cref="Column"/> objects.
	/// </summary>
	/// <typeparam name="TTable">Table type.</typeparam>
	/// <typeparam name="TEntity">Entity type.</typeparam>
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
			e => R.Fail(e).Msg("Error getting columns from table '{Table}'.", table)
				.Ctx(nameof(DataF), nameof(GetColumns)
			)
		)
		.Map(
			x => new ColumnList(x)
		);
}
