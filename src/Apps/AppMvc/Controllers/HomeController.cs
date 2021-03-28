// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;
using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Controllers
{
	public class HomeController : Jeebs.Mvc.Controller
	{
		public HomeController(ILog<HomeController> log) : base(log) { }

		public IActionResult Index()
		{
			Log.Information("Hello, world!");
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}
	}
}
