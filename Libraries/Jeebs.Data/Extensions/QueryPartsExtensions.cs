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
		/// Get Query from QueryParts
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="parts">QueryParts</param>
		/// <param name="unitOfWork">IUnitOfWork</param>
		public static Query<T> GetQuery<T>(this QueryParts<T> parts, IUnitOfWork unitOfWork) => new Query<T>(unitOfWork, parts);
	}
}
