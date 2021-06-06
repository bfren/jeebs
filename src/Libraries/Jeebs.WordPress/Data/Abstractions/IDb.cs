// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Enables agnostic interaction with a database
	/// </summary>
	public interface IDb
	{
		/// <summary>
		/// Create new IUnitOfWork
		/// </summary>
		IUnitOfWork UnitOfWork { get; }
	}
}
