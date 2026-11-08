// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Logging;
using Base = Jeebs.Data.Repository;

namespace Jeebs.Data.Common;

/// <inheritdoc cref="IRepository{TEntity, TId}"/>
public abstract class Repository<TEntity, TId> : Base.Repository<TEntity, TId>, IRepository<TEntity, TId>
	where TEntity : IWithId
	where TId : class, IMonad, new()
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
	protected Repository(IDb db, ILog<IRepository<TEntity, TId>> log) : base(db, log) =>
		Db = db;

	#region CRUD Queries

	/// <inheritdoc/>
	public override Task<Result<TId>> CreateAsync(TEntity entity) =>
		Db.DoWorkAsync(
			unitOfWork => CreateAsync(entity, unitOfWork)
		);

	/// <inheritdoc/>
	public virtual Task<Result<TId>> CreateAsync(TEntity entity, IUnitOfWork unitOfWork) =>
		Db.Client.GetCreateQuery<TEntity>()
		.Audit(
			fOk: _ => LogFunc(nameof(CreateAsync))
		)
		.BindAsync(
			x => Db.InsertAsync<TId>(x, entity, CommandType.Text, unitOfWork)
		);

	/// <inheritdoc/>
	public override Task<Result<TModel>> RetrieveAsync<TModel>(TId id) =>
		Db.DoWorkAsync(
			unitOfWork => RetrieveAsync<TModel>(id, unitOfWork)
		);

	/// <inheritdoc/>
	public virtual Task<Result<TModel>> RetrieveAsync<TModel>(TId id, IUnitOfWork unitOfWork) =>
		Db.Client.GetRetrieveQuery<TEntity, TModel>()
		.Audit(
			fOk: _ => LogFunc(nameof(RetrieveAsync))
		)
		.BindAsync(
			x => Db.QuerySingleAsync<TModel>(x, new { Id = id.Value }, CommandType.Text, unitOfWork)
		);

	/// <inheritdoc/>
	public override Task<Result<bool>> UpdateAsync<TModel>(TModel model) =>
		Db.DoWorkAsync(
			unitOfWork => UpdateAsync(model, unitOfWork)
		);

	/// <inheritdoc/>
	public virtual Task<Result<bool>> UpdateAsync<TModel>(TModel model, IUnitOfWork unitOfWork)
		where TModel : IWithId =>
		Db.Client.GetUpdateQuery<TEntity, TModel>()
		.Audit(
			fOk: _ => LogFunc(nameof(UpdateAsync))
		)
		.BindAsync(
			x => Db.ExecuteAsync(x, model, CommandType.Text, unitOfWork)
		);

	/// <inheritdoc/>
	public override Task<Result<bool>> DeleteAsync<TModel>(TModel model) =>
		Db.DoWorkAsync(
			unitOfWork => DeleteAsync(model, unitOfWork)
		);

	/// <inheritdoc/>
	public virtual Task<Result<bool>> DeleteAsync<TModel>(TModel model, IUnitOfWork unitOfWork)
		where TModel : IWithId =>
		Db.Client.GetDeleteQuery<TEntity>()
		.Audit(
			fOk: _ => LogFunc(nameof(DeleteAsync))
		)
		.BindAsync(
			x => Db.ExecuteAsync(x, model, CommandType.Text, unitOfWork)
		);

	#endregion CRUD Queries
}
