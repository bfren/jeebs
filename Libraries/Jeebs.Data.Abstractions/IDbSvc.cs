using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <summary>
	/// Db Service
	/// </summary>
	/// <typeparam name="TDb">IDb</typeparam>
	public interface IDbSvc<TDb>
		where TDb : IDb
	{
		/// <summary>
		/// IDb
		/// </summary>
		public TDb Db { get; }

		/// <summary>
		/// Retrieve items from the database into a PagedList
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="page">Current page number</param>
		/// <param name="args">QueryArgs</param>
		public Task<Result<PagedList<T>>> RetrieveAsync<T>(long page, QueryArgs<T> args);
	}
}
