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
/// Mapping Functions
/// </summary>
public static partial class MapF
{
	/// <summary>
	/// Get all columns as <see cref="Column"/> objects
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <param name="table">Table object</param>
	public static Maybe<ColumnList> GetColumns<TEntity>(ITable table)
		where TEntity : IWithId =>
		F.Some(
			table
		)
		.Map(
			x => from tableProperty in x.GetType().GetProperties()
				 join entityProperty in typeof(TEntity).GetProperties() on tableProperty.Name equals entityProperty.Name
				 let column = tableProperty.GetValue(x)?.ToString()
				 where tableProperty.GetCustomAttribute<IgnoreAttribute>() is null
				 select new Column
				 (
					 tblName: x.GetName(),
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
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingColumnsMsg<TEntity>(Exception Value) : ExceptionMsg;
	}
}
