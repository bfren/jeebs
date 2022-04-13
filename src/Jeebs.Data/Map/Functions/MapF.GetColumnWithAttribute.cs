// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using System.Reflection;
using Jeebs.Messages;

namespace Jeebs.Data.Map.Functions;

/// <summary>
/// Mapping Functions
/// </summary>
public static partial class MapF
{
	/// <summary>
	/// Get the column with the specified attribute
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <typeparam name="TAttribute">Attribute type</typeparam>
	/// <param name="columns">List of mapped columns</param>
	public static Maybe<Column> GetColumnWithAttribute<TTable, TAttribute>(ColumnList columns)
		where TTable : ITable
		where TAttribute : Attribute =>
		F.Some(
			columns
		)
		.Map(
			x => x.Where(p => p.PropertyInfo.GetCustomAttribute(typeof(TAttribute)) != null).ToList(),
			e => new M.ErrorGettingColumnsWithAttributeMsg<TTable, TAttribute>(e)
		)
		.UnwrapSingle<IColumn>(
			noItems: () => new M.NoPropertyWithAttributeMsg<TTable, TAttribute>(),
			tooMany: () => new M.TooManyPropertiesWithAttributeMsg<TTable, TAttribute>()
		)
		.Map(
			x => new Column(x),
			F.DefaultHandler
		);

	public static partial class M
	{
		/// <summary>Something went wrong while getting columns with the specified attribute</summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TAttribute">Attribute type</typeparam>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingColumnsWithAttributeMsg<TEntity, TAttribute>(Exception Value) : ExceptionMsg;

		/// <summary>No property with specified attribute found on entity</summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <typeparam name="TAttribute">Attribute type</typeparam>
		public sealed record class NoPropertyWithAttributeMsg<TTable, TAttribute>() : Msg
		{
			/// <inheritdoc/>
			public override string Format =>
				"Required {Attribute} missing on table {Type}.";

			/// <inheritdoc/>
			public override object[]? Args =>
				new object[] { typeof(TAttribute), typeof(TTable) };
		}

		/// <summary>Too many properties with specified attribute found on table</summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <typeparam name="TAttribute">Attribute type</typeparam>
		public sealed record class TooManyPropertiesWithAttributeMsg<TTable, TAttribute>() : Msg
		{
			/// <inheritdoc/>
			public override string Format =>
				"More than one {Attribute} found on table {Type}.";

			/// <inheritdoc/>
			public override object[]? Args =>
				new object[] { typeof(TAttribute), typeof(TTable) };
		}
	}
}
