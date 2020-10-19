using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// IAdapter extensions - queries
	/// </summary>
	public static partial class AdapterExtensions
	{
		/// <summary>
		/// Query to insert a single row and return the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IAdapter</param>
		/// <param name="maps">[Optional] IMapService - if null will use default static instance</param>
		public static string CreateSingleAndReturnId<T>(this IAdapter @this, IMapService? maps = null)
			where T : IEntity
		{
			// Get map and columns
			var map = (maps ?? MapService.Instance).GetTableMapFor<T>();
			(var columns, var aliases) = map.GetWriteableColumnNamesAndAliases();

			// Get SQL from adapter
			return @this.CreateSingleAndReturnId(
				@this.Escape(map.Name),
				Escape(@this, columns),
				aliases
			);
		}

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IAdapter</param>
		/// <param name="maps">[Optional] IMapService - if null will use default static instance</param>
		public static string RetrieveSingleById<T>(this IAdapter @this, IMapService? maps = null)
			where T : IEntity
		{
			// Get map and select list
			var map = (maps ?? MapService.Instance).GetTableMapFor<T>();
			var select = new List<string>();

			// Add each column to the select list
			foreach (var mc in map.Columns)
			{
				select.Add(Escape(@this, mc));
			}

			// Get SQL from adapter
			return @this.RetrieveSingleById(
				@this.Escape(map.Name),
				Escape(@this, map.Columns),
				@this.Escape(map.IdColumn.Name),
				nameof(IEntity.Id)
			);
		}

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IAdapter</param>
		/// <param name="maps">[Optional] IMapService - if null will use default static instance</param>
		public static string UpdateSingle<T>(this IAdapter @this, IMapService? maps = null)
			where T : IEntity
		{
			// Get map and columns
			var map = (maps ?? MapService.Instance).GetTableMapFor<T>();
			(var columns, var aliases) = map.GetWriteableColumnNamesAndAliases();
			var id = map.IdColumn;

			// Get SQL from adapter
			if (map.VersionColumn is MappedColumn v)
			{
				return @this.UpdateSingle(
					@this.Escape(map.Name),
					Escape(@this, columns),
					aliases,
					@this.Escape(id.Name),
					nameof(IEntity.Id),
					@this.Escape(v.Name),
					v.Property.Name
				);
			}
			else
			{
				return @this.UpdateSingle(
					@this.Escape(map.Name),
					Escape(@this, columns),
					aliases,
					@this.Escape(id.Name),
					nameof(IEntity.Id)
				);
			}
		}

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="this">IAdapter</param>
		/// <param name="maps">[Optional] IMapService - if null will use default static instance</param>
		public static string DeleteSingle<T>(this IAdapter @this, IMapService? maps = null)
			where T : IEntity
		{
			// Get map and columns
			var map = (maps ?? MapService.Instance).GetTableMapFor<T>();
			var id = map.IdColumn;

			// Get SQL from adapter
			if (map.VersionColumn is MappedColumn v)
			{
				return @this.DeleteSingle(
					@this.Escape(map.Name),
					@this.Escape(id.Name),
					nameof(IEntity.Id),
					@this.Escape(v.Name),
					v.Property.Name
				);
			}
			else
			{
				return @this.DeleteSingle(
					@this.Escape(map.Name),
					@this.Escape(id.Name),
					nameof(IEntity.Id)
				);
			}
		}
	}
}
