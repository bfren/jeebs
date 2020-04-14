using System;
using System.Collections.Generic;
using System.Text;
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
		/// <returns>Name of the current controller</returns>
		public static string ControllerName(this ViewContext context) => context.RouteData?.Values["controller"]?.ToString() ?? "## Unknown ##";
	}
}
