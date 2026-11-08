// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;

namespace Jeebs.Data.Common;

public abstract partial class Db
{
	/// <inheritdoc/>
	public override Task<Result<T>> InsertAsync<T>(string query, object? param) =>
		InsertAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Result<T>> InsertAsync<T>(string query, object? param, CommandType type) =>
		DoWorkAsync(
			unitOfWork => InsertAsync<T>(query, param, type, unitOfWork)
		);

	/// <inheritdoc/>
	public Task<Result<T>> InsertAsync<T>(string query, object? param, CommandType type, IUnitOfWork unitOfWork) =>
		R.Wrap(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			fOk: LogQuery<T>
		)
		.BindAsync(
			async x => await Client.Adapter.ExecuteAsync<T>(unitOfWork.Connection, await unitOfWork.Transaction, x.query, x.parameters, x.type)
		)
		.BindAsync(
			x => x switch
			{
				T =>
					R.Wrap(x),

				_ =>
					R.Fail("Execution returned null value.", query, param)
						.Ctx(nameof(Db), nameof(ExecuteAsync))
			}
		);
}
