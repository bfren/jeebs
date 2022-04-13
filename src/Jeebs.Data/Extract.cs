// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using Jeebs.Data.Map;
using Jeebs.Data.Query.Functions;
using Jeebs.Messages;

namespace Jeebs.Data;

/// <inheritdoc cref="IExtract"/>
public sealed class Extract : IExtract
{
	/// <inheritdoc/>
	public IColumnList From<TModel>(params ITable[] tables) =>
		Extract<TModel>.From(tables).Unwrap((Func<IColumnList>)(() => new ColumnList()));

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>An error occurred extracting columns from a table</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorExtractingColumnsFromTableExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>An error occurred getting distinct columns</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorExtractingDistinctColumnsExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>No matching columns were extracted from the table</summary>
		public sealed record class NoColumnsExtractedFromTableMsg : Msg;
	}
}

/// <summary>
/// Extract columns from a table that match <typeparamref name="TModel"/>
/// </summary>
/// <typeparam name="TModel">Model type</typeparam>
public static class Extract<TModel>
{
	/// <summary>
	/// Extract columns from specified tables
	/// </summary>
	/// <param name="tables">List of tables</param>
#pragma warning disable CA1000 // Do not declare static members on generic types
	public static Maybe<IColumnList> From(params ITable[] tables)
#pragma warning restore CA1000 // Do not declare static members on generic types
	{
		// If no tables, return empty extracted list
		if (tables.Length == 0)
		{
			return new ColumnList();
		}

		// Extract distinct columns
		return
			F.Some(
				() => from table in tables
					  from column in QueryF.GetColumnsFromTable<TModel>(table)
					  select column,
				e => new Extract.M.ErrorExtractingColumnsFromTableExceptionMsg(e)
			)
			.SwitchIf(
				x => x.Any(),
				_ => new Extract.M.NoColumnsExtractedFromTableMsg()
			)
			.Map(
				x => x.Distinct(new Column.AliasComparer()),
				e => new Extract.M.ErrorExtractingDistinctColumnsExceptionMsg(e)
			)
			.Map(
				x => (IColumnList)new ColumnList(x),
				F.DefaultHandler
			);
	}
}
