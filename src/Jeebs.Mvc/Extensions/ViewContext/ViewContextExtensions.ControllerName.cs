// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jeebs.Mvc;

public static partial class ViewContextExtensions
{
	/// <summary>
	/// Return the name of the current controller.
	/// </summary>
	/// <param name="this">ViewContext object.</param>
	public static string ControllerName(this ViewContext @this) =>
		@this.RouteData?.Values["controller"]?.ToString() ?? "## Unknown ##";
}
