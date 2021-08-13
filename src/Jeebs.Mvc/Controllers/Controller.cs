// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
		public ILog Log { get; }

		/// <summary>
		/// Current page number
		/// </summary>
		public ulong Page =>
			ulong.TryParse(Request.Query["p"], out ulong p) switch
			{
				true =>
					p,

				false =>
					1
			};

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="log">ILog</param>
		protected Controller(ILog log) =>
			Log = log;

		/// <summary>
		/// Do something, process the result and return errors if necessary, or perform the success function
		/// </summary>
		/// <typeparam name="T">Result type</typeparam>
		/// <param name="option">Option value</param>
		/// <param name="success">Function to run when the result is successful</param>
		protected Task<IActionResult> ProcessOptionAsync<T>(Option<T> option, Func<T, Task<IActionResult>> success) =>
			option.SwitchAsync(
				some: value =>
					success(value),
				none: reason =>
					this.ExecuteErrorAsync(reason)
			);

		/// <summary>
		/// Do something, process the result and return errors if necessary, or perform the success function
		/// </summary>
		/// <typeparam name="T">Result type</typeparam>
		/// <param name="option">Option value</param>
		/// <param name="success">Function to run when the result is successful</param>
		protected Task<IActionResult> ProcessOptionAsync<T>(Task<Option<T>> option, Func<T, IActionResult> success) =>
			option.SwitchAsync(
				some: value =>
					success(value),
				none: reason =>
					this.ExecuteErrorAsync(reason)
			);

		/// <summary>
		/// Do something, process the result and return errors if necessary, or perform the success function
		/// </summary>
		/// <typeparam name="T">Result type</typeparam>
		/// <param name="option">Option value</param>
		/// <param name="success">Function to run when the result is successful</param>
		protected Task<IActionResult> ProcessOptionAsync<T>(Task<Option<T>> option, Func<T, Task<IActionResult>> success) =>
			option.SwitchAsync(
				some: value =>
					success(value),
				none: reason =>
					this.ExecuteErrorAsync(reason)
			);

		/// <inheritdoc cref="ProcessOptionAsync{T}(Option{T}, Func{T, Task{IActionResult}})"/>
		protected IActionResult ProcessOption<T>(Option<T> option, Func<T, IActionResult> success) =>
			option.Switch(
				some: value =>
					success(value),
				none: reason =>
					this.ExecuteErrorAsync(reason).GetAwaiter().GetResult()
			);

		/// <summary>
		/// Redirect to error page
		/// </summary>
		/// <param name="code">HTTP Status Code</param>
		protected static RedirectToActionResult RedirectToError(int code = StatusCodes.Status500InternalServerError) =>
			new(nameof(ErrorController.Handle), "Error", new { code });

		/// <summary>
		/// Return a 403 Not Allowed result
		/// </summary>
		protected StatusCodeResult NotAllowed() =>
			StatusCode(403);
	}
}
