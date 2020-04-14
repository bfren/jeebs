using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <summary>
	/// Database Service
	/// </summary>
	/// <typeparam name="TDb">IDb</typeparam>
	public abstract class DbSvc<TDb> : IDbSvc<TDb>
		where TDb : IDb
	{
		/// <summary>
		/// TDb
		/// </summary>
		public TDb Db { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="db">TDb</param>
		protected DbSvc(TDb db)
		{
			Db = db;
		}

		/// <summary>
		/// Retrieve items from the database into a PagedList
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="page">Current page number</param>
		/// <param name="args">QueryArgs</param>
		public async Task<Result<PagedList<T>>> RetrieveAsync<T>(long page, QueryArgs<T> args)
		{
			// Failure result
			static Result<PagedList<T>> Fail(ErrorList err) => Result.Failure<PagedList<T>>(err);

			// Start UnitOfWork
			using var w = Db.UnitOfWork;

			// Get count
			var countExec = new QueryExec<T>(w, args);
			var countResult = await countExec.Count();
			if (countResult.Err is ErrorList)
			{
				return Fail(countResult.Err);
			}

			// Create PagedList
			var list = new PagedList<T>(page, countResult.Val, args.Limit ?? 10);
			list.Calculate();

			// Set the LIMIT and OFFSET values
			args.Limit = list.ItemsPerPage;
			args.Offset = (list.CurrentPage - 1) * list.ItemsPerPage;

			// Run the query
			var itemsExec = new QueryExec<T>(w, args);
			var itemsResult = await itemsExec.Retrieve();
			if (itemsResult.Err is ErrorList)
			{
				return Fail(itemsResult.Err);
			}

			// Fill list with items and return
			list.AddRange(itemsResult.Val);
			return Result.Success(list);
		}
	}
}
