using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Database
	/// </summary>
	public interface IDb
	{
		/// <summary>
		/// Create IUnitOfWork
		/// </summary>
		IUnitOfWork UnitOfWork { get; }
	}
}
