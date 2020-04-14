using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Apps.WebApps.Mvc
{
	/// <summary>
	/// Controller class
	/// </summary>
	/// <typeparam name="TApp">MvcApp</typeparam>
	public abstract class Controller : Microsoft.AspNetCore.Mvc.Controller
	{
		/// <summary>
		/// ILog
		/// </summary>
		protected ILog Log { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="log">ILog</param>
		protected Controller(ILog log)
		{
			Log = log;
		}
	}
}
