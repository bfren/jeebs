// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs;
using Jeebs.Auth;
using Jeebs.Auth.Data;
using Jeebs.Data.Querying;
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
