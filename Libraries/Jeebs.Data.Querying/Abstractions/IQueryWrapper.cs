// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// Disposable Query Wrapper - implementations should start a new UnitOfWork as it is created, which can then be disposed
	/// </summary>
	public interface IQueryWrapper : IDisposable
	{
		/// <summary>
		/// IUnitOfWork
		/// </summary>
		IUnitOfWork UnitOfWork { get; }

		/// <summary>
		/// Start a new Query using the current UnitOfWork
		/// </summary>
		IQueryBuilder StartNewQuery();
	}
}
