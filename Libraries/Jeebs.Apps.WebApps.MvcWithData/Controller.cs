using System;
using Jeebs.Data;

namespace Jeebs.Apps.WebApps.Mvc
{
	/// <summary>
	/// Controller class
	/// </summary>
	/// <typeparam name="TDb">IDb</typeparam>
	/// <typeparam name="TDbSvc">IDbSvc</typeparam>
	public abstract class Controller<TDb, TDbSvc> : Controller
		where TDb : IDb
		where TDbSvc : IDbSvc<TDb>
	{
		/// <summary>
		/// TDbSvc
		/// </summary>
		protected TDbSvc Svc { get; }

		/// <summary>
		/// Current page number
		/// </summary>
		public int Page => int.TryParse(Request.Query["p"], out int p) ? p : 1;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="log">ILog</param>
		/// <param name="svc">TDbSvc</param>
		protected Controller(ILog log, TDbSvc svc) : base(log) => Svc = svc;
	}
}
