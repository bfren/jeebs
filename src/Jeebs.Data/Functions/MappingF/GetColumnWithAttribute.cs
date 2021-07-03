// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Linq;
using System.Reflection;
using Jeebs;
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
		/// Get the column with the specified attribute
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TAttribute">Attribute type</typeparam>
		/// <param name="columns">List of mapped columns</param>
		public static Option<MappedColumn> GetColumnWithAttribute<TEntity, TAttribute>(MappedColumnList columns)
			where TEntity : IWithId
			where TAttribute : Attribute =>
			Return(
				columns
			)
			.Map(
				x => x.Where(p => p.Property.GetCustomAttribute(typeof(TAttribute)) != null).ToList(),
				e => new Msg.ErrorGettingColumnsWithAttributeMsg<TEntity, TAttribute>(e)
			)
			.UnwrapSingle<IMappedColumn>(
				noItems: () => new Msg.NoPropertyWithAttributeMsg<TEntity, TAttribute>(),
				tooMany: () => new Msg.TooManyPropertiesWithAttributeMsg<TEntity, TAttribute>()
			)
			.Map(
				x => new MappedColumn(x),
				DefaultHandler
			);

		public static partial class Msg
		{
			/// <summary>Something went wrong while getting columns with the specified attribute</summary>
			/// <typeparam name="TEntity">Entity type</typeparam>
			/// <typeparam name="TAttribute">Attribute type</typeparam>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingColumnsWithAttributeMsg<TEntity, TAttribute>(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>No property with specified attribute found on entity</summary>
			/// <typeparam name="TEntity">Entity type</typeparam>
			/// <typeparam name="TAttribute">Attribute type</typeparam>
			public sealed record NoPropertyWithAttributeMsg<TEntity, TAttribute>() : IMsg
			{
				/// <summary>Return message with class type parameters</summary>
				public override string ToString() =>
					$"Required {typeof(TAttribute)} missing on entity {typeof(TEntity)}.";
			}

			/// <summary>Too many properties with specified attribute found on entity</summary>
			/// <typeparam name="TEntity">Entity type</typeparam>
			/// <typeparam name="TAttribute">Attribute type</typeparam>
			public sealed record TooManyPropertiesWithAttributeMsg<TEntity, TAttribute>() : IMsg
			{
				/// <summary>Return message with class type parameters</summary>
				public override string ToString() =>
					$"More than one {typeof(TAttribute)} found on entity {typeof(TEntity)}.";
			}
		}
	}
}
