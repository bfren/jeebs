using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Jeebs.Mvc
{
	/// <summary>
	/// Error Controller
	/// </summary>
	public abstract class ErrorController : Controller
	{
		/// <summary>
		/// ICompositeViewEngine
		/// </summary>
		private readonly ICompositeViewEngine viewEngine;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="log">ILog</param>
		/// <param name="viewEngine">ICompositeViewEngine</param>
		protected ErrorController(ILog log, ICompositeViewEngine viewEngine) : base(log)
			=> this.viewEngine = viewEngine;

		/// <summary>
		/// Default error view
		/// </summary>
		/// <returns>IActionResult</returns>
		public async Task<IActionResult> Index()
			=> await Execute(StatusCodes.Status500InternalServerError).ConfigureAwait(false);

		/// <summary>
		/// Execute error view
		/// </summary>
		/// <param name="code">Error code</param>
		/// <returns>IActionResult</returns>
		public async Task<IActionResult> Execute(int? code)
		{
			// Look for a view
			var viewName = $"Error{code}";
			if ((FindView(viewName) ?? FindView("Default")) is IActionResult view)
			{
				return view;
			}

			// If response has stared we can't do anything
			if (Response.HasStarted)
			{
				Log.Warning("Unable to execute Error view: output already started.");
			}
			else
			{
				Response.Clear();
				Response.StatusCode = StatusCodes.Status500InternalServerError;
				await Response.WriteAsync($"Unable to find views '{viewName}' or 'Default' in Views/Shared/Error/.").ConfigureAwait(false);
			}

			// Return empty result
			return new EmptyResult();
		}

		/// <summary>
		/// Find an Error View
		/// Returns null if the view cannot be found
		/// </summary>
		/// <param name="viewName">The name of the view inside the Error/ subfolder</param>
		private IActionResult? FindView(string viewName)
		{
			var viewPath = $"Error/{viewName}";
			var result = viewEngine.FindView(ControllerContext, viewPath, true);
			return result.Success ? View(viewPath) : null;
		}
	}
}
