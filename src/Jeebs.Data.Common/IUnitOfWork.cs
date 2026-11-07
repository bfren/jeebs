// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data;
using System.Threading.Tasks;

namespace Jeebs.Data.Common;

/// <summary>
/// Database unit of work.
/// </summary>
public interface IUnitOfWork : IAsyncDisposable, IDisposable
{
	/// <summary>
	/// Database connection for this Unit of Work.
	/// </summary>
	IDbConnection Connection { get; }

	/// <summary>
	/// Database transaction for this Unit of Work.
	/// </summary>
	IDbTransaction Transaction { get; }

	/// <inheritdoc cref="CommitAsync()"/>
	Result<bool> Commit();

	/// <summary>
	/// Commits all queries - must be called to save work, as Dispose() rolls back anything not committed.
	/// </summary>
	/// <returns>True if the commit succeeded, or a failure (after rolling back) if it did not.</returns>
	Task<Result<bool>> CommitAsync();

	/// <inheritdoc cref="CommitAsync()"/>
	/// <typeparam name="T">Value type.</typeparam>
	/// <param name="value">Value to return if the commit succeeds.</param>
	/// <returns><paramref name="value"/> if the commit succeeded, or a failure (after rolling back) if it did not.</returns>
	Task<Result<T>> CommitAsync<T>(T value);

	/// <inheritdoc cref="RollbackAsync"/>
	void Rollback();

	/// <summary>
	/// Rollback all queries.
	/// </summary>
	Task RollbackAsync();
}
