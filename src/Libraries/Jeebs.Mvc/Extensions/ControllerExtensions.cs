// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Mvc
{
	/// <summary>
	/// Controller extension methods
	/// </summary>
	public static class ControllerExtensions
	{
		/// <summary>
		/// Execute an Option.None result and return the View
		/// </summary>
		/// <param name="this">Controller</param>
		/// <param name="reason">None</param>
		/// <param name="code">[Optional] HTTP Status Code</param>
		public async static Task<IActionResult> ExecuteErrorAsync(this Controller @this, IMsg reason, int? code = null)
		{
			// Log error
			@this.Log.Message(reason);

			// Check for 404
			var status = code switch
			{
				int x =>
					x,

				_ =>
					reason switch
					{
						INotFoundMsg =>
							StatusCodes.Status404NotFound,

						_ =>
							StatusCodes.Status500InternalServerError
					}
			};

			// Look for a view
			var viewName = $"Error{status}";
			@this.Log.Verbose("Search for View {ViewName}", viewName);
			if ((findView(viewName) ?? findView("Default")) is string view)
			{
				@this.Log.Verbose("Found view {view}", view);
				return @this.View(view, reason);
			}

			// If response has stared we can't do anything
			var unableToFindViews = $"Unable to find views '{viewName}' or 'Default'.";
			@this.Log.Warning(unableToFindViews);

			if (!@this.Response.HasStarted)
			{
				@this.Response.Clear();
				@this.Response.StatusCode = StatusCodes.Status500InternalServerError;
				await @this.Response.WriteAsync(unableToFindViews).ConfigureAwait(false);
			}

			// Return empty result
			return new EmptyResult();

			// Find a view
			string? findView(string viewName)
			{
				// Get View Engine
				var viewEngine = @this.HttpContext.RequestServices.GetService<ICompositeViewEngine>();
				if (viewEngine == null)
				{
					return null;
				}

				// Find View
				var viewPath = $"Error/{viewName}";
				var result = viewEngine.FindView(@this.ControllerContext, viewPath, true);

				// Return result
				return result.Success ? viewPath : null;
			}
		}
	}
}
