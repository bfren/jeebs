// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Logging;

namespace Jeebs.Data.Repository;

/// <inheritdoc cref="IRepository{TEntity, TId}"/>
public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
	where TEntity : IWithId
	where TId : class, IUnion, new()
{
	/// <summary>
	/// IDb.
	/// </summary>
	protected IDb Db { get; private init; }

	internal IDb DbTest =>
		Db;

	/// <summary>
	/// ILog (should be given a context of the implementing class).
	/// </summary>
	protected ILog<IRepository<TEntity, TId>> Log { get; private init; }

	internal ILog LogTest =>
		Log;

	/// <summary>
	/// Inject database and log objects.
	/// </summary>
	/// <param name="db">IDb.</param>
	/// <param name="log">ILog (should be given a context of the implementing class).</param>
	protected Repository(IDb db, ILog<IRepository<TEntity, TId>> log) =>
		(Db, Log) = (db, log);

	/// <inheritdoc/>
	public virtual IFluentQuery<TEntity, TId> Fluent() =>
		new FluentQuery<TEntity, TId>(Db, Db.Client.EntityMapper, Log.ForContext<FluentQuery<TEntity, TId>>());

	/// <summary>
	/// Use Debug log by default - override to send elsewhere (or to disable entirely).
	/// </summary>
	/// <param name="message">Log message.</param>
	/// <param name="args">Log message arguments.</param>
	internal virtual void WriteToLog(string message, object[] args) =>
		Log.Vrb(message, args);

	/// <summary>
	/// Log an operation.
	/// </summary>
	/// <param name="operation">Operation (method) name.</param>
	protected void LogFunc(string operation) =>
		WriteToLog("{Operation} {Entity}",
		[
			operation.Replace("Async", string.Empty),
			typeof(TEntity).Name.Replace("Entity", string.Empty)
		]);

	#region CRUD

	/// <inheritdoc/>
	public virtual Task<Result<TId>> CreateAsync(TEntity entity) =>
		Db.Client.GetCreateQuery<TEntity>()
		.Audit(
			ok: _ => LogFunc(nameof(CreateAsync))
		)
		.BindAsync(
			x => Db.ExecuteAsync<TId>(x, entity)
		);

	/// <inheritdoc/>
	public virtual Task<Result<TModel>> RetrieveAsync<TModel>(TId id) =>
		Db.Client.GetRetrieveQuery<TEntity, TModel>(id.Value)
		.Audit(
			ok: _ => LogFunc(nameof(RetrieveAsync))
		)
		.BindAsync(
			x => Db.QuerySingleAsync<TModel>(x, null)
		);

	/// <inheritdoc/>
	public virtual Task<Result<bool>> UpdateAsync<TModel>(TModel model)
		where TModel : IWithId =>
		Db.Client.GetUpdateQuery<TEntity, TModel>(model.Id.Value)
		.Audit(
			ok: _ => LogFunc(nameof(UpdateAsync))
		)
		.BindAsync(
			x => Db.ExecuteAsync(x, model)
		);

	/// <inheritdoc/>
	public virtual Task<Result<bool>> DeleteAsync<TModel>(TModel model)
		where TModel : IWithId =>
		Db.Client.GetDeleteQuery<TEntity>(model.Id.Value)
		.Audit(
			ok: _ => LogFunc(nameof(DeleteAsync))
		)
		.BindAsync(
			x => Db.ExecuteAsync(x, model)
		);

	#endregion

	#region Testing

	internal void WriteToLogTest(string message, object[] args) =>
		WriteToLog(message, args);

	#endregion Testing
}
