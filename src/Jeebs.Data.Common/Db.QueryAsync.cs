// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data.Functions;
using Jeebs.Data.Query;

namespace Jeebs.Data.Common;

public abstract partial class Db
{
	#region Query + Param

	/// <inheritdoc/>
	public override Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param) =>
		QueryAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type) =>
		DoWorkAsync(
			unitOfWork => QueryAsync<T>(query, param, type, unitOfWork)
		);

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(string query, object? param, CommandType type, IUnitOfWork unitOfWork) =>
		R.Wrap(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			fOk: LogQuery<T>
		)
		.BindAsync(
			// a read joins the transaction only if a write has already begun it
			async x => await Client.Adapter.QueryAsync<T>(unitOfWork.Connection, unitOfWork.Transaction.Initialised ? await unitOfWork.Transaction : null, x.query, x.parameters, x.type)
		);

	#endregion

	#region Parts

	/// <inheritdoc/>
	public override Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts) =>
		DoWorkAsync(
			unitOfWork => QueryAsync<T>(parts, unitOfWork)
		);

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(IQueryParts parts, IUnitOfWork unitOfWork) =>
		from q in Client.GetQuery(parts)
		from r in QueryAsync<T>(q.query, q.param, CommandType.Text, unitOfWork)
		select r;

	/// <inheritdoc/>
	public override Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts) =>
		DoWorkAsync(
			unitOfWork => QueryAsync<T>(page, parts, unitOfWork)
		);

	/// <inheritdoc/>
	public Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, IQueryParts parts, IUnitOfWork unitOfWork) =>
		QueryAsync(
			page,
			parts,
			(query, param) => QuerySingleAsync<ulong>(query, param, CommandType.Text, unitOfWork),
			(query, param) => QueryAsync<T>(query, param, CommandType.Text, unitOfWork)
		);

	#endregion

	#region Builder

	/// <inheritdoc/>
	public override Task<Result<IEnumerable<T>>> QueryAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
		DoWorkAsync(
			unitOfWork => QueryAsync<T>(builder, unitOfWork)
		);

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> QueryAsync<T>(Func<IQueryBuilder, IQueryBuilderWithFrom> builder, IUnitOfWork unitOfWork) =>
		DataF.BuildQuery<T>(
			builder
		)
		.BindAsync(
			x => QueryAsync<T>(x, unitOfWork)
		);

	/// <inheritdoc/>
	public override Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, Func<IQueryBuilder, IQueryBuilderWithFrom> builder) =>
		DoWorkAsync(
			unitOfWork => QueryAsync<T>(page, builder, unitOfWork)
		);

	/// <inheritdoc/>
	public Task<Result<IPagedList<T>>> QueryAsync<T>(ulong page, Func<IQueryBuilder, IQueryBuilderWithFrom> builder, IUnitOfWork unitOfWork) =>
		DataF.BuildQuery<T>(
			builder
		)
		.BindAsync(
			x => QueryAsync<T>(page, x, unitOfWork)
		);

	#endregion
}
