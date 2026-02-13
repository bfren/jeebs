// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Apps.Web.Constants;
using Jeebs.Logging;
using Jeebs.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Wrap;
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

	[HttpGet("/id/{testId}")]
	public IActionResult Id(TestId testId) =>
		View("Monad", testId);

	[HttpGet("/postcode/{postcode}")]
	public IActionResult Postcode(Postcode postcode) =>
		View("Monad", postcode);

	[HttpGet("/maybe/{maybe?}")]
	public IActionResult Maybe(Maybe<bool> maybe) =>
		View("Monad", maybe);
}

public sealed record class TestId : LongId<TestId>;

public sealed record class Postcode : Monad<Postcode, string>;
