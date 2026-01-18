// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using System.Reflection;
using Jeebs.Data.Attributes;
using Jeebs.Messages;
using StrongId;

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
	public static Maybe<ColumnList> GetColumns<TTable, TEntity>(TTable table)
		where TTable : ITable
		where TEntity : IWithId =>
		F.Some(
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
			e => new M.ErrorGettingColumnsMsg<TEntity>(e)
		)
		.Map(
			x => new ColumnList(x),
			F.DefaultHandler
		);

	public static partial class M
	{
		/// <summary>Messages</summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="Value">Exception object.</param>
		public sealed record class ErrorGettingColumnsMsg<TEntity>(Exception Value) : ExceptionMsg;
	}
}
