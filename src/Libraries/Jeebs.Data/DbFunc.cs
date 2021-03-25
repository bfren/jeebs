// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IDbFunc{TEntity, TId}"/>
	public abstract class DbFunc<TEntity, TId> : DbQuery, IDbFunc<TEntity, TId>
		where TEntity : IEntity
		where TId : StrongId
	{
		/// <summary>
		/// Inject database and log objects
		/// </summary>
		/// <param name="db">IDb</param>
		/// <param name="log">ILog (should be given a context of the implementing class)</param>
		protected DbFunc(IDb db, ILog log) : base(db, log) { }

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
		public virtual Task<Option<TModel>> QuerySingleAsync<TModel>(
			params (Expression<Func<TEntity, object>>, SearchOperator, object)[] predicates
		) =>
			QueryAsync<TModel>(
				predicates
			)
			.UnwrapAsync(
				x => x.Single<TModel>()
			);

		/// <inheritdoc/>
		public virtual Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(
			params (Expression<Func<TEntity, object>>, SearchOperator, object)[] predicates
		) =>
			Db.Client.GetRetrieveQuery<TEntity, TModel>(predicates)
			.AuditSwitch(
				some: x => LogFunc(nameof(RetrieveAsync), x.query, x.param)
			)
			.BindAsync(
				x => Db.QueryAsync<TModel>(x.query, x.param, CommandType.Text)
			);

		#endregion

		#region CRUD Queries

		/// <inheritdoc/>
		public virtual Task<Option<TId>> CreateAsync(TEntity entity) =>
			Db.Client.GetCreateQuery<TEntity>()
			.AuditSwitch(
				some: x => LogFunc(nameof(CreateAsync), x, entity)
			)
			.BindAsync(
				x => Db.ExecuteAsync<TId>(x, entity, CommandType.Text)
			);

		/// <inheritdoc/>
		public virtual Task<Option<TModel>> RetrieveAsync<TModel>(TId id) =>
			Db.Client.GetRetrieveQuery<TEntity, TModel>(id.Value)
			.AuditSwitch(
				some: x => LogFunc(nameof(RetrieveAsync), x, id)
			)
			.BindAsync(
				x => Db.QuerySingleAsync<TModel>(x, null, CommandType.Text)
			);

		/// <inheritdoc/>
		public virtual Task<Option<bool>> UpdateAsync<TModel>(TModel model)
			where TModel : IWithId =>
			Db.Client.GetUpdateQuery<TEntity, TModel>(model.Id.Value)
			.AuditSwitch(
				some: x => LogFunc(nameof(UpdateAsync), x, model)
			)
			.BindAsync(
				x => Db.ExecuteAsync(x, model, CommandType.Text)
			);

		/// <inheritdoc/>
		public virtual Task<Option<bool>> DeleteAsync(TId id) =>
			Db.Client.GetDeleteQuery<TEntity>(id.Value)
			.AuditSwitch(
				some: x => LogFunc(nameof(DeleteAsync), x, id)
			)
			.BindAsync(
				x => Db.ExecuteAsync(x, null, CommandType.Text)
			);

		#endregion
	}
}
