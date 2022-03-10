// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Logging;
using Jeebs.Mvc.Controllers;

namespace Jeebs.Mvc.Data.Controllers;

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
