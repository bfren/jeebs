// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Jeebs.Data.Common;

/// <summary>
/// Database unit of work.
/// </summary>
public interface IUnitOfWork : IAsyncDisposable
{
	/// <summary>
	/// Open database connection for this Unit of Work.
	/// </summary>
	DbConnection Connection { get; }

	/// <summary>
	/// Database transaction for this Unit of Work - it is only begun the first time it is used,
	/// so work that never writes has no transaction to commit or roll back.
	/// </summary>
	LazyAsync<DbTransaction> Transaction { get; }

	/// <summary>
	/// Commits all queries - must be called to save work, as DisposeAsync() rolls back anything not committed.
	/// Calling it again after a successful commit returns true; calling it after a rollback fails.
	/// </summary>
	/// <returns>True if the commit succeeded, or a failure (after rolling back) if it did not.</returns>
	Task<Result<bool>> CommitAsync();

	/// <inheritdoc cref="CommitAsync()"/>
	/// <typeparam name="T">Value type.</typeparam>
	/// <param name="value">Value to return if the commit succeeds.</param>
	/// <returns><paramref name="value"/> if the commit succeeded, or a failure (after rolling back) if it did not.</returns>
	Task<Result<T>> CommitAsync<T>(T value);

	/// <summary>
	/// Rollback all queries.
	/// </summary>
	Task RollbackAsync();
}
