// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data;
using System.Threading.Tasks;

namespace Jeebs.Data;

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

	/// <inheritdoc cref="CommitAsync"/>
	void Commit();

	/// <summary>
	/// Commits all queries - should normally be called as part of Dispose().
	/// </summary>
	Task CommitAsync();

	/// <inheritdoc cref="RollbackAsync"/>
	void Rollback();

	/// <summary>
	/// Rollback all queries.
	/// </summary>
	Task RollbackAsync();
}
