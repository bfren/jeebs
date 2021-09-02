// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;

namespace Jeebs.Data
{
	/// <summary>
	/// Database client
	/// </summary>
	public interface IDbClient
	{
		/// <summary>
		/// Return a new database connection
		/// </summary>
		/// <param name="connectionString">Database connection string</param>
		IDbConnection Connect(string connectionString);

		#region Escaping and Joining

		/// <summary>
		/// Escape a column
		/// </summary>
		/// <param name="column">Column</param>
		/// <param name="withAlias">[Optional] If true, will escape and add the column alias as well</param>
		string Escape(IColumn column, bool withAlias = false);

		/// <summary>
		/// Escape a column with its table name
		/// </summary>
		/// <param name="column">Column</param>
		/// <param name="withAlias">[Optional] If true, will escape and add the column alias as well</param>
		string EscapeWithTable(IColumn column, bool withAlias = false);

		/// <summary>
		/// Escape a table
		/// </summary>
		/// <param name="table">Table</param>
		string Escape(ITable table);

		/// <summary>
		/// Escape a column or table
		/// </summary>
		/// <param name="columnOrTable">Column or Table name</param>
		string Escape(string columnOrTable);

		/// <summary>
		/// Escape a column with its table
		/// </summary>
		/// <param name="column">Column name</param>
		/// <param name="table">Table name</param>
		string Escape(string column, string table);

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

		#endregion

		#region Custom Queries

		/// <summary>
		/// Return a query to retrieve a list of entities that match all the specified parameters
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TModel">Return model type</typeparam>
		/// <param name="predicates">Predicates (matched using AND)</param>
		Option<(string query, IQueryParameters param)> GetQuery<TEntity, TModel>(
			(Expression<Func<TEntity, object>>, Compare, object)[] predicates
		)
			where TEntity : IWithId;

		/// <summary>
		/// Return a query to retrieve how many entities match the specified query parts
		/// </summary>
		(string query, IQueryParameters param) GetCountQuery(IQueryParts parts);

		/// <summary>
		/// Return a query to retrieve a list of entities using the specified query parts
		/// </summary>
		(string query, IQueryParameters param) GetQuery(IQueryParts parts);

		#endregion

		#region CRUD Queries

		/// <summary>
		/// Return a query to create an entity
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		Option<string> GetCreateQuery<TEntity>()
			where TEntity : IWithId;

		/// <summary>
		/// Return a query to retrieve a single entity by ID
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TModel">Return model type</typeparam>
		/// <param name="id">Entity ID</param>
		Option<string> GetRetrieveQuery<TEntity, TModel>(ulong id)
			where TEntity : IWithId;

		/// <summary>
		/// Return a query to update a single entity
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TModel">Return model type</typeparam>
		/// <param name="id">Entity ID</param>
		Option<string> GetUpdateQuery<TEntity, TModel>(ulong id)
			where TEntity : IWithId;

		/// <summary>
		/// Return a query to delete a single entity by ID
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="id">Entity ID</param>
		Option<string> GetDeleteQuery<TEntity>(ulong id)
			where TEntity : IWithId;

		#endregion
	}
}
