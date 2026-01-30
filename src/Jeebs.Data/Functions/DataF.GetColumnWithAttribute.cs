// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using System.Reflection;

namespace Jeebs.Data;

public static partial class DataF
{
	/// <summary>
	/// Get the column with the specified attribute.
	/// </summary>
	/// <typeparam name="TTable">Table type.</typeparam>
	/// <typeparam name="TAttribute">Attribute type.</typeparam>
	/// <param name="columns">List of mapped columns.</param>
	public static Result<Column> GetColumnWithAttribute<TTable, TAttribute>(ColumnList columns)
		where TTable : ITable
		where TAttribute : Attribute =>
		R.Wrap(
			columns
		)
		.Map(
			x => x.Where(p => p.PropertyInfo.GetCustomAttribute<TAttribute>() != null).ToList(),
			e => R.Fail(e).Msg(
					"Error getting properties with attribute '{Attribute}' from table '{Table}'.",
					typeof(TAttribute).Name, typeof(TTable).Name
				)
				.Ctx(nameof(DataF), nameof(GetColumnWithAttribute))
		)
		.GetSingle(
			x => x.Value<IColumn>(),
			(msg, args) => R.Fail(
					"Unable to get single column with attribute '{Attribute}' from table '{Table}': " + msg,
					[typeof(TAttribute).Name, typeof(TTable).Name, .. args]
				)
				.Ctx(nameof(DataF), nameof(GetColumnWithAttribute))
		)
		.Map(
			x => new Column(x)
		);
}
