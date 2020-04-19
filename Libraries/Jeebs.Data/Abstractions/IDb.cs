using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
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
