// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Jeebs.Config;
using Jeebs.Data.Exceptions;
using Jeebs.Data.TypeHandlers;
using Microsoft.Extensions.Options;
using static F.OptionF;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IDb"/>
	public abstract class Db : IDb, IDisposable
	{
		/// <inheritdoc/>
		public IDbClient Client { get; private init; }

		/// <inheritdoc/>
		public IUnitOfWork UnitOfWork
		{
			get
			{
				Log.Debug("Starting new Unit of Work.");
				return new UnitOfWork(Connection.BeginTransaction(), Log);
			}
		}

		/// <summary>
		/// Configuration for this database connection
		/// </summary>
		public DbConnectionConfig Config { get; private init; }

		/// <summary>
		/// IDbConnection object
		/// </summary>
		private IDbConnection Connection { get; init; }

		/// <summary>
		/// ILog (should be given a context of the implementing class)
		/// </summary>
		protected ILog Log { get; private init; }

		/// <summary>
		/// Inject database connection and connect to client
		/// </summary>
		/// <param name="client">Database client</param>
		/// <param name="config">Database configuration</param>
		/// <param name="log">ILog (should be given a context of the implementing class)</param>
		/// <param name="name">Connection name</param>
		protected Db(IDbClient client, IOptions<DbConfig> config, ILog log, string name) :
			this(client, config.Value.GetConnection(name), log, name)
		{ }

		/// <summary>
		/// Inject database connection and connect to client
		/// </summary>
		/// <param name="client">Database client</param>
		/// <param name="config">Database configuration</param>
		/// <param name="log">ILog (should be given a context of the implementing class)</param>
		/// <param name="name">Connection name</param>
		protected Db(IDbClient client, DbConnectionConfig config, ILog log, string name)
		{
			Client = client;
			Config = config;
			Log = log;

			try
			{
				Connection = client.Connect(Config.ConnectionString);
				Connection.Open();
			}
			catch (Exception e)
			{
				throw new UnableToConnectToDatabaseException($"Unable to connect to database {name}.", e);
			}
		}

		/// <summary>
		/// Use Verbose log by default - override to send elsewhere (or to disable entirely)
		/// </summary>
		protected virtual Action<string, object[]> WriteToLog =>
			Log.Verbose;

		/// <summary>
		/// Write query to the log
		/// </summary>
		/// <param name="input">Input values</param>
		private void LogQuery((string query, object? parameters, CommandType type) input)
		{
			var (query, parameters, type) = input;

			// Always log operation, entity, and query
			var message = "{Type}: {Query}";
			var args = new object[] { type, query };

			// Log with or without parameters
			if (parameters == null)
			{
				WriteToLog(message, args);
			}
			else if (parameters.ToString() is string param)
			{
				message += " Parameters: {@Parameters}";
				WriteToLog(message, args.ExtendWith(param));
			}
		}

		/// <inheritdoc/>
		public Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			string query,
			object? parameters,
			CommandType type,
			IDbTransaction? transaction = null
		) =>
			Return(
				(query, parameters: parameters ?? new object(), type)
			)
			.Audit(
				some: LogQuery
			)
			.MapAsync(
				x => Connection.QueryAsync<TModel>(x.query, x.parameters, transaction, commandType: x.type),
				e => new Msg.QueryExceptionMsg(e)
			)
			.SwitchIfAsync(
				x => x.Any(),
				_ => new Msg.QueryItemsNotFoundMsg()
			);

		/// <inheritdoc/>
		public Task<Option<TModel>> QuerySingleAsync<TModel>(
			string query,
			object? parameters,
			CommandType type,
			IDbTransaction? transaction = null
		) =>
			Return(
				(query, parameters: parameters ?? new object(), type)
			)
			.Audit(
				some: LogQuery
			)
			.MapAsync(
				x => Connection.QuerySingleOrDefaultAsync<TModel>(x.query, x.parameters, transaction, commandType: x.type),
				e => new Msg.QuerySingleExceptionMsg(e)
			)
			.IfNullAsync(
				() => new Msg.QuerySingleItemNotFoundMsg()
			);

		/// <inheritdoc/>
		public Task<Option<bool>> ExecuteAsync(
			string query,
			object? parameters,
			CommandType type,
			IDbTransaction? transaction = null
		) =>
			Return(
				(query, parameters: parameters ?? new object(), type)
			)
			.Audit(
				some: LogQuery
			)
			.MapAsync(
				x => Connection.ExecuteAsync(x.query, x.parameters, transaction, commandType: x.type),
				e => new Msg.ExecuteExceptionMsg(e)
			)
			.MapAsync(
				x => x > 0,
				DefaultHandler
			);

		/// <inheritdoc/>
		public Task<Option<TReturn>> ExecuteAsync<TReturn>(
			string query,
			object? parameters,
			CommandType type,
			IDbTransaction? transaction = null
		) =>
			Return(
				(query, parameters: parameters ?? new object(), type)
			)
			.Audit(
				some: LogQuery
			)
			.MapAsync(
				x => Connection.ExecuteScalarAsync<TReturn>(x.query, x.parameters, transaction, commandType: x.type),
				e => new Msg.ExecuteScalarExceptionMsg(e)
			);

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
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Dispose managed resources
		/// </summary>
		/// <param name="disposing">True if disposing</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}

			if (disposing)
			{
				Connection?.Dispose();
			}

			disposed = true;
		}

		#endregion

		#region Static

		/// <summary>
		/// Add default type handlers
		/// </summary>
		static Db() =>
			SqlMapper.AddTypeHandler(new GuidTypeHandler());

		/// <summary>
		/// Persist an EnumList to the database by encoding it as JSON
		/// </summary>
		/// <typeparam name="T">Type to handle</typeparam>
		protected static void AddEnumeratedListTypeHandler<T>()
			where T : Enumerated =>
			SqlMapper.AddTypeHandler(new EnumeratedListTypeHandler<T>());

		/// <summary>
		/// Persist a type to the database by encoding it as JSON
		/// </summary>
		/// <typeparam name="T">Type to handle</typeparam>
		protected static void AddJsonTypeHandler<T>() =>
			SqlMapper.AddTypeHandler(new JsonTypeHandler<T>());

		/// <summary>
		/// Persist a StrongId to the database
		/// </summary>
		/// <typeparam name="T">StrongId itype</typeparam>
		protected static void AddStrongIdTypeHandler<T>()
			where T : StrongId, new() =>
			SqlMapper.AddTypeHandler(new StrongIdTypeHandler<T>());

		#endregion

		#region Testing

		internal void WriteToLogTest(string message, object[] args) =>
			WriteToLog(message, args);

		#endregion

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Error running QueryAsync</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record QueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>The query returned any empty list</summary>
			public sealed record QueryItemsNotFoundMsg : NotFoundMsg { }

			/// <summary>Error running QuerySingleAsync</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record QuerySingleExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>The query returned no items, or more than one</summary>
			public sealed record QuerySingleItemNotFoundMsg : NotFoundMsg { }

			/// <summary>Error running ExecuteAsync</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ExecuteExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Error running ExecuteScalarAsync</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ExecuteScalarExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
