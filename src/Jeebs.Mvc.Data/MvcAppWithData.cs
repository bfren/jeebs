// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Apps.Web;
using Jeebs.Mvc.Data.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Data;

/// <summary>
/// MVC Application bootstrapped using IHost - with Data access enabled
/// </summary>
public class MvcAppWithData : MvcApp
{
	/// <summary>
	/// Create MVC app with data and HSTS enabled
	/// </summary>
	public MvcAppWithData() : this(true) { }

	/// <summary>
	/// Create MVC app with data
	/// </summary>
	/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
	public MvcAppWithData(bool useHsts) : base(useHsts) { }

	/// <inheritdoc/>
	protected override void ConfigureServicesMvcOptions(MvcOptions opt)
	{
		base.ConfigureServicesMvcOptions(opt);

		// Add custom model binders
		opt.ModelBinderProviders.Insert(0, new StrongIdModelBinderProvider());
	}
}
