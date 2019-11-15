using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Unit of Work Factory
	/// </summary>
	internal sealed class UnitOfWorkFactory
	{
		/// <summary>
		/// Create a new UnitOfWork
		/// </summary>
		/// <param name="connection">IDbConnection</param>
		/// <param name="adapter">IAdapter</param>
		/// <param name="log">ILog</param>
		/// <returns>UnitOfWork</returns>
		internal UnitOfWork Create(IDbConnection connection, IAdapter adapter, ILog log) => new UnitOfWork(connection, adapter, log);
	}
}
