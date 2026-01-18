// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Mvc.Controllers;

/// <summary>
/// Controller extension methods.
/// </summary>
public static class ControllerExtensions
{
	/// <summary>
	/// Execute a Maybe.None result and return the View.
	/// </summary>
	/// <param name="this">Controller</param>
	/// <param name="msg">None</param>
	public static Task<IActionResult> ExecuteErrorAsync(this Controller @this, IMsg msg) =>
		ExecuteErrorAsync(@this, msg, null);

	/// <summary>
	/// Execute a Maybe.None result and return the View.
	/// </summary>
	/// <param name="this">Controller</param>
	/// <param name="msg">None</param>
	/// <param name="code">HTTP Status Code</param>
	public static async Task<IActionResult> ExecuteErrorAsync(this Controller @this, IMsg msg, int? code)
	{
		// Log error
		if (msg is IMsg m)
		{
			@this.Log.Msg(m);
		}

		// Check for 404
		var status = code switch
		{
			int x =>
				x,

			_ =>
				msg switch
				{
					INotFoundMsg =>
						StatusCodes.Status404NotFound,

					_ =>
						StatusCodes.Status500InternalServerError
				}
		};

		// Look for a view
		var viewName = $"Error{status}";
		@this.Log.Vrb("Search for View {ViewName}", viewName);
		if ((findView(viewName) ?? findView("Default")) is string view)
		{
			@this.Log.Vrb("Found view {view}", view);
			return @this.View(view, msg);
		}

		// If response has stared we can't do anything
		var unableToFindViews = $"Unable to find views '{viewName}' or 'Default'.";
		@this.Log.Wrn(unableToFindViews);

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
			if (viewEngine is null)
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
