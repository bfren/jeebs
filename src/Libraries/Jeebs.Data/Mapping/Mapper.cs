// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jeebs.Data.Exceptions;
using Jeebs.Data.Mapping.Exceptions;
using Jx.Data.Mapping;
using static F.OptionF;

namespace Jeebs.Data
{
	/// <inheritdoc/>
	internal sealed class Mapper : IDisposable
	{
		#region Static

		/// <summary>
		/// Default (global) instance
		/// </summary>
		internal static Mapper Instance =>
			instance.Value;

		/// <summary>
		/// Lazily create a <see cref="Mapper"/>
		/// </summary>
		private static readonly Lazy<Mapper> instance = new(() => new Mapper(), true);

		#endregion

		/// <summary>
		/// Mapped entities
		/// </summary>
		private readonly ConcurrentDictionary<Type, TableMap> mappedEntities = new();

		/// <summary>
		/// Only allow internal creation
		/// </summary>
		internal Mapper() { }

		/// <summary>
		/// FOR TESTING
		/// Map the specified <typeparamref name="TEntity"/> to the specified <typeparamref name="TTable"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TTable">Table type</typeparam>
		internal ITableMap Map<TEntity, TTable>()
			where TEntity : IEntity
			where TTable : Table, new() =>
			Map<TEntity>(new TTable());

		/// <inheritdoc/>
		internal ITableMap Map<TEntity>(ITable table)
			where TEntity : IEntity =>
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
					() => throw new UnableToGetMappedColumnsException<TEntity>()
				);

				// Get ID property
				var idProperty = GetColumnWithAttribute<TEntity, IdAttribute>(columns).Unwrap(
					() => throw new UnableToFindIdColumnException<TEntity>()
				);

				// Create Table Map
				var map = new TableMap(table, columns, idProperty);

				// Get Version property
				if (typeof(TEntity).Implements<IEntityWithVersion>())
				{
					map.VersionColumn = GetColumnWithAttribute<TEntity, VersionAttribute>(columns).Unwrap(
						() => throw new UnableToFindVersionColumnException<TEntity>()
					);
				}

				// Return map
				return map;
			});

		/// <inheritdoc/>
		internal static (bool valid, string errors) ValidateTable<TEntity>(ITable table)
			where TEntity : IEntity
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
				return (false, string.Join("\n", errors));
			}

			// Return valid with no errors
			return (true, string.Empty);
		}

		/// <inheritdoc/>
		internal static Option<MappedColumnList> GetMappedColumns<TEntity>(ITable table)
			where TEntity : IEntity =>
			Return(
				table
			)
			.Map(
				x => from column in x.GetType().GetProperties()
					 let columnName = column.GetValue(x)?.ToString()
					 join property in typeof(TEntity).GetProperties() on column.Name equals property.Name
					 where property.GetCustomAttribute<IgnoreAttribute>() == null
					 select new MappedColumn
					 (
						 Table: x.TableName,
						 Name: columnName,
						 Property: property
					 ),
				e => new Msg.ErrorGettingMappedColumnsMsg<TEntity>(e)
			)
			.Map(
				x => new MappedColumnList(x),
				DefaultHandler
			);

		/// <inheritdoc/>
		internal static Option<MappedColumn> GetColumnWithAttribute<TEntity, TAttribute>(MappedColumnList columns)
			where TEntity : IEntity
			where TAttribute : Attribute =>
			Return(
				columns
			)
			.Map(
				x => x.Where(p => p.Property.GetCustomAttribute(typeof(TAttribute)) != null),
				e => new Msg.ErrorGettingColumnsWithAttributeMsg<TEntity, TAttribute>(e)
			)
			.UnwrapSingle<MappedColumn>(
				noItems: () => new Msg.NoPropertyWithAttributeMsg<TEntity, TAttribute>(),
				tooMany: () => new Msg.TooManyPropertiesWithAttributeMsg<TEntity, TAttribute>()
			);

		/// <inheritdoc/>
		internal Option<ITableMap> GetTableMapFor<TEntity>()
			where TEntity : IEntity
		{
			if (mappedEntities.TryGetValue(typeof(TEntity), out var map))
			{
				return map;
			}

			return None<ITableMap, Msg.TryingToGetUnmappedEntityMsg<TEntity>>();
		}

		#region Dispose

		/// <summary>
		/// Set to true if the object has been disposed
		/// </summary>
		private bool disposed = false;

		/// <summary>
		/// Suppress garbage collection and call <see cref="Dispose(bool)"/>
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
			public sealed record NoPropertyWithAttributeMsg<TEntity, TAttribute>() : IMsg { }

			/// <summary>Too many properties with specified attribute found on entity</summary>
			/// <typeparam name="TEntity">Entity type</typeparam>
			/// <typeparam name="TAttribute">Attribute type</typeparam>
			public sealed record TooManyPropertiesWithAttributeMsg<TEntity, TAttribute>() : IMsg { }

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
