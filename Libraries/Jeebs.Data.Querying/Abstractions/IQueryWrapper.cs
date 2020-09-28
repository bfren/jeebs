using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// Disposable Query Wrapper - implementations should start a new UnitOfWork as it is created, which can then be disposed
	/// </summary>
	public interface IQueryWrapper : IDisposable
	{
		/// <summary>
		/// Start a new Query using the current UnitOfWork
		/// </summary>
		IQueryBuilder StartNewQuery();
	}
}
