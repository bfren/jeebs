// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.Mvc
{
	/// <summary>
	/// Controller class for interacting with data
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
		/// Create object
		/// </summary>
		/// <param name="log">ILog</param>
		/// <param name="db">TDb</param>
		protected Controller(ILog log, TDb db) : base(log) =>
			Db = db;
	}
}
