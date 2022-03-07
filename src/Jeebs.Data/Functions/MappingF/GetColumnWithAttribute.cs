// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using System.Reflection;
using Jeebs;
using Jeebs.Data.Mapping;
using static F.MaybeF;

namespace F.DataF;

/// <summary>
/// Mapping Functions
/// </summary>
public static partial class MappingF
{
	/// <summary>
	/// Get the column with the specified attribute
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TAttribute">Attribute type</typeparam>
	/// <param name="columns">List of mapped columns</param>
	public static Maybe<MappedColumn> GetColumnWithAttribute<TEntity, TAttribute>(MappedColumnList columns)
		where TEntity : IWithId
		where TAttribute : Attribute =>
		Some(
			columns
		)
		.Map(
			x => x.Where(p => p.PropertyInfo.GetCustomAttribute(typeof(TAttribute)) != null).ToList(),
			e => new M.ErrorGettingColumnsWithAttributeMsg<TEntity, TAttribute>(e)
		)
		.UnwrapSingle<IMappedColumn>(
			noItems: () => new M.NoPropertyWithAttributeMsg<TEntity, TAttribute>(),
			tooMany: () => new M.TooManyPropertiesWithAttributeMsg<TEntity, TAttribute>()
		)
		.Map(
			x => new MappedColumn(x),
			DefaultHandler
		);

	public static partial class M
	{
		/// <summary>Something went wrong while getting columns with the specified attribute</summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TAttribute">Attribute type</typeparam>
		/// <param name="Value">Exception object</param>
		public sealed record class ErrorGettingColumnsWithAttributeMsg<TEntity, TAttribute>(Exception Value) : ExceptionMsg;

		/// <summary>No property with specified attribute found on entity</summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TAttribute">Attribute type</typeparam>
		public sealed record class NoPropertyWithAttributeMsg<TEntity, TAttribute>() : Msg
		{
			/// <inheritdoc/>
			public override string Format =>
				"Required {Attribute} missing on entity {Type}.";

			/// <inheritdoc/>
			public override object[]? Args =>
				new object[] { typeof(TAttribute), typeof(TEntity) };
		}

		/// <summary>Too many properties with specified attribute found on entity</summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TAttribute">Attribute type</typeparam>
		public sealed record class TooManyPropertiesWithAttributeMsg<TEntity, TAttribute>() : Msg
		{
			/// <inheritdoc/>
			public override string Format =>
				"More than one {Attribute} found on entity {Type}.";

			/// <inheritdoc/>
			public override object[]? Args =>
				new object[] { typeof(TAttribute), typeof(TEntity) };
		}
	}
}
