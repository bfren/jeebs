using System;
using Jeebs.Data;

namespace Jeebs.Mvc.Controllers
{
	/// <summary>
	/// Controller class
	/// </summary>
	/// <typeparam name="TDb">IDb</typeparam>
	public abstract class Controller<TDb> : Controller
		where TDb : IDb
	{
		/// <summary>
		/// TDb
		/// </summary>
		protected TDb Db { get; }

		/// <summary>
		/// Current page number
		/// </summary>
		public long Page => long.TryParse(Request.Query["p"], out long p) ? p : 1;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="log">ILog</param>
		/// <param name="db">TDb</param>
		protected Controller(ILog log, TDb db) : base(log) => Db = db;
	}
}
