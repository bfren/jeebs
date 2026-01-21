// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using System.Reflection;

namespace Jeebs.Data.Map.Functions;

/// <summary>
/// Mapping Functions.
/// </summary>
public static partial class MapF
{
	/// <summary>
	/// Get the column with the specified attribute.
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <typeparam name="TAttribute">Attribute type</typeparam>
	/// <param name="columns">List of mapped columns.</param>
	public static Result<Column> GetColumnWithAttribute<TTable, TAttribute>(ColumnList columns)
		where TTable : ITable
		where TAttribute : Attribute =>
		R.Wrap(
			columns
		)
		.Map(
			x => x.Where(p => p.PropertyInfo.GetCustomAttribute<TAttribute>() != null).ToList(),
			e => R.Fail(nameof(MapF), nameof(GetColumnWithAttribute),
				e, "Error getting properties with attribute '{Attribute}' from table '{Table}'.", typeof(TAttribute).Name, typeof(TTable).Name
			)
		)
		.GetSingle(
			x => x.Value<IColumn>(),
			() => R.Fail(nameof(MapF), nameof(GetColumnWithAttribute),
				"Unable to get single column with attribute '{Attribute}' from table '{Table}'.", typeof(TAttribute).Name, typeof(TTable).Name
			)
		)
		.Map(
			x => new Column(x)
		);
}
