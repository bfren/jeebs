// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;
using System.Data;

namespace Jeebs.Data
{
	/// <summary>
	/// Database unit of work
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Database transaction for this Unit of Work
		/// </summary>
		IDbTransaction Transaction { get; }

		/// <summary>
		/// Commits all queries - should normally be called as part of Dispose()
		/// </summary>
		void Commit();

		/// <summary>
		/// Rollback all queries
		/// </summary>
		void Rollback();
	}
}
