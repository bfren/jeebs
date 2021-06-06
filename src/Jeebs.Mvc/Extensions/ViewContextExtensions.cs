// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jeebs.Mvc
{
	/// <summary>
	/// ViewContext Extensions
	/// </summary>
	public static class ViewContextExtensions
	{
		/// <summary>
		/// Return the name of the current controller
		/// </summary>
		/// <param name="this">ViewContext object</param>
		public static string ControllerName(this ViewContext @this) =>
			@this.RouteData?.Values["controller"]?.ToString() ?? "## Unknown ##";

		/// <summary>
		/// Return the name of the current action
		/// </summary>
		/// <param name="this">ViewContext object</param>
		public static string ActionName(this ViewContext @this) =>
			((ControllerActionDescriptor)@this.ActionDescriptor).ActionName ?? "## Unknown ##";
	}
}
