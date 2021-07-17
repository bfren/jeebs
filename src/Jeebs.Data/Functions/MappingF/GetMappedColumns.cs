// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Linq;
using System.Reflection;
using Jeebs;
using Jeebs.Data.Entities;
using Jeebs.Data.Mapping;
using static F.OptionF;

namespace F.DataF
{
	/// <summary>
	/// Mapping Functions
	/// </summary>
	public static partial class MappingF
	{
		/// <summary>
		/// Get all columns as <see cref="MappedColumn"/> objects
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="table">Table object</param>
		public static Option<MappedColumnList> GetMappedColumns<TEntity>(ITable table)
			where TEntity : IWithId =>
			Return(
				table
			)
			.Map(
				x => from tableProperty in x.GetType().GetProperties()
					 let column = tableProperty.GetValue(x)?.ToString()
					 join entityProperty in typeof(TEntity).GetProperties() on tableProperty.Name equals entityProperty.Name
					 where entityProperty.GetCustomAttribute<IgnoreAttribute>() == null
					 select new MappedColumn
					 (
						 Table: x.GetName(),
						 Name: column,
						 Property: entityProperty
					 ),
				e => new Msg.ErrorGettingMappedColumnsMsg<TEntity>(e)
			)
			.Map(
				x => new MappedColumnList(x),
				DefaultHandler
			);

		public static partial class Msg
		{
			/// <summary>Messages</summary>
			/// <typeparam name="TEntity">Entity type</typeparam>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingMappedColumnsMsg<TEntity>(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
