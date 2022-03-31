// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Apps.Web.Constants;
using Jeebs.Logging;
using Microsoft.AspNetCore.Mvc;
using StrongId;

namespace MvcApp.Controllers;

#if DEBUG
[ResponseCache(CacheProfileName = CacheProfiles.None)]
#else
[ResponseCache(CacheProfileName = CacheProfiles.Default)]
#endif
public class HomeController : Jeebs.Mvc.Controllers.Controller
{
	public HomeController(ILog<HomeController> log) : base(log) { }

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

public sealed record class TestId : LongId;
