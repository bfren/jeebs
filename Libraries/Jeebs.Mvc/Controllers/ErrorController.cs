using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Apps.WebApps.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Jeebs.Mvc
{
	/// <summary>
	/// Error Controller
	/// </summary>
	public abstract class ErrorController : Controller
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="log">ILog</param>
		protected ErrorController(ILog log) : base(log) { }

		[Route("/Error/{code:int}")]
		public async Task<IActionResult> Handle(int code)
			=> await this.ExecuteErrorAsync(R.Error(), code);

		/// <summary>
		/// Default error view
		/// </summary>
		/// <returns>IActionResult</returns>
		public async Task<IActionResult> Index()
			=> await Handle(StatusCodes.Status500InternalServerError);
	}
}
