using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Query wrapper
	/// </summary>
	public class Query : IDisposable
	{
		/// <summary>
		/// IUnitOfWork
		/// </summary>
		protected IUnitOfWork UnitOfWork;

		/// <summary>
		/// Create <see cref="UnitOfWork"/>
		/// </summary>
		/// <param name="db">IDb</param>
		public Query(IDb db) => UnitOfWork = db.UnitOfWork;

		/// <summary>
		/// Dispose <see cref="UnitOfWork"/>
		/// </summary>
		public void Dispose() => UnitOfWork.Dispose();
	}
}
