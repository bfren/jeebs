using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Query Wrapper interface
	/// </summary>
	public interface IQueryWrapper : IDisposable
	{
		/// <summary>
		/// Start a new Query using the current UnitOfWork
		/// </summary>
		IQueryBuilder StartNewQuery();
	}
}
