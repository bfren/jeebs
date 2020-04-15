using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Extensions for QueryParts
	/// </summary>
	public static class QueryPartsExtensions
	{
		/// <summary>
		/// Get QueryExec from QueryParts
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="parts">QueryParts</param>
		/// <param name="unitOfWork">IUnitOfWork</param>
		public static QueryExec<T> GetExec<T>(this QueryParts<T> parts, IUnitOfWork unitOfWork) => new QueryExec<T>(unitOfWork, parts);
	}
}
