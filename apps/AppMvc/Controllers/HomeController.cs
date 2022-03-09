// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;
using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Controllers;

public class HomeController : Jeebs.Mvc.Controllers.Controller
{
	public HomeController(ILog<HomeController> log) : base(log) { }

	public IActionResult Index()
	{
		Log.Inf("Hello, world!");
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}
}
