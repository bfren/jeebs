// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
