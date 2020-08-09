using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
		/// <returns>IActionResult</returns>
		public async Task<IActionResult> Index()
			=> await this.ExecuteErrorAsync(R.Error(), StatusCodes.Status500InternalServerError);

		/// <summary>
		/// Execute error view
		/// </summary>
		/// <param name="code">Error code</param>
		/// <returns>IActionResult</returns>
		public async Task<IActionResult> Execute(int? code)
			=> await this.ExecuteErrorAsync(R.Error(), code ?? StatusCodes.Status500InternalServerError);
	}
}
