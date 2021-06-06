// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Data;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IUnitOfWork"/>
	public sealed class UnitOfWork : IUnitOfWork
	{
		/// <inheritdoc/>
		public IDbTransaction Transaction { get; private init; }

		private ILog Log { get; init; }

		/// <summary>
		/// Inject a transaction
		/// </summary>
		/// <param name="transaction">Transaction</param>
		/// <param name="log">ILog</param>
		public UnitOfWork(IDbTransaction transaction, ILog log) =>
			(Transaction, Log) = (transaction, log);

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
		/// Commits and disposes of <see cref="Transaction"/> object
		/// </summary>
		public void Dispose()
		{
			Commit();
			Transaction.Dispose();
		}
	}
}
