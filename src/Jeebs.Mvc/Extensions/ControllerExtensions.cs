// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Mvc.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Mvc.Extensions;

/// <summary>
/// Controller extension methods.
/// </summary>
public static class ControllerExtensions
{
	/// <summary>
	/// Execute a Maybe.None result and return the View.
	/// </summary>
	/// <param name="this">Controller.</param>
	/// <param name="failure">Failure value.</param>
	public static Task<IActionResult> ExecuteErrorAsync(this MvcController @this, FailureValue failure) =>
		ExecuteErrorAsync(@this, failure, null);

	/// <summary>
	/// Execute a Maybe.None result and return the View.
	/// </summary>
	/// <param name="this">Controller.</param>
	/// <param name="failure">Failure value.</param>
	/// <param name="code">HTTP Status Code.</param>
	public static async Task<IActionResult> ExecuteErrorAsync(this MvcController @this, FailureValue failure, int? code)
	{
		// Log error
		@this.Log.Failure(failure);

		// Use 500 error as default
		var status = code ?? StatusCodes.Status500InternalServerError;

		// Look for a view
		var viewName = $"Error{status}";
		@this.Log.Vrb("Search for View {ViewName}.", viewName);
		if ((findView(viewName) ?? findView("Default")) is string view)
		{
			@this.Log.Vrb("Found view {view}", view);
			return @this.View(view, failure);
		}

		// If response has stared we can't do anything
		var unableToFindViews = "Unable to find view '{Name}' or 'Default'.";
		@this.Log.Wrn(unableToFindViews, viewName);

		if (!@this.Response.HasStarted)
		{
			@this.Response.Clear();
			@this.Response.StatusCode = StatusCodes.Status500InternalServerError;
			await @this.Response.WriteAsync(unableToFindViews);
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
