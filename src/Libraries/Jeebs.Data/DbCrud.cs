// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IDbCrud{TEntity}"/>
	public abstract class DbCrud<TEntity, TId> : DbQuery, IDbCrud<TEntity, TId>
		where TEntity : IEntity
		where TId : StrongId
	{
		/// <summary>
		/// Inject database and log objects
		/// </summary>
		/// <param name="db">IDb</param>
		/// <param name="log">ILog (should be given a context of the implementing class)</param>
		protected DbCrud(IDb db, ILog log) : base(db, log) { }

		/// <summary>
		/// Log a query
		/// </summary>
		/// <typeparam name="T">Parameter type (an entity or model)</typeparam>
		/// <param name="operation">Operation (method) name</param>
		/// <param name="query">Query text</param>
		/// <param name="parameters">Query parameters</param>
		protected void LogCrudQuery<T>(string operation, string query, T? parameters)
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

		/// <inheritdoc/>
		public Task<Option<TId>> CreateAsync(TEntity entity) =>
			Db.Client.GetCreateQuery<TEntity>()
			.AuditSwitch(
				some: x => LogCrudQuery(nameof(CreateAsync), x, entity)
			)
			.BindAsync(
				x => Db.ExecuteAsync<TId>(x, entity, CommandType.Text)
			);

		/// <inheritdoc/>
		public Task<Option<TModel>> RetrieveAsync<TModel>(TId id) =>
			Db.Client.GetRetrieveQuery<TEntity, TModel>()
			.AuditSwitch(
				some: x => LogCrudQuery(nameof(RetrieveAsync), x, new { id })
			)
			.BindAsync(
				x => Db.QuerySingleAsync<TModel>(x, new { id }, CommandType.Text)
			);

		/// <inheritdoc/>
		public Task<Option<bool>> UpdateAsync<TModel>(TModel model) =>
			Db.Client.GetUpdateQuery<TEntity, TModel>()
			.AuditSwitch(
				some: x => LogCrudQuery(nameof(UpdateAsync), x, model)
			)
			.BindAsync(
				x => Db.ExecuteAsync(x, model, CommandType.Text)
			);

		/// <inheritdoc/>
		public Task<Option<bool>> DeleteAsync(TId id) =>
			Db.Client.GetDeleteQuery<TEntity>()
			.AuditSwitch(
				some: x => LogCrudQuery(nameof(DeleteAsync), x, new { id })
			)
			.BindAsync(
				x => Db.ExecuteAsync(x, new { id }, CommandType.Text)
			);
	}
}
