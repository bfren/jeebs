// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jeebs.Data.Entities;
using Jeebs.Data.Exceptions;
using static F.OptionF;

namespace Jeebs.Data.Mapping
{
	/// <inheritdoc cref="IMapper"/>
	internal sealed class Mapper : IMapper, IDisposable
	{
		#region Static

		/// <summary>
		/// Default (global) instance
		/// </summary>
		internal static IMapper Instance =>
			instance.Value;

		/// <summary>
		/// Lazily create a <see cref="Mapper"/>
		/// </summary>
		private static readonly Lazy<IMapper> instance = new(() => new Mapper(), true);

		#endregion

		/// <summary>
		/// Mapped entities
		/// </summary>
		private readonly ConcurrentDictionary<Type, TableMap> mappedEntities = new();

		/// <summary>
		/// Only allow internal creation
		/// </summary>
		internal Mapper() { }

		/// <inheritdoc/>
		public ITableMap Map<TEntity>(ITable table)
			where TEntity : IWithId =>
			mappedEntities.GetOrAdd(typeof(TEntity), _ =>
			{
				// Validate table
				var (valid, errors) = ValidateTable<TEntity>(table);
				if (!valid)
				{
					throw new InvalidTableMapException(errors);
				}

				// Get mapped columns
				var columns = GetMappedColumns<TEntity>(table).Unwrap(
					reason => throw new UnableToGetMappedColumnsException(reason)
				);

				// Get ID property
				var idProperty = GetColumnWithAttribute<TEntity, IdAttribute>(columns).Unwrap(
					reason => throw new UnableToFindIdColumnException(reason)
				) with
				{ Alias = nameof(IWithId.Id) };

				// Create Table Map
				var map = new TableMap(table, columns, idProperty);

				// Get Version property
				if (typeof(TEntity).Implements<IWithVersion>())
				{
					map.VersionColumn = GetColumnWithAttribute<TEntity, VersionAttribute>(columns).Unwrap(
						reason => throw new UnableToFindVersionColumnException(reason)
					);
				}

				// Return map
				return map;
			});

		/// <inheritdoc/>
		public Option<ITableMap> GetTableMapFor<TEntity>()
			where TEntity : IWithId
		{
			if (mappedEntities.TryGetValue(typeof(TEntity), out var map))
			{
				return map;
			}

			return None<ITableMap, Msg.TryingToGetUnmappedEntityMsg<TEntity>>();
		}

		/// <summary>
		/// Validate that the properties on the entity and the columns on the table match
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="table">Table object</param>
		internal static (bool valid, List<string> errors) ValidateTable<TEntity>(ITable table)
			where TEntity : IWithId
		{
			// Get types
			var tableType = table.GetType();
			var entityType = typeof(TEntity);

			// Get the table field names
			var tablePropertyNames = from p in tableType.GetProperties()
									 where p.PropertyType.IsPublic
									 select p.Name;

			// Get the entity property names
			var entityPropertyNames = from p in entityType.GetProperties()
									  where p.GetCustomAttribute<IgnoreAttribute>() == null
									  select p.Name;

			// Compare the table columns with the entity properties
			var errors = new List<string>();

			// Check for missing table columns
			var missingTableFields = entityPropertyNames.Except(tablePropertyNames);
			if (missingTableFields.Any())
			{
				foreach (var field in missingTableFields)
				{
					errors.Add($"The definition of table '{tableType.FullName}' is missing field '{field}'.");
				}
			}

			// Check for missing entity properties
			var missingEntityProperties = tablePropertyNames.Except(entityPropertyNames);
			if (missingEntityProperties.Any())
			{
				foreach (var property in missingEntityProperties)
				{
					errors.Add($"The definition of entity '{entityType.FullName}' is missing property '{property}'.");
				}
			}

			// If there are any errors, return false and list errors on new lines
			if (errors.Count > 0)
			{
				return (false, errors);
			}

			// Return valid with no errors
			return (true, new());
		}

		/// <summary>
		/// Get all columns as <see cref="MappedColumn"/> objects
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="table">Table object</param>
		internal static Option<MappedColumnList> GetMappedColumns<TEntity>(ITable table)
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

		/// <summary>
		/// Get the column with the specified attribute
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TAttribute">Attribute type</typeparam>
		/// <param name="columns">List of mapped columns</param>
		internal static Option<MappedColumn> GetColumnWithAttribute<TEntity, TAttribute>(MappedColumnList columns)
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

		#region Dispose

		/// <summary>
		/// Set to true if the object has been disposed
		/// </summary>
		private bool disposed;

		/// <summary>
		/// Suppress garbage collection and clear mapped entities
		/// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
		/// </summary>
		public void Dispose()
		{
			if (disposed)
			{
				return;
			}

			mappedEntities.Clear();
			disposed = true;

			GC.SuppressFinalize(this);
		}

		#endregion

		/// <summary>Messages</summary>
		public static class Msg
		{
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

			/// <summary>The entity being requested has not been mapped yet</summary>
			/// <typeparam name="TEntity">Entity type</typeparam>
			public sealed record TryingToGetUnmappedEntityMsg<TEntity> : IMsg { }

			/// <summary>Something went wrong while getting columns with the specified attribute</summary>
			/// <typeparam name="TEntity">Entity type</typeparam>
			/// <typeparam name="TAttribute">Attribute type</typeparam>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingColumnsWithAttributeMsg<TEntity, TAttribute>(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Messages</summary>
			/// <typeparam name="TEntity">Entity type</typeparam>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingMappedColumnsMsg<TEntity>(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
