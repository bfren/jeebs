// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Data.Query;
using Jeebs.Logging;
using StrongId;

namespace Jeebs.Data;

/// <inheritdoc cref="IRepository{TEntity, TId}"/>
public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
	where TEntity : IWithId<TId>
	where TId : class, IStrongId, new()
{
	/// <summary>
	/// IDb
	/// </summary>
	protected IDb Db { get; private init; }

	internal IDb DbTest =>
		Db;

	/// <summary>
	/// ILog (should be given a context of the implementing class)
	/// </summary>
	protected ILog<IRepository<TEntity, TId>> Log { get; private init; }

	internal ILog LogTest =>
		Log;

	/// <summary>
	/// Inject database and log objects
	/// </summary>
	/// <param name="db">IDb</param>
	/// <param name="log">ILog (should be given a context of the implementing class)</param>
	protected Repository(IDb db, ILog<IRepository<TEntity, TId>> log) =>
		(Db, Log) = (db, log);

	/// <summary>
	/// Use Debug log by default - override to send elsewhere (or to disable entirely)
	/// </summary>
	/// <param name="message">Log message</param>
	/// <param name="args">Log message arguments</param>
	internal virtual void WriteToLog(string message, object[] args) =>
		Log.Vrb(message, args);

	/// <summary>
	/// Log an operation
	/// </summary>
	/// <param name="operation">Operation (method) name</param>
	protected void LogFunc(string operation) =>
		WriteToLog("{Operation} {Entity}",
		[
			operation.Replace("Async", string.Empty),
			typeof(TEntity).Name.Replace("Entity", string.Empty)
		]);

	#region Fluent Queries

	/// <inheritdoc/>
	public virtual IFluentQuery<TEntity, TId> StartFluentQuery() =>
		new FluentQuery<TEntity, TId>(Db, Db.Client.Entities, Log);

	#endregion Fluent Queries

	#region CRUD Queries

	/// <inheritdoc/>
	public virtual async Task<Maybe<TId>> CreateAsync(TEntity entity)
	{
		using var w = await Db.StartWorkAsync();
		return await CreateAsync(entity, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public virtual Task<Maybe<TId>> CreateAsync(TEntity entity, IDbTransaction transaction) =>
		Db.Client.GetCreateQuery<TEntity>()
		.Audit(
			some: _ => LogFunc(nameof(CreateAsync))
		)
		.BindAsync(
			x => Db.ExecuteAsync<TId>(x, entity, CommandType.Text, transaction)
		);

	/// <inheritdoc/>
	public virtual async Task<Maybe<TModel>> RetrieveAsync<TModel>(TId id)
	{
		using var w = await Db.StartWorkAsync();
		return await RetrieveAsync<TModel>(id, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public virtual Task<Maybe<TModel>> RetrieveAsync<TModel>(TId id, IDbTransaction transaction) =>
		Db.Client.GetRetrieveQuery<TEntity, TModel>(id.Value)
		.Audit(
			some: _ => LogFunc(nameof(RetrieveAsync))
		)
		.BindAsync(
			x => Db.QuerySingleAsync<TModel>(x, null, CommandType.Text, transaction)
		);

	/// <inheritdoc/>
	public virtual async Task<Maybe<bool>> UpdateAsync<TModel>(TModel model)
		where TModel : IWithId
	{
		using var w = await Db.StartWorkAsync();
		return await UpdateAsync(model, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public virtual Task<Maybe<bool>> UpdateAsync<TModel>(TModel model, IDbTransaction transaction)
		where TModel : IWithId =>
		Db.Client.GetUpdateQuery<TEntity, TModel>(model.Id.Value)
		.Audit(
			some: _ => LogFunc(nameof(UpdateAsync))
		)
		.BindAsync(
			x => Db.ExecuteAsync(x, model, CommandType.Text, transaction)
		);

	/// <inheritdoc/>
	public virtual async Task<Maybe<bool>> DeleteAsync<TModel>(TModel model)
		where TModel : IWithId
	{
		using var w = await Db.StartWorkAsync();
		return await DeleteAsync(model, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public virtual Task<Maybe<bool>> DeleteAsync<TModel>(TModel model, IDbTransaction transaction)
		where TModel : IWithId =>
		Db.Client.GetDeleteQuery<TEntity>(model.Id.Value)
		.Audit(
			some: _ => LogFunc(nameof(DeleteAsync))
		)
		.BindAsync(
			x => Db.ExecuteAsync(x, model, CommandType.Text, transaction)
		);

	#endregion CRUD Queries

	#region Testing

	internal void WriteToLogTest(string message, object[] args) =>
		WriteToLog(message, args);

	#endregion Testing
}
