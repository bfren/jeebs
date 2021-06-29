// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Data;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IUnitOfWork"/>
	public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly IDbConnection connection;

		/// <inheritdoc/>
		public IDbTransaction Transaction { get; private init; }

		private ILog Log { get; init; }

		/// <summary>
		/// Save connection and start transaction
		/// </summary>
		/// <param name="connection">IDbConnection</param>
		/// <param name="log">ILog</param>
		public UnitOfWork(IDbConnection connection, ILog log) =>
			(this.connection, Transaction, Log) = (connection, connection.BeginTransaction(), log);

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
				Log.Debug("Committing transaction.");
				Transaction.Commit();
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error committing transaction.");
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
				Log.Debug("Rolling back transaction.");
				Transaction.Rollback();
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Error rolling back transaction.");
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
}
