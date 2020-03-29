using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Extensions for QueryArgs
	/// </summary>
	public static class QueryArgsExtensions
	{
		/// <summary>
		/// Get QueryExec from QueryArgs
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="args">QueryArgs</param>
		/// <param name="unitOfWork">IUnitOfWork</param>
		public static QueryExec<T> GetExec<T>(this QueryArgs<T> args, IUnitOfWork unitOfWork) => new QueryExec<T>(unitOfWork, args);
	}
}
