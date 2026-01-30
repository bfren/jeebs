// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Reflection;
using Jeebs.Data.Attributes;

namespace Jeebs.Data;

public static partial class DataF
{
	/// <summary>
	/// Get the ID column.
	/// </summary>
	/// <typeparam name="TTable">Table type.</typeparam>
	/// <param name="columns">List of mapped columns.</param>
	public static Result<Column> GetIdColumn<TTable>(ColumnList columns)
		where TTable : ITable =>
		R.Wrap(
			columns
		)
		.Map(
			x => x.Where(p => (p.PropertyInfo.Name == nameof(IWithId.Id) || p.PropertyInfo.GetCustomAttribute<IdAttribute>() is not null) && p.PropertyInfo.GetCustomAttribute<IgnoreAttribute>() is null).ToList(),
			e => R.Fail(e).Msg("Error getting Id properties from table '{Name}'.", typeof(TTable).Name)
				.Ctx(nameof(DataF), nameof(GetIdColumn))
		)
		.GetSingle(
			x => x.Value<IColumn>(),
			(msg, args) => R.Fail("Unable to get Id column from table '{Name}': " + msg, [typeof(TTable).Name, .. args])
				.Ctx(nameof(DataF), nameof(GetIdColumn))
		)
		.Map(
			x => new Column(x)
		);
}
