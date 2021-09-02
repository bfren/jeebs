// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Mapping;
using static F.DataF.QueryF;
using static F.OptionF;
using Msg = Jeebs.Data.ExtractMsg;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IExtract"/>
	public sealed class Extract : IExtract
	{
		/// <inheritdoc/>
		public IColumnList From<TModel>(params ITable[] tables) =>
			Extract<TModel>.From(tables).Unwrap(() => new ColumnList());
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
		public static Option<IColumnList> From(params ITable[] tables)
		{
			// If no tables, return empty extracted list
			if (tables.Length == 0)
			{
				return new ColumnList();
			}

			// Extract distinct columns
			return
				Some(
					() =>
					{
						return from table in tables
							   from column in GetColumnsFromTable<TModel>(table)
							   select column;
					},
					e => new Msg.ErrorExtractingColumnsFromTableExceptionMsg(e)
				)
				.SwitchIf(
					x => x.Any(),
					_ => new Msg.NoColumnsExtractedFromTableMsg()
				)
				.Map(
					x => x.Distinct(new Column.AliasComparer()),
					e => new Msg.ErrorExtractingDistinctColumnsExceptionMsg(e)
				)
				.Map(
					x => (IColumnList)new ColumnList(x),
					DefaultHandler
				);
		}
	}

	/// <summary>Messages</summary>
	public static class ExtractMsg
	{
		/// <summary>An error occurred extracting columns from a table</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record class ErrorExtractingColumnsFromTableExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

		/// <summary>An error occurred getting distinct columns</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record class ErrorExtractingDistinctColumnsExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

		/// <summary>No matching columns were extracted from the table</summary>
		public sealed record class NoColumnsExtractedFromTableMsg : IMsg { }
	}
}
