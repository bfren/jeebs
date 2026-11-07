// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Jeebs.Logging;

namespace Jeebs.Data.Common;

/// <inheritdoc cref="IUnitOfWork"/>
public sealed class UnitOfWork : IUnitOfWork
{
	private readonly ILog log;

	private bool pending = true;

	/// <summary>
	/// DbConnection object to allow access to async methods.
	/// </summary>
	private readonly DbConnection connection;

	IDbConnection IUnitOfWork.Connection =>
		connection;

	/// <summary>
	/// DbTransaction object to allow access to async methods.
	/// </summary>
	private readonly DbTransaction transaction;

	IDbTransaction IUnitOfWork.Transaction =>
		transaction;

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="connection">Database Connection wrapper.</param>
	/// <param name="transaction">Database Transaction wrapper.</param>
	/// <param name="log">ILog.</param>
	public UnitOfWork(DbConnection connection, DbTransaction transaction, ILog log) =>
		(this.connection, this.transaction, this.log) = (connection, transaction, log);

	/// <inheritdoc/>
	public Result<bool> Commit()
	{
		if (!pending)
		{
			return R.Fail("No pending transaction to commit.")
				.Ctx(nameof(UnitOfWork), nameof(Commit));
		}

		try
		{
			log.Vrb("Committing transaction.");
			transaction.Commit();
			return R.True;
		}
		catch (Exception ex)
		{
			log.Err(ex, "Error committing transaction.");
			Rollback();
			return R.Fail(ex)
				.Ctx(nameof(UnitOfWork), nameof(Commit));
		}
		finally
		{
			pending = false;
		}
	}

	/// <inheritdoc/>
	public async Task<Result<bool>> CommitAsync()
	{
		if (!pending)
		{
			return R.Fail("No pending transaction to commit.")
				.Ctx(nameof(UnitOfWork), nameof(CommitAsync));
		}

		try
		{
			log.Vrb("Committing transaction.");
			await transaction.CommitAsync();
			return R.True;
		}
		catch (Exception ex)
		{
			log.Err(ex, "Error committing transaction.");
			await RollbackAsync();
			return R.Fail(ex)
				.Ctx(nameof(UnitOfWork), nameof(CommitAsync));
		}
		finally
		{
			pending = false;
		}
	}

	/// <inheritdoc/>
	public Task<Result<T>> CommitAsync<T>(T value) =>
		CommitAsync().MapAsync(_ => value);

	/// <inheritdoc/>
	public void Rollback()
	{
		if (!pending)
		{
			return;
		}

		try
		{
			log.Dbg("Rolling back transaction.");
			transaction.Rollback();
		}
		catch (Exception ex)
		{
			log.Err(ex, "Error rolling back transaction.");
		}
		finally
		{
			pending = false;
		}
	}

	/// <inheritdoc/>
	public async Task RollbackAsync()
	{
		if (!pending)
		{
			return;
		}

		try
		{
			log.Dbg("Rolling back transaction.");
			await transaction.RollbackAsync();
		}
		catch (Exception ex)
		{
			log.Err(ex, "Error rolling back transaction.");
		}
		finally
		{
			pending = false;
		}
	}

	/// <inheritdoc cref="DisposeAsync"/>
	public void Dispose()
	{
		if (pending)
		{
			Rollback();
		}

		Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Rolls back anything not committed, then disposes <see cref="transaction"/> and <see cref="connection"/> objects.
	/// </summary>
	public async ValueTask DisposeAsync()
	{
		if (pending)
		{
			await RollbackAsync();
		}

		await transaction.DisposeAsync();
		await connection.DisposeAsync();

		Dispose(false);
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool disposing)
	{
		if (!disposing)
		{
			return;
		}

		transaction.Dispose();
		connection.Dispose();
	}
}
