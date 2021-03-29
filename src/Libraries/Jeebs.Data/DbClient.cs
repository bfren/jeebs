// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying;
using Jeebs.Linq;
using static F.DataF.QueryF;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IDbClient"/>
	public abstract class DbClient : IDbClient
	{
		/// <summary>
		/// IMapper
		/// </summary>
		protected IMapper Mapper { get; private init; }

		internal IMapper MapperTest =>
			Mapper;

		/// <summary>
		/// Create using default Mapper instance
		/// </summary>
		protected DbClient() : this(Data.Mapper.Instance) { }

		/// <summary>
		/// Inject a Mapper instance
		/// </summary>
		/// <param name="mapper">IMapper</param>
		protected DbClient(IMapper mapper) =>
			Mapper = mapper;

		/// <inheritdoc/>
		public abstract IDbConnection Connect(string connectionString);

		#region Escaping and Joining

		/// <inheritdoc/>
		public abstract string Escape(IColumn column, bool withAlias = false);

		/// <inheritdoc/>
		public abstract string EscapeWithTable(IColumn column, bool withAlias = false);

		/// <inheritdoc/>
		public abstract string Escape(ITable table);

		/// <inheritdoc/>
		public abstract string Escape(string columnOrTable);

		/// <inheritdoc/>
		public abstract string Escape(string column, string table);

		/// <inheritdoc/>
		public abstract string GetOperator(SearchOperator op);

		/// <inheritdoc/>
		public abstract string GetParamRef(string paramName);

		/// <inheritdoc/>
		public abstract string JoinList(List<string> objects, bool wrap);

		#endregion

		#region Custom Queries

		/// <summary>
		/// Return a query to retrieve a list of entities that match all the specified parameters
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to select</param>
		/// <param name="predicates">Predicates (matched using AND)</param>
		protected abstract (string query, IQueryParameters param) GetQuery(
			string table,
			ColumnList columns,
			List<(IColumn column, SearchOperator op, object value)> predicates
		);

		/// <inheritdoc/>
		public Option<(string query, IQueryParameters param)> GetQuery<TEntity, TModel>(
			(Expression<Func<TEntity, object>>, SearchOperator, object)[] predicates
		)
			where TEntity : IEntity =>
			(
				from map in Mapper.GetTableMapFor<TEntity>()
				from sel in Extract<TModel>.From(map.Table)
				from whr in ConvertPredicatesToColumns(map.Columns, predicates)
				select (map, sel, whr)
			)
			.Map(
				x => GetQuery(x.map.Name, x.sel, x.whr),
				e => new Msg.ErrorGettingGeneralRetrieveQueryExceptionMsg(e)
			);

		/// <inheritdoc/>
		public (string query, IQueryParameters param) GetCountQuery(IQueryParts parts) =>
			GetQuery(new QueryParts(parts) with { Select = new() });

		/// <inheritdoc/>
		public abstract (string query, IQueryParameters param) GetQuery(IQueryParts parts);

		#endregion

		#region CRUD Queries

		/// <inheritdoc/>
		public Option<string> GetCreateQuery<TEntity>()
			where TEntity : IEntity =>
			Mapper.GetTableMapFor<TEntity>().Map(
				x => GetCreateQuery(x.Name, x.Columns),
				e => new Msg.ErrorGettingCrudCreateQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetCreateQuery{TEntity}"/>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of mapped columns</param>
		protected abstract string GetCreateQuery(
			string table,
			IMappedColumnList columns
		);

		/// <inheritdoc/>
		public Option<string> GetRetrieveQuery<TEntity, TModel>(long id)
			where TEntity : IEntity =>
			(
				from map in Mapper.GetTableMapFor<TEntity>()
				from columns in Extract<TModel>.From(map.Table)
				select (map, columns)
			)
			.Map(
				x => GetRetrieveQuery(x.map.Name, x.columns, x.map.IdColumn, id),
				e => new Msg.ErrorGettingCrudRetrieveQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetRetrieveQuery{TEntity, TModel}(long)"/>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to select</param>
		/// <param name="idColumn">ID column for predicate</param>
		/// <param name="id">Entity ID</param>
		protected abstract string GetRetrieveQuery(
			string table,
			ColumnList columns,
			IColumn idColumn,
			long id
		);

		/// <inheritdoc/>
		public Option<string> GetUpdateQuery<TEntity, TModel>(long id)
			where TEntity : IEntity =>
			(
				from map in Mapper.GetTableMapFor<TEntity>()
				from columns in Extract<TModel>.From(map.Table)
				select (map, columns)
			)
			.Map(
				x => typeof(TEntity).Implements<IEntityWithVersion>() switch
				{
					false =>
						GetUpdateQuery(x.map.Name, x.columns, x.map.IdColumn, id),

					true =>
						GetUpdateQuery(x.map.Name, x.columns, x.map.IdColumn, id, x.map.VersionColumn),

				},
				e => new Msg.ErrorGettingCrudUpdateQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetUpdateQuery(string, ColumnList, IColumn, long, IColumn)"/>
		protected abstract string GetUpdateQuery(
			string table,
			ColumnList columns,
			IColumn idColumn,
			long id
		);

		/// <inheritdoc cref="GetUpdateQuery{TEntity, TModel}"/>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to update</param>
		/// <param name="idColumn">ID column for predicate</param>
		/// <param name="id">Entity ID</param>
		/// <param name="versionColumn">Version column for predicate</param>
		protected abstract string GetUpdateQuery(
			string table,
			ColumnList columns,
			IColumn idColumn,
			long id,
			IColumn? versionColumn
		);

		/// <inheritdoc/>
		public Option<string> GetDeleteQuery<TEntity>(long id)
			where TEntity : IEntity =>
			Mapper.GetTableMapFor<TEntity>().Map(
				x => typeof(TEntity).Implements<IEntityWithVersion>() switch
				{
					false =>
						GetDeleteQuery(x.Name, x.IdColumn, id),

					true =>
						GetDeleteQuery(x.Name, x.IdColumn, id, x.VersionColumn),

				},
				e => new Msg.ErrorGettingCrudDeleteQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetDeleteQuery(string, IColumn, long, IColumn?)"/>
		protected abstract string GetDeleteQuery(
			string table,
			IColumn idColumn,
			long id
		);

		/// <inheritdoc cref="GetDeleteQuery{TEntity}"/>
		/// <param name="table">Table name</param>
		/// <param name="idColumn">ID column for predicate</param>
		/// <param name="id">Entity ID</param>
		/// <param name="versionColumn">Version column for predicate</param>
		protected abstract string GetDeleteQuery(
			string table,
			IColumn idColumn,
			long id,
			IColumn? versionColumn
		);

		#endregion

		#region Testing

		internal (string query, IQueryParameters param) GetQueryTest(
			string table,
			ColumnList columns,
			List<(IColumn column, SearchOperator op, object value)> predicates
		) =>
			GetQuery(table, columns, predicates);

		internal string GetCreateQueryTest(string table, IMappedColumnList columns) =>
			GetCreateQuery(table, columns);

		internal string GetRetrieveQueryTest(string table, ColumnList columns, IColumn idColumn, long id) =>
			GetRetrieveQuery(table, columns, idColumn, id);

		internal string GetUpdateQueryTest(string table, ColumnList columns, IColumn idColumn, long id) =>
			GetUpdateQuery(table, columns, idColumn, id);

		internal string GetUpdateQueryTest(string table, ColumnList columns, IColumn idColumn, long id, IColumn? versionColumn) =>
			GetUpdateQuery(table, columns, idColumn, id, versionColumn);

		internal string GetDeleteQueryTest(string table, IColumn idColumn, long id) =>
			GetDeleteQuery(table, idColumn, id);

		internal string GetDeleteQueryTest(string table, IColumn idColumn, long id, IColumn? versionColumn) =>
			GetDeleteQuery(table, idColumn, id, versionColumn);

		#endregion

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Error getting General Retrieve query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingGeneralRetrieveQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Error getting CRUD Create query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingCrudCreateQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Error getting CRUD Retrieve query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingCrudRetrieveQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Error getting CRUD Update query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingCrudUpdateQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Error getting CRUD Delete query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingCrudDeleteQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
