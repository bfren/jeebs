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
	/// Get the ID column
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <param name="columns">List of mapped columns</param>
	public static Maybe<Column> GetIdColumn<TTable>(ColumnList columns)
		where TTable : ITable =>
		F.Some(
			columns
		)
		.Map(
			x => x.Where(p => p.PropertyInfo.Name == nameof(IWithId.Id) && p.PropertyInfo.GetCustomAttribute(typeof(IgnoreAttribute)) is null).ToList(),
			e => new M.ErrorGettingIdPropertyMsg<TTable>(e)
		)
		.UnwrapSingle<IColumn>(
			noItems: () => new M.NoIdPropertyMsg<TTable>()
		)
		.Map(
			x => new Column(x),
			F.DefaultHandler
		);

	public static partial class M
	{
		/// <summary>No Id property found on table</summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="Value">Exception</param>
		public sealed record class ErrorGettingIdPropertyMsg<TTable>(Exception Value) : ExceptionMsg;

		/// <summary>No property with specified attribute found on table</summary>
		/// <typeparam name="TTable">Table type</typeparam>
		public sealed record class NoIdPropertyMsg<TTable>() : Msg
		{
			/// <inheritdoc/>
			public override string Format =>
				"Required {Property} or {Attribute} missing on table {Type}.";

			/// <inheritdoc/>
			public override object[]? Args =>
				[nameof(IWithId.Id), typeof(IdAttribute), typeof(TTable)];
		}

		/// <summary>Too many properties with specified attribute found on table</summary>
		/// <typeparam name="TTable">Table type</typeparam>
		public sealed record class TooManyPropertiesWithIdAttributeMsg<TTable>() : Msg
		{
			/// <inheritdoc/>
			public override string Format =>
				"More than one {Attribute} found on table {Type}.";

			/// <inheritdoc/>
			public override object[]? Args =>
				[typeof(IdAttribute), typeof(TTable)];
		}
	}
}
