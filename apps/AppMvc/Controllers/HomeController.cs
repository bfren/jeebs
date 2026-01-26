// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Apps.Web.Constants;
using Jeebs.Logging;
using Jeebs.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Wrap.Ids;

namespace MvcApp.Controllers;

#if DEBUG
[ResponseCache(CacheProfileName = CacheProfiles.None)]
#else
[ResponseCache(CacheProfileName = CacheProfiles.Default)]
#endif
public class HomeController(ILog<HomeController> log) : MvcController(log)
{
	public IActionResult Index()
	{
		Log.Inf("Hello, world!");
		return View();
	}

	public IActionResult Privacy() =>
		View();

	public IActionResult Id(TestId testId) =>
		View(testId);
}

public sealed record class TestId : LongId<TestId>;
