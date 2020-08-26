using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc
{
	/// <summary>
	/// Simple Controller
	/// </summary>
	public abstract class SimpleController : Microsoft.AspNetCore.Mvc.Controller
	{
		/// <summary>
		/// Disable favicon.ico
		/// </summary>
		[Route("favicon.ico")]
		public EmptyResult Favicon()
			=> new EmptyResult();

		/// <summary>
		/// Keep alive page
		/// </summary>
		[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
		[Route("keep-alive")]
		public ContentResult KeepAlive()
			=> Content(DateTime.UtcNow.ToString("u"), "text/plain");

		/// <summary>
		/// Robots.txt file
		/// </summary>
		[Route("robots.txt")]
		public ContentResult RobotsTxt()
			=> Content("User-agent: * Allow: /", "text/plain");
	}
}
