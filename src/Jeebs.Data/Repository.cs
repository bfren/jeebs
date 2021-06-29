// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IRepository{TEntity, TId}"/>
	public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
		where TEntity : IWithId
		where TId : StrongId
	{
		/// <inheritdoc/>
		public IUnitOfWork UnitOfWork =>
			Db.UnitOfWork;

		/// <summary>
		/// IDb
		/// </summary>
		protected IDb Db { get; private init; }

		internal IDb DbTest =>
			Db;

		/// <summary>
		/// ILog (should be given a context of the implementing class)
		/// </summary>
		protected ILog Log { get; private init; }

		internal ILog LogTest =>
			Log;

		/// <summary>
		/// Inject database and log objects
		/// </summary>
		/// <param name="db">IDb</param>
		/// <param name="log">ILog (should be given a context of the implementing class)</param>
		protected Repository(IDb db, ILog log) =>
			(Db, Log) = (db, log);

		/// <summary>
		/// Use Debug log by default - override to send elsewhere (or to disable entirely)
		/// </summary>
		/// <param name="message">Log message</param>
		/// <param name="args">Log message arguments</param>
		internal virtual void WriteToLog(string message, object[] args) =>
			Log.Debug(message, args);

		/// <summary>
		/// Log the query for a function
		/// </summary>
		/// <typeparam name="T">Parameter type (an entity or model)</typeparam>
		/// <param name="operation">Operation (method) name</param>
		/// <param name="query">Query text</param>
		/// <param name="parameters">[Optional] Query parameters</param>
		protected void LogFunc<T>(string operation, string query, T? parameters)
		{
			// Always log operation, entity, and query
			var message = "{Operation} {Entity}: {Query}";
			var args = new object[]
			{
				operation.Replace("Async", string.Empty),
				typeof(TEntity).Name.Replace("Entity", string.Empty),
				query
			};

			// Log with or without parameters
			if (parameters == null)
			{
				WriteToLog(message, args);
			}
			else
			{
				message += " {@Parameters}";
				WriteToLog(message, args.ExtendWith(parameters));
			}
		}

		#region Custom Queries

		/// <inheritdoc/>
		public virtual async Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			params (Expression<Func<TEntity, object>>, Compare, object)[] predicates
		)
		{
			using var w = Db.UnitOfWork;
			return await
				Db.Client.GetQuery<TEntity, TModel>(
					predicates
				)
				.Audit(
					some: x => LogFunc(nameof(RetrieveAsync), x.query, x.param)
				)
				.BindAsync(
					x => Db.QueryAsync<TModel>(x.query, x.param, CommandType.Text, w.Transaction)
				)
				.ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public virtual Task<Option<TModel>> QuerySingleAsync<TModel>(
			params (Expression<Func<TEntity, object>>, Compare, object)[] predicates
		) =>
			QueryAsync<TModel>(
				predicates
			)
			.UnwrapAsync(
				x => x.Single<TModel>()
			);

		#endregion

		#region CRUD Queries

		/// <inheritdoc/>
		public virtual async Task<Option<TId>> CreateAsync(TEntity entity)
		{
			using var w = Db.UnitOfWork;
			return await CreateAsync(entity, w.Transaction).ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public virtual Task<Option<TId>> CreateAsync(TEntity entity, IDbTransaction transaction) =>
			Db.Client.GetCreateQuery<TEntity>()
			.Audit(
				some: x => LogFunc(nameof(CreateAsync), x, entity)
			)
			.BindAsync(
				x => Db.ExecuteAsync<TId>(x, entity, CommandType.Text, transaction)
			);

		/// <inheritdoc/>
		public virtual async Task<Option<TModel>> RetrieveAsync<TModel>(TId id)
		{
			using var w = Db.UnitOfWork;
			return await RetrieveAsync<TModel>(id, w.Transaction).ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public virtual Task<Option<TModel>> RetrieveAsync<TModel>(TId id, IDbTransaction transaction) =>
			Db.Client.GetRetrieveQuery<TEntity, TModel>(id.Value)
			.Audit(
				some: x => LogFunc(nameof(RetrieveAsync), x, id)
			)
			.BindAsync(
				x => Db.QuerySingleAsync<TModel>(x, null, CommandType.Text, transaction)
			);

		/// <inheritdoc/>
		public virtual async Task<Option<bool>> UpdateAsync<TModel>(TModel model)
			where TModel : IWithId
		{
			using var w = Db.UnitOfWork;
			return await UpdateAsync(model, w.Transaction).ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public virtual Task<Option<bool>> UpdateAsync<TModel>(TModel model, IDbTransaction transaction)
			where TModel : IWithId =>
			Db.Client.GetUpdateQuery<TEntity, TModel>(model.Id.Value)
			.Audit(
				some: x => LogFunc(nameof(UpdateAsync), x, model)
			)
			.BindAsync(
				x => Db.ExecuteAsync(x, model, CommandType.Text, transaction)
			);

		/// <inheritdoc/>
		public virtual async Task<Option<bool>> DeleteAsync(TId id)
		{
			using var w = Db.UnitOfWork;
			return await DeleteAsync(id, w.Transaction).ConfigureAwait(false);
		}

		/// <inheritdoc/>
		public virtual Task<Option<bool>> DeleteAsync(TId id, IDbTransaction transaction) =>
			Db.Client.GetDeleteQuery<TEntity>(id.Value)
			.Audit(
				some: x => LogFunc(nameof(DeleteAsync), x, id)
			)
			.BindAsync(
				x => Db.ExecuteAsync(x, null, CommandType.Text, transaction)
			);

		#endregion

		#region Testing

		internal void WriteToLogTest(string message, object[] args) =>
			WriteToLog(message, args);

		#endregion
	}
}
