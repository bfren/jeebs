// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Wrapper for Dapper extension methods
	/// </summary>
	public class DapperQueryDriver : IQueryDriver
	{
		/// <inheritdoc/>
		public IEnumerable<dynamic> Query(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType) =>
			cnn.Query(sql, param, transaction, commandType: commandType);

		/// <inheritdoc/>
		public Task<IEnumerable<dynamic>> QueryAsync(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType) =>
			cnn.QueryAsync(sql, param, transaction, commandType: commandType);

		/// <inheritdoc/>
		public IEnumerable<T> Query<T>(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType) =>
			cnn.Query<T>(sql, param, transaction, commandType: commandType);

		/// <inheritdoc/>
		public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType) =>
			cnn.QueryAsync<T>(sql, param, transaction, commandType: commandType);

		/// <inheritdoc/>
		public T QuerySingle<T>(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType) =>
			cnn.QuerySingle<T>(sql, param, transaction, commandType: commandType);

		/// <inheritdoc/>
		public Task<T> QuerySingleAsync<T>(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType) =>
			cnn.QuerySingleAsync<T>(sql, param, transaction, commandType: commandType);

		/// <inheritdoc/>
		public int Execute(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType) =>
			cnn.Execute(sql, param, transaction, commandType: commandType);

		/// <inheritdoc/>
		public Task<int> ExecuteAsync(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType) =>
			cnn.ExecuteAsync(sql, param, transaction, commandType: commandType);

		/// <inheritdoc/>
		public T ExecuteScalar<T>(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType) =>
			cnn.ExecuteScalar<T>(sql, param, transaction, commandType: commandType);

		/// <inheritdoc/>
		public Task<T> ExecuteScalarAsync<T>(IDbConnection cnn, string sql, object? param, IDbTransaction transaction, CommandType commandType) =>
			cnn.ExecuteScalarAsync<T>(sql, param, transaction, commandType: commandType);
	}
}
