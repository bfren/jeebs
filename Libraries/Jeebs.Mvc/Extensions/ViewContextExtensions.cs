using System;
using System.Collections.Generic;
using System.Text;
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
		/// <param name="context">ViewContext object</param>
		public static string ControllerName(this ViewContext context) => context.RouteData?.Values["controller"]?.ToString() ?? "## Unknown ##";

		/// <summary>
		/// Return the name of the current action
		/// </summary>
		/// <param name="context">ViewContext object</param>
		public static string ActionName(this ViewContext context) => ((ControllerActionDescriptor)context.ActionDescriptor).ActionName ?? "## Unknown ##";
	}
}
