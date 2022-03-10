// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using System.Reflection;
using Jeebs.Data.Attributes;
using Jeebs.Messages;
using Jeebs.Id;
using Maybe;
using Maybe.Functions;

namespace Jeebs.Data.Map.Functions;

/// <summary>
/// Mapping Functions
/// </summary>
public static partial class MapF
{
	/// <summary>
	/// Get all columns as <see cref="MappedColumn"/> objects
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <param name="table">Table object</param>
	public static Maybe<MappedColumnList> GetMappedColumns<TEntity>(ITable table)
		where TEntity : IWithId =>
		MaybeF.Some(
			table
		)
		.Map(
			x => from tableProperty in x.GetType().GetProperties()
				 let column = tableProperty.GetValue(x)?.ToString()
				 join entityProperty in typeof(TEntity).GetProperties() on tableProperty.Name equals entityProperty.Name
				 where entityProperty.GetCustomAttribute<IgnoreAttribute>() is null
				 select new MappedColumn
				 (
					 Table: x.GetName(),
					 Name: column,
					 PropertyInfo: entityProperty
				 ),
			e => new M.ErrorGettingMappedColumnsMsg<TEntity>(e)
		)
		.Map(
			x => new MappedColumnList(x),
			MaybeF.DefaultHandler
		);

	public static partial class M
	{
		/// <summary>Messages</summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingMappedColumnsMsg<TEntity>(Exception Value) : ExceptionMsg;
	}
}
