// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Data.Common.FluentQuery;
using Jeebs.Logging;

namespace Jeebs.Data.Common;

/// <inheritdoc cref="IRepository{TEntity, TId}"/>
public abstract class Repository<TEntity, TId> : Repository<FluentQuery<TEntity, TId>, TEntity, TId>, IRepository<FluentQuery<TEntity, TId>, TEntity, TId>
	where TEntity : IWithId
	where TId : class, IUnion, new()
{
	/// <summary>
	/// IDb.
	/// </summary>
	protected new IDb Db { get; private init; }

	/// <summary>
	/// Inject database and log objects.
	/// </summary>
	/// <param name="db">IDb.</param>
	/// <param name="log">ILog (should be given a context of the implementing class).</param>
	protected Repository(IDb db, ILog<IRepository<FluentQuery<TEntity, TId>, TEntity, TId>> log) : base(db, log) =>
		Db = db;

	#region Fluent Queries

	/// <inheritdoc/>
	public override FluentQuery<TEntity, TId> StartFluentQuery() =>
		new(Db, Db.Client.Entities, Log);

	#endregion Fluent Queries

	#region CRUD Queries

	/// <inheritdoc/>
	public override async Task<Result<TId>> CreateAsync(TEntity entity)
	{
		using var w = await Db.StartWorkAsync();
		return await CreateAsync(entity, w.Transaction);
	}

	/// <inheritdoc/>
	public virtual Task<Result<TId>> CreateAsync(TEntity entity, IDbTransaction transaction) =>
		Db.Client.GetCreateQuery<TEntity>()
		.Audit(
			ok: _ => LogFunc(nameof(CreateAsync))
		)
		.BindAsync(
			x => Db.ExecuteAsync<TId>(x, entity, CommandType.Text, transaction)
		);

	/// <inheritdoc/>
	public override async Task<Result<TModel>> RetrieveAsync<TModel>(TId id)
	{
		using var w = await Db.StartWorkAsync();
		return await RetrieveAsync<TModel>(id, w.Transaction);
	}

	/// <inheritdoc/>
	public virtual Task<Result<TModel>> RetrieveAsync<TModel>(TId id, IDbTransaction transaction) =>
		Db.Client.GetRetrieveQuery<TEntity, TModel>(id.Value)
		.Audit(
			ok: _ => LogFunc(nameof(RetrieveAsync))
		)
		.BindAsync(
			x => Db.QuerySingleAsync<TModel>(x, null, CommandType.Text, transaction)
		);

	/// <inheritdoc/>
	public override async Task<Result<bool>> UpdateAsync<TModel>(TModel model)
	{
		using var w = await Db.StartWorkAsync();
		return await UpdateAsync(model, w.Transaction);
	}

	/// <inheritdoc/>
	public virtual Task<Result<bool>> UpdateAsync<TModel>(TModel model, IDbTransaction transaction)
		where TModel : IWithId =>
		Db.Client.GetUpdateQuery<TEntity, TModel>(model.Id.Value)
		.Audit(
			ok: _ => LogFunc(nameof(UpdateAsync))
		)
		.BindAsync(
			x => Db.ExecuteAsync(x, model, CommandType.Text, transaction)
		);

	/// <inheritdoc/>
	public override async Task<Result<bool>> DeleteAsync<TModel>(TModel model)
	{
		using var w = await Db.StartWorkAsync();
		return await DeleteAsync(model, w.Transaction);
	}

	/// <inheritdoc/>
	public virtual Task<Result<bool>> DeleteAsync<TModel>(TModel model, IDbTransaction transaction)
		where TModel : IWithId =>
		Db.Client.GetDeleteQuery<TEntity>(model.Id.Value)
		.Audit(
			ok: _ => LogFunc(nameof(DeleteAsync))
		)
		.BindAsync(
			x => Db.ExecuteAsync(x, model, CommandType.Text, transaction)
		);

	#endregion CRUD Queries
}
