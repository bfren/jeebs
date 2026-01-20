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
	/// Get the ID column.
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <param name="columns">List of mapped columns.</param>
	public static Result<Column> GetIdColumn<TTable>(ColumnList columns)
		where TTable : ITable =>
		R.Wrap(
			columns
		)
		.Map(
			x => x.Where(p => p.PropertyInfo.Name == nameof(IWithId.Id) && p.PropertyInfo.GetCustomAttribute<IgnoreAttribute>() is null).ToList(),
			e => R.Fail(nameof(MapF), nameof(GetIdColumn),
				e, "Unable to get Id column from table '{Table}'.", typeof(TTable).Name
			)
		)
		.GetSingle(
			x => x.Value<IColumn>()
		)
		.Map(
			x => new Column(x)
		);
}
