using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
		protected ILog Log { get; }

		/// <summary>
		/// Current page number
		/// </summary>
		public long Page => long.TryParse(Request.Query["p"], out long p) ? p : 1;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="log">ILog</param>
		protected Controller(ILog log) => Log = log;

		/// <summary>
		/// Redirect to error page
		/// </summary>
		/// <param name="code">HTTP Status Code</param>
		protected RedirectToActionResult RedirectToError(int code = 500)
		{
			return new RedirectToActionResult(nameof(ErrorController.Execute), "Error", new { code });
		}

		/// <summary>
		/// Do something, process the result and return errors if necessary, or perform the success function
		/// </summary>
		/// <typeparam name="T">Result type</typeparam>
		/// <param name="r">The result of some action</param>
		/// <param name="success">Function to run when the result is successful</param>
		protected IActionResult ProcessResult<T>(IR<T> r, Func<T, IActionResult> success) => r switch
		{
			IOkV<T> okV => success(okV.Val),
			IError<T> error => HandleError(error),
			{ } other => HandleError(other.Error<Jm.Mvc.Controller_ProcessResult_Unknown_IR>())
		};

		/// <inheritdoc cref="ProcessResult{T}(IR{T}, Func{T, IActionResult})"/>
		protected async Task<IActionResult> ProcessResultAsync<T>(IR<T> r, Func<T, Task<IActionResult>> success) => r switch
		{
			IOkV<T> okV => await success(okV.Val).ConfigureAwait(false),
			IError<T> error => HandleError(error),
			{ } other => HandleError(other.Error<Jm.Mvc.Controller_ProcessResult_Unknown_IR>())
		};

		/// <summary>
		/// Handle a process error
		/// </summary>
		/// <typeparam name="T">Result type</typeparam>
		/// <param name="error">Error result</param>
		private IActionResult HandleError<T>(IError<T> error)
		{
			if (error.Messages.Contains<Jm.NotFound>())
			{
				return NotFound();
			}

			Log.Warning("Error while processing controller action: {0}", error.Messages);
			return RedirectToError();
		}
	}
}
