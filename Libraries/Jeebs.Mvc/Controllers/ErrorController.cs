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

		/// <summary>
		/// Default error view
		/// </summary>
		public async Task<IActionResult> Index()
			=> await Handle(StatusCodes.Status500InternalServerError);

		/// <summary>
		/// Handle an error with the specified code
		/// </summary>
		/// <param name="code">Http Status Code</param>
		[Route("/Error/{code:int}")]
		public async Task<IActionResult> Handle(int code)
			=> await this.ExecuteErrorAsync(Result.Error(), code);
	}
}
