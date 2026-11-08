// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;

namespace Jeebs.Data.Common;

public abstract partial class Db
{
	/// <inheritdoc/>
	public override Task<Result<bool>> ExecuteAsync(string query, object? param) =>
		ExecuteAsync(query, param, CommandType.Text);

	/// <inheritdoc/>
	public Task<Result<bool>> ExecuteAsync(string query, object? param, CommandType type) =>
		DoWorkAsync(
			unitOfWork => ExecuteAsync(query, param, type, unitOfWork)
		);

	/// <inheritdoc/>
	public Task<Result<bool>> ExecuteAsync(string query, object? param, CommandType type, IUnitOfWork unitOfWork) =>
		R.Wrap(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			fOk: LogQuery<bool>
		)
		.BindAsync(
			async x => await Client.Adapter.ExecuteAsync(unitOfWork.Connection, await unitOfWork.Transaction, x.query, x.parameters, x.type)
		)
		.MapAsync(
			x => x > 0
		);
}
