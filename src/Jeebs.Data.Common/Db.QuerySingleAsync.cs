// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Data.Functions;
using Jeebs.Data.Query;

namespace Jeebs.Data.Common;

public abstract partial class Db
{
	#region Query + Param

	/// <inheritdoc/>
	public override Task<Result<T>> QuerySingleAsync<T>(string query, object? param) =>
		QuerySingleAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(string query, object? param, CommandType type) =>
		DoWorkAsync(
			unitOfWork => QuerySingleAsync<T>(query, param, type, unitOfWork)
		);

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(string query, object? param, CommandType type, IUnitOfWork unitOfWork) =>
		R.Wrap(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			fOk: LogQuery<T>
		)
		.BindAsync(
			// a read joins the transaction only if a write has already begun it
			async x => await Client.Adapter.QuerySingleAsync<T>(unitOfWork.Connection, unitOfWork.Transaction.Initialised ? await unitOfWork.Transaction : null, x.query, x.parameters, x.type)
		)
		.BindAsync(
			x => x switch
			{
				T =>
					R.Wrap(x),

				_ =>
					R.Fail("Item not found or multiple items returned.", query, param)
						.Ctx(nameof(Db), nameof(QuerySingleAsync))
			}
		);

	#endregion

	#region Parts

	/// <inheritdoc/>
	public override Task<Result<T>> QuerySingleAsync<T>(IQueryParts parts) =>
		DoWorkAsync(
			unitOfWork => QuerySingleAsync<T>(parts, unitOfWork)
		);

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(IQueryParts parts, IUnitOfWork unitOfWork) =>
		from q in Client.GetQuery(parts)
		from r in QuerySingleAsync<T>(q.query, q.param, CommandType.Text, unitOfWork)
		select r;

	#endregion

	#region Builder

	/// <inheritdoc/>
	public override Task<Result<T>> QuerySingleAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
		DoWorkAsync(
			unitOfWork => QuerySingleAsync<T>(builder, unitOfWork)
		);

	/// <inheritdoc/>
	public Task<Result<T>> QuerySingleAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder, IUnitOfWork unitOfWork) =>
		DataF.BuildQuery<T>(
			builder
		)
		.BindAsync(
			x => QuerySingleAsync<T>(x, unitOfWork)
		);

	#endregion
}
