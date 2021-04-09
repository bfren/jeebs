// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs.Data.Mapping;
using static F.DataF.QueryF;
using static F.OptionF;
using Msg = Jeebs.Data.ExtractMsg;

namespace Jeebs.Data
{
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
		public static Option<ColumnList> From(params ITable[] tables)
		{
			// If no tables, return empty extracted list
			if (tables.Length == 0)
			{
				return new ColumnList();
			}

			// Extract distinct columns
			return
				Return(
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
					_ => None<IEnumerable<IColumn>, Msg.NoColumnsExtractedFromTableMsg>()
				)
				.Map(
					x => x.Distinct(new Column.AliasComparer()),
					e => new Msg.ErrorExtractingDistinctColumnsExceptionMsg(e)
				)
				.Map(
					x => new ColumnList(x),
					DefaultHandler
				);
		}
	}

	/// <summary>Messages</summary>
	public static class ExtractMsg
	{
		/// <summary>An error occurred extracting columns from a table</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record ErrorExtractingColumnsFromTableExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

		/// <summary>An error occurred getting distinct columns</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record ErrorExtractingDistinctColumnsExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

		/// <summary>No matching columns were extracted from the table</summary>
		public sealed record NoColumnsExtractedFromTableMsg : IMsg { }
	}
}
