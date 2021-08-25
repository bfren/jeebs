// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
		/// Get the ID column
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="columns">List of mapped columns</param>
		public static Option<MappedColumn> GetIdColumn<TEntity>(MappedColumnList columns)
			where TEntity : IWithId =>
			Return(
				columns
			)
			.Map(
				x => x.Where(p => p.Property.Name == nameof(IWithId.Id) && p.Property.GetCustomAttribute(typeof(IgnoreAttribute)) == null).ToList(),
				e => new Msg.ErrorGettingIdPropertyMsg<TEntity>(e)
			)
			.UnwrapSingle<IMappedColumn>(
				noItems: () => new Msg.NoIdPropertyMsg<TEntity>()
			)
			.Map(
				x => new MappedColumn(x),
				DefaultHandler
			);

		public static partial class Msg
		{
			/// <summary>No Id property found on entity</summary>
			public sealed record class ErrorGettingIdPropertyMsg<TEntity>(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>No property with specified attribute found on entity</summary>
			/// <typeparam name="TEntity">Entity type</typeparam>
			public sealed record class NoIdPropertyMsg<TEntity>() : IMsg
			{
				/// <summary>Return message with class type parameters</summary>
				public override string ToString() =>
					$"Required {nameof(IWithId.Id)} or {typeof(IdAttribute)} missing on entity {typeof(TEntity)}.";
			}

			/// <summary>Too many properties with specified attribute found on entity</summary>
			/// <typeparam name="TEntity">Entity type</typeparam>
			public sealed record class TooManyPropertiesWithIdAttributeMsg<TEntity>() : IMsg
			{
				/// <summary>Return message with class type parameters</summary>
				public override string ToString() =>
					$"More than one {typeof(IdAttribute)} found on entity {typeof(TEntity)}.";
			}
		}
	}
}
