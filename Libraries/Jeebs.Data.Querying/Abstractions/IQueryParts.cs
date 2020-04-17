using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Query Parts interface
	/// </summary>
	/// <typeparam name="T">Model type</typeparam>
	public interface IQueryParts<T> : IQueryParts
	{
		/// <summary>
		/// Query Parameters
		/// </summary>
		IQueryParameters Parameters { get; set; }

		/// <summary>
		/// Get Query
		/// </summary>
		/// <param name="unitOfWork">IUnitOfWork</param>
		IQuery<T> GetQuery(IUnitOfWork unitOfWork);
	}
}
