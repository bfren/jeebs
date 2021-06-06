// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs.WordPress.Data.Mapping
{
	/// <summary>
	/// Provides services for <see cref="Map{TEntity}"/> to enable better testing
	/// </summary>
	public interface IMapService
	{
		/// <summary>
		/// Map the specified <typeparamref name="TEntity"/> to the specified <paramref name="table"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="table">Table to map</param>
		ITableMap Map<TEntity>(ITable table)
			where TEntity : IEntity;

		/// <summary>
		/// Validate a table's columns against the entity's properties
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="table">Table to validate</param>
		(bool valid, string errors) ValidateTable<TEntity>(ITable table)
			where TEntity : IEntity;

		/// <summary>
		/// Get mapped columns from the specified <typeparamref name="TEntity"/> and <paramref name="table"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="table">Table</param>
		IMappedColumnList GetMappedColumns<TEntity>(ITable table)
		   where TEntity : IEntity;

		/// <summary>
		/// Returns the column in the list marked with the specified attribute
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TAttribute">Attribute type</typeparam>
		/// <param name="columns">IMappedColumnList</param>
		IMappedColumn GetColumnWithAttribute<TEntity, TAttribute>(IMappedColumnList columns)
			where TEntity : IEntity
			where TAttribute : Attribute;

		/// <summary>
		/// Get table map for <typeparamref name="TEntity"/>
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		ITableMap GetTableMapFor<TEntity>()
			where TEntity : IEntity;
	}
}
