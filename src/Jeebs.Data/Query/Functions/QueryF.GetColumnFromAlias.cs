// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using Jeebs.Data.Map;
using Jeebs.Messages;
using Jeebs.Reflection;

namespace Jeebs.Data.Query.Functions;

public static partial class QueryF
{
	/// <summary>
	/// Build a column object from a column alias.
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <param name="table">Table object</param>
	/// <param name="columnAlias">Column alias</param>
	public static Maybe<IColumn> GetColumnFromAlias<TTable>(TTable table, string columnAlias)
		where TTable : ITable =>
		table.GetProperties()
			.SingleOrNone(
				x => x.Name == columnAlias
			)
			.Map(
				x => (name: x.GetValue(table)?.ToString()!, prop: x),
				F.DefaultHandler
			)
			.SwitchIf(
				x => string.IsNullOrEmpty(x.name),
				ifTrue: _ => F.None<(string, PropertyInfo)>(new M.UnableToGetColumnFromAliasMsg(table, columnAlias))
			)
			.Map<IColumn>(
				x => new Column(table.GetName(), x.name, x.prop),
				F.DefaultHandler
			);

	/// <inheritdoc cref="GetColumnFromAlias{TTable}(TTable, string)"/>
	public static Maybe<IColumn> GetColumnFromAlias<TTable>(string columnAlias)
		where TTable : ITable, new() =>
		GetColumnFromAlias(
			new TTable(), columnAlias
		);

	public static partial class M
	{
		/// <summary>Unable to get column name using the alias</summary>
		/// <param name="Table">Table object</param>
		/// <param name="ColumnAlias">Column alias</param>
		public sealed record class UnableToGetColumnFromAliasMsg(ITable Table, string ColumnAlias) : Msg;
	}
}
