// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data.Common;
using System.Threading.Tasks;
using Jeebs.Logging;

namespace Jeebs.Data.Common;

/// <inheritdoc cref="IUnitOfWork"/>
public sealed class UnitOfWork : IUnitOfWork
{
	private readonly ILog log;

	/// <summary>
	/// Null while work is pending, true once committed, false once rolled back.
	/// </summary>
	private bool? committed;

	/// <inheritdoc/>
	public DbConnection Connection { get; }

	/// <inheritdoc/>
	public LazyAsync<DbTransaction> Transaction { get; }

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="connection">Open database connection.</param>
	/// <param name="transaction">Begins the transaction the first time it is used.</param>
	/// <param name="log">ILog.</param>
	public UnitOfWork(DbConnection connection, LazyAsync<DbTransaction> transaction, ILog log) =>
		(Connection, Transaction, this.log) = (connection, transaction, log);

	/// <inheritdoc/>
	public async Task<Result<bool>> CommitAsync()
	{
		// nothing to commit: no transaction has begun (stay pending for later writes), or it is already
		// committed (a repeat commit, e.g. by DoWorkAsync) - but a commit after a rollback fails
		if (committed is not null || !Transaction.Initialised)
		{
			return committed switch
			{
				false =>
					R.Fail("No pending transaction to commit.")
						.Ctx(nameof(UnitOfWork), nameof(CommitAsync)),

				_ =>
					R.True
			};
		}

		try
		{
			log.Vrb("Committing transaction.");
			await (await Transaction).CommitAsync();
			committed = true;
			return R.True;
		}
		catch (Exception ex)
		{
			log.Err(ex, "Error committing transaction.");
			await RollbackAsync();
			return R.Fail(ex)
				.Ctx(nameof(UnitOfWork), nameof(CommitAsync));
		}
	}

	/// <inheritdoc/>
	public Task<Result<T>> CommitAsync<T>(T value) =>
		CommitAsync().MapAsync(_ => value);

	/// <inheritdoc/>
	public async Task RollbackAsync()
	{
		// nothing to roll back: already finished, or no transaction has begun (stay pending for later writes)
		if (committed is not null || !Transaction.Initialised)
		{
			return;
		}

		try
		{
			log.Dbg("Rolling back transaction.");
			await (await Transaction).RollbackAsync();
		}
		catch (Exception ex)
		{
			log.Err(ex, "Error rolling back transaction.");
		}
		finally
		{
			committed = false;
		}
	}

	/// <summary>
	/// Rolls back anything not committed, then disposes the transaction (if one was begun) and the connection.
	/// </summary>
	public async ValueTask DisposeAsync()
	{
		await RollbackAsync();

		// a transaction that failed to begin has nothing to dispose
		if (Transaction is { Initialised: true, Value.IsCompletedSuccessfully: true })
		{
			await Transaction.Value.Result.DisposeAsync();
		}

		await Connection.DisposeAsync();
	}
}
