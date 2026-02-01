// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;

namespace Jeebs.Data.Common;

public abstract partial class Db
{
	#region No Return Value

	/// <inheritdoc/>
	public override Task<Result<bool>> ExecuteAsync(string query, object? param) =>
		ExecuteAsync(query, param, CommandType.Text);

	/// <inheritdoc/>
	public async Task<Result<bool>> ExecuteAsync(string query, object? param, CommandType type)
	{
		await using var w = await StartWorkAsync();
		return await ExecuteAsync(query, param, type, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<bool>> ExecuteAsync(string query, object? param, CommandType type, IDbTransaction transaction) =>
		R.Wrap(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			ok: LogQuery<bool>
		)
		.BindAsync(
			x => Client.Adapter.ExecuteAsync(transaction, x.query, x.parameters, x.type)
		)
		.MapAsync(
			x => x > 0
		);

	#endregion

	#region With Return Value

	/// <inheritdoc/>
	public override Task<Result<T>> ExecuteAsync<T>(string query, object? param) =>
		ExecuteAsync<T>(query, param, CommandType.Text);

	/// <inheritdoc/>
	public async Task<Result<T>> ExecuteAsync<T>(string query, object? param, CommandType type)
	{
		await using var w = await StartWorkAsync();
		return await ExecuteAsync<T>(query, param, type, w.Transaction);
	}

	/// <inheritdoc/>
	public Task<Result<T>> ExecuteAsync<T>(string query, object? param, CommandType type, IDbTransaction transaction) =>
		R.Wrap(
			(query, parameters: param ?? new object(), type)
		)
		.Audit(
			ok: LogQuery<T>
		)
		.BindAsync(
			x => Client.Adapter.ExecuteAsync<T>(transaction, x.query, x.parameters, x.type)
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

	#endregion Querying
}
