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
		/// <param name="result">The result of some action</param>
		/// <param name="success">Function to run when the result is successful</param>
		protected IActionResult ProcessResult<T>(IResult<T> result, Func<T, IActionResult> success)
		{
			if(result.Err is IErrorList err)
			{
				if(err.NotFound)
				{
					return NotFound();
				}

				Log.Warning("Error while processing controller action: {0}", err);
				return RedirectToError();
			}

			return success(result.Val);
		}

		/// <inheritdoc cref="ProcessResult{T}(IResult{T}, Func{T, IActionResult})"/>
		protected async Task<IActionResult> ProcessResultAsync<T>(IResult<T> result, Func<T, Task<IActionResult>> success)
		{
			if(result.Err is IErrorList err)
			{
				if(err.NotFound)
				{
					return NotFound();
				}

				Log.Warning("Error while processing controller action: {0}", err);
				return RedirectToError();
			}

			return await success(result.Val);
		}
	}
}
