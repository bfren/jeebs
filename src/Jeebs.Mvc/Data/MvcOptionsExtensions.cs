// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc;
using StrongId.Mvc;

namespace Jeebs.Mvc.Data;

/// <summary>
/// <see cref="MvcOptions"/> extension methods
/// </summary>
public static class MvcOptionsExtensions
{
	/// <summary>
	/// Insert <see cref="StrongIdModelBinderProvider"/> into the MVC options
	/// </summary>
	/// <param name="this"></param>
	public static void AddStrongIdModelBinding(this MvcOptions @this) =>
		@this.ModelBinderProviders.Insert(0, new StrongIdModelBinderProvider());
}
