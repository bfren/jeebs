// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Jeebs.Logging;

namespace Jeebs.Data;

/// <inheritdoc cref="IUnitOfWork"/>
public sealed class UnitOfWork : IUnitOfWork
{
	private readonly ILog log;

	private bool pending = true;

	/// <summary>
	/// DbConnection object to allow access to async methods
	/// </summary>
	private readonly DbConnection connection;

	IDbConnection IUnitOfWork.Connection =>
		connection;

	/// <summary>
	/// DbTransaction object to allow access to async methods
	/// </summary>
	private readonly DbTransaction transaction;

	IDbTransaction IUnitOfWork.Transaction =>
		transaction;

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="connection">Database Connection wrapper</param>
	/// <param name="transaction">Database Transaction wrapper</param>
	/// <param name="log">ILog</param>
	public UnitOfWork(DbConnection connection, DbTransaction transaction, ILog log) =>
		(this.connection, this.transaction, this.log) = (connection, transaction, log);

	/// <inheritdoc/>
	public void Commit()
	{
		if (!pending)
		{
			return;
		}

		try
		{
			log.Vrb("Committing transaction.");
			transaction.Commit();
		}
		catch (Exception ex)
		{
			log.Err(ex, "Error committing transaction.");
			Rollback();
		}
		finally
		{
			pending = false;
		}
	}

	/// <inheritdoc/>
	public async Task CommitAsync()
	{
		if (!pending)
		{
			return;
		}

		try
		{
			log.Vrb("Committing transaction.");
			await transaction.CommitAsync();
		}
		catch (Exception ex)
		{
			log.Err(ex, "Error committing transaction.");
			await RollbackAsync();
		}
		finally
		{
			pending = false;
		}
	}

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
		Commit();
		transaction.Dispose();
		connection.Close();
		connection.Dispose();
	}

	/// <summary>
	/// Commits transaction, then disposes <see cref="transaction"/> and <see cref="connection"/> objects
	/// </summary>
	public async ValueTask DisposeAsync()
	{
		await CommitAsync();
		await transaction.DisposeAsync();
		await connection.CloseAsync();
		await connection.DisposeAsync();
	}
}
