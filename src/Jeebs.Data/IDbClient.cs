// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Data.Common;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query;
using StrongId;

namespace Jeebs.Data;

/// <summary>
/// Database client.
/// </summary>
public interface IDbClient
{
	/// <summary>
	/// Entity / table mapper
	/// </summary>
	IEntityMapper Entities { get; }

	/// <summary>
	/// Type mapper
	/// </summary>
	IDbTypeMapper Types { get; }

	/// <summary>
	/// Return an open database connection
	/// </summary>
	/// <param name="connectionString">Database connection string</param>
	DbConnection GetConnection(string connectionString);

	#region Escaping and Joining

	/// <summary>
	/// Escape a table
	/// </summary>
	/// <param name="table">Table</param>
	string Escape(ITable table);

	/// <summary>
	/// Escape a table name
	/// </summary>
	/// <param name="table">ITableName</param>
	string Escape(IDbName table);

	/// <summary>
	/// Escape a column with its table
	/// </summary>
	/// <param name="table">ITableName</param>
	/// <param name="column">Column name</param>
	string Escape(IDbName table, string column);

	/// <summary>
	/// Escape a column without using an alias
	/// </summary>
	/// <param name="column">Column</param>
	string Escape(IColumn column);

	/// <summary>
	/// Escape a column
	/// </summary>
	/// <param name="column">Column</param>
	/// <param name="withAlias">[Optional] If true, will escape and add the column alias as well</param>
	string Escape(IColumn column, bool withAlias);

	/// <summary>
	/// Escape a column with its table name without using an alias
	/// </summary>
	/// <param name="column">Column</param>
	string EscapeWithTable(IColumn column);

	/// <summary>
	/// Escape a column with its table name
	/// </summary>
	/// <param name="column">Column</param>
	/// <param name="withAlias">[Optional] If true, will escape and add the column alias as well</param>
	string EscapeWithTable(IColumn column, bool withAlias);

	/// <summary>
	/// Escape an object, usually a column or table
	/// </summary>
	/// <param name="obj">Column or Table name</param>
	string Escape(string obj);

	/// <summary>
	/// Convert a <see cref="Compare"/> to actual operator
	/// </summary>
	/// <param name="cmp">Compare</param>
	string GetOperator(Compare cmp);

	/// <summary>
	/// Get a parameter reference - e.g. 'P2' becomes '@P2' for MySQL
	/// </summary>
	/// <param name="paramName">Param name</param>
	string GetParamRef(string paramName);

	/// <summary>
	/// Join a list of columns or parameters to be used in a query, e.g. to (@P0,@P1,@P2)
	/// </summary>
	/// <param name="objects">Objects to join</param>
	/// <param name="wrap">If true, the list will be wrapped (usually in parentheses)</param>
	string JoinList(List<string> objects, bool wrap);

	#endregion Escaping and Joining

	#region Custom Queries

	/// <summary>
	/// Return a query to retrieve a list of entities that match all the specified parameters
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TModel">Return model type</typeparam>
	/// <param name="predicates">Predicates (matched using AND)</param>
	Maybe<(string query, IQueryParametersDictionary param)> GetQuery<TEntity, TModel>(
		(string, Compare, dynamic)[] predicates
	)
		where TEntity : IWithId;

	/// <summary>
	/// Return a query to retrieve how many entities match the specified query parts
	/// </summary>
	/// <param name="parts">IQueryParts</param>
	(string query, IQueryParametersDictionary param) GetCountQuery(IQueryParts parts);

	/// <summary>
	/// Return a query to retrieve a list of entities using the specified query parts
	/// </summary>
	/// <param name="parts">IQueryParts</param>
	(string query, IQueryParametersDictionary param) GetQuery(IQueryParts parts);

	#endregion Custom Queries

	#region CRUD Queries

	/// <summary>
	/// Return a query to create an entity
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	Maybe<string> GetCreateQuery<TEntity>()
		where TEntity : IWithId;

	/// <summary>
	/// Return a query to retrieve a single entity by ID
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TModel">Return model type</typeparam>
	/// <param name="id">Entity ID</param>
	Maybe<string> GetRetrieveQuery<TEntity, TModel>(object id)
		where TEntity : IWithId;

	/// <summary>
	/// Return a query to update a single entity
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TModel">Return model type</typeparam>
	/// <param name="id">Entity ID</param>
	Maybe<string> GetUpdateQuery<TEntity, TModel>(object id)
		where TEntity : IWithId;

	/// <summary>
	/// Return a query to delete a single entity by ID
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <param name="id">Entity ID</param>
	Maybe<string> GetDeleteQuery<TEntity>(object id)
		where TEntity : IWithId;

	#endregion CRUD Queries
}
