using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Jm.Mvc.Controllers.Controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc
{
	/// <summary>
	/// Controller class
	/// </summary>
	public abstract class Controller : Microsoft.AspNetCore.Mvc.Controller
	{
		/// <summary>
		/// ILog
		/// </summary>
		internal ILog Log { get; }

		/// <summary>
		/// Current page number
		/// </summary>
		public long Page
			=> long.TryParse(Request.Query["p"], out long p) ? p : 1;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="log">ILog</param>
		protected Controller(ILog log)
			=> Log = log;

		/// <summary>
		/// Do something, process the result and return errors if necessary, or perform the success function
		/// </summary>
		/// <typeparam name="T">Result type</typeparam>
		/// <param name="r">The result of some action</param>
		/// <param name="success">Function to run when the result is successful</param>
		protected async Task<IActionResult> ProcessResultAsync<T>(IR<T> r, Func<T, Task<IActionResult>> success)
			=> r switch
			{
				IOkV<T> okV => await success(okV.Value).ConfigureAwait(false),
				IError<T> error => await this.ExecuteErrorAsync(error),
				{ } other => await this.ExecuteErrorAsync(other.Error<UnknownResultTypeMsg>())
			};

		/// <inheritdoc cref="ProcessResultAsync{T}(IR{T}, Func{T, Task{IActionResult}})"/>
		protected IActionResult ProcessResult<T>(IR<T> r, Func<T, IActionResult> success)
			=> r switch
			{
				IOkV<T> okV => success(okV.Value),
				IError<T> error => this.ExecuteErrorAsync(error).GetAwaiter().GetResult(),
				{ } other => this.ExecuteErrorAsync(other.Error<UnknownResultTypeMsg>()).GetAwaiter().GetResult()
			};

		/// <summary>
		/// Redirect to error page
		/// </summary>
		/// <param name="code">HTTP Status Code</param>
		protected RedirectToActionResult RedirectToError(int code = StatusCodes.Status500InternalServerError)
			=> new RedirectToActionResult(nameof(ErrorController.Handle), "Error", new { code });

		/// <summary>
		/// Return a 403 Not Allowed result
		/// </summary>
		protected StatusCodeResult NotAllowed()
			=> StatusCode(403);
	}
}
