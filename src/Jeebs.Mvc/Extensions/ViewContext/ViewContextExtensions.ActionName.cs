// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jeebs.Mvc;

public static partial class ViewContextExtensions
{
	/// <summary>
	/// Return the name of the current action.
	/// </summary>
	/// <param name="this">ViewContext object.</param>
	public static string ActionName(this ViewContext @this) =>
		((ControllerActionDescriptor)@this.ActionDescriptor).ActionName ?? "## Unknown ##";
}
