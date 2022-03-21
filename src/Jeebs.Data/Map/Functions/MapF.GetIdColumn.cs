// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using System.Reflection;
using Jeebs.Data.Attributes;
using Jeebs.Id;
using Jeebs.Messages;

namespace Jeebs.Data.Map.Functions;

/// <summary>
/// Mapping Functions
/// </summary>
public static partial class MapF
{
	/// <summary>
	/// Get the ID column
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <param name="columns">List of mapped columns</param>
	public static Maybe<MappedColumn> GetIdColumn<TEntity>(MappedColumnList columns)
		where TEntity : IWithId =>
		F.Some(
			columns
		)
		.Map(
			x => x.Where(p => p.PropertyInfo.Name == nameof(IWithId.Id) && p.PropertyInfo.GetCustomAttribute(typeof(IgnoreAttribute)) is null).ToList(),
			e => new M.ErrorGettingIdPropertyMsg<TEntity>(e)
		)
		.UnwrapSingle<IMappedColumn>(
			noItems: () => new M.NoIdPropertyMsg<TEntity>()
		)
		.Map(
			x => new MappedColumn(x),
			F.DefaultHandler
		);

	public static partial class M
	{
		/// <summary>No Id property found on entity</summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="Value">Exception</param>
		public sealed record class ErrorGettingIdPropertyMsg<TEntity>(Exception Value) : ExceptionMsg;

		/// <summary>No property with specified attribute found on entity</summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		public sealed record class NoIdPropertyMsg<TEntity>() : Msg
		{
			/// <inheritdoc/>
			public override string Format =>
				"Required {Property} or {Attribute} missing on entity {Type}.";

			/// <inheritdoc/>
			public override object[]? Args =>
				new object[] { nameof(IWithId.Id), typeof(IdAttribute), typeof(TEntity) };
		}

		/// <summary>Too many properties with specified attribute found on entity</summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		public sealed record class TooManyPropertiesWithIdAttributeMsg<TEntity>() : Msg
		{
			/// <inheritdoc/>
			public override string Format =>
				"More than one {Attribute} found on entity {Type}.";

			/// <inheritdoc/>
			public override object[]? Args =>
				new object[] { typeof(IdAttribute), typeof(TEntity) };
		}
	}
}
