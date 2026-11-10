// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data;
using Jeebs.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Jeebs.Mvc.Auth.Controllers.AuthControllerBase_Tests;

public static class Setup
{
	public static (TestController controller, IAuthDataProvider auth, IUrlHelper url) Get()
	{
		var auth = Substitute.For<IAuthDataProvider>();
		var url = Substitute.For<IUrlHelper>();
		var http = new DefaultHttpContext();
		var controller = new TestController(auth, Substitute.For<ILog>())
		{
			ControllerContext = new() { HttpContext = http },
			TempData = new TempDataDictionary(http, Substitute.For<ITempDataProvider>()),
			Url = url
		};
		return (controller, auth, url);
	}

	public sealed class TestController(IAuthDataProvider auth, ILog log) : AuthControllerBase(auth, log)
	{
		public string? GetSignOutRedirect() =>
			SignOutRedirect();
	}
}
