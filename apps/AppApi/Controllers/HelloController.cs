// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc;

namespace AppApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HelloController : ControllerBase
	{
		[HttpGet]
		public string Get() =>
			"Hello, world!";
	}
}
