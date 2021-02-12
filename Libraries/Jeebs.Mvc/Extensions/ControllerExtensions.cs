﻿using System;
using System.Collections.Generic;
using System.Text;
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
		/// Execute an error result and return the View
		/// </summary>
		/// <param name="this">Controller</param>
		/// <param name="error">IError</param>
		/// <param name="code">[Optional] HTTP Status Code</param>
		public async static Task<IActionResult> ExecuteErrorAsync(this Controller @this, IError error, int? code = null)
		{
			// Check for 404
			if (code == null)
			{
				code = error.Messages.Contains<Jm.NotFoundMsg>() switch
				{
					true =>
						StatusCodes.Status404NotFound,

					false =>
						StatusCodes.Status500InternalServerError
				};
			}

			// Log errors
			foreach (var item in error.Messages.GetEnumerable())
			{
				@this.Log.Message(item);
			}

			// Look for a view
			var viewName = $"Error{code}";
			@this.Log.Trace("Search for View {ViewName}", viewName);
			if ((findView(viewName) ?? findView("Default")) is string view)
			{
				@this.Log.Trace("Found view {view}", view);
				return @this.View(view, error);
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
				if (@this.HttpContext.RequestServices.GetService<ICompositeViewEngine>() is ICompositeViewEngine viewEngine)
				{
					// Find View
					var viewPath = $"Error/{viewName}";
					var result = viewEngine.FindView(@this.ControllerContext, viewPath, true);

					// Return result
					return result.Success ? viewPath : null;
				}

				return null;
			}
		}
	}
}
