// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data;

namespace Jeebs.Data;

/// <inheritdoc cref="IUnitOfWork"/>
public sealed class UnitOfWork : IUnitOfWork
{
	/// <inheritdoc/>
	public IDbTransaction Transaction { get; private init; }

	private readonly IDbConnection connection;

	private readonly ILog log;

	/// <summary>
	/// Save connection and start transaction
	/// </summary>
	/// <param name="connection">IDbConnection</param>
	/// <param name="log">ILog</param>
	public UnitOfWork(IDbConnection connection, ILog log) =>
		(Transaction, this.connection, this.log) = (connection.BeginTransaction(), connection, log);

	private bool pending = true;

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
			Transaction.Commit();
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
	public void Rollback()
	{
		if (!pending)
		{
			return;
		}

		try
		{
			log.Dbg("Rolling back transaction.");
			Transaction.Rollback();
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

	/// <summary>
	/// Commits transaction, then disposes <see cref="Transaction"/> and <see cref="connection"/> objects
	/// </summary>
	public void Dispose()
	{
		Commit();
		Transaction.Dispose();
		connection.Dispose();
	}
}
