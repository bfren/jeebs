// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Data.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Apps;

/// <summary>
/// MVC Application bootstrapped using IHost - with Data access enabled
/// </summary>
public abstract class MvcAppWithData : MvcApp
{
	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
	protected MvcAppWithData(bool useHsts) : base(useHsts) { }

	/// <inheritdoc/>
	protected override void ConfigureServicesMvcOptions(MvcOptions opt)
	{
		base.ConfigureServicesMvcOptions(opt);

		// Add custom model binders
		opt.ModelBinderProviders.Insert(0, new StrongIdModelBinderProvider());
	}
}
