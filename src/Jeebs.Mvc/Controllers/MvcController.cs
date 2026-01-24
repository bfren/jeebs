// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Logging;
using Jeebs.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Controllers;

/// <summary>
/// Controller class.
/// </summary>
public abstract class MvcController : Controller
{
	/// <summary>
	/// ILog.
	/// </summary>
	public ILog Log { get; }

	/// <summary>
	/// Current page number.
	/// </summary>
	public ulong Page =>
		M.ParseUInt64(Request.Query["p"]).Unwrap(() => 1UL);

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="log">ILog.</param>
	protected MvcController(ILog log) =>
		Log = log;

	/// <summary>
	/// Do something, process the result and return errors if necessary, or perform the success function.
	/// </summary>
	/// <typeparam name="T">Result value type.</typeparam>
	/// <param name="result">Result object.</param>
	/// <param name="success">Function to run when the result is successful.</param>
	protected Task<IActionResult> ProcessAsync<T>(Wrap.Result<T> result, Func<T, Task<IActionResult>> success) =>
		result.MatchAsync(
			ok: success,
			fail: this.ExecuteErrorAsync
		);

	/// <summary>
	/// Do something, process the result and return errors if necessary, or perform the success function.
	/// </summary>
	/// <typeparam name="T">Result value type.</typeparam>
	/// <param name="result">Result object.</param>
	/// <param name="success">Function to run when the result is successful.</param>
	protected Task<IActionResult> ProcessAsync<T>(Task<Wrap.Result<T>> result, Func<T, IActionResult> success) =>
		result.MatchAsync(
			ok: success,
			fail: this.ExecuteErrorAsync
		);

	/// <summary>
	/// Do something, process the result and return errors if necessary, or perform the success function.
	/// </summary>
	/// <typeparam name="T">Result value type.</typeparam>
	/// <param name="result">Result object.</param>
	/// <param name="success">Function to run when the result is successful.</param>
	protected Task<IActionResult> ProcessAsync<T>(Task<Wrap.Result<T>> result, Func<T, Task<IActionResult>> success) =>
		result.MatchAsync(
			ok: success,
			fail: this.ExecuteErrorAsync
		);

	/// <inheritdoc cref="ProcessAsync{T}(Task{Wrap.Result{T}}, Func{T, Task{IActionResult}})"/>
	protected IActionResult Process<T>(Wrap.Result<T> result, Func<T, IActionResult> success) =>
		result.Match(
			ok: success,
			fail: f => this.ExecuteErrorAsync(f).GetAwaiter().GetResult()
		);

	/// <summary>
	/// Redirect to error page with HTTP status 500.
	/// </summary>
	protected static RedirectToActionResult RedirectToError() =>
		RedirectToError(StatusCodes.Status500InternalServerError);

	/// <summary>
	/// Redirect to error page.
	/// </summary>
	/// <param name="code">HTTP Status Code.</param>
	protected static RedirectToActionResult RedirectToError(int code) =>
		new(nameof(ErrorController.Handle), "Error", new { code });

	/// <summary>
	/// Return a 403 Not Allowed result.
	/// </summary>
	protected StatusCodeResult NotAllowed() =>
		StatusCode(403);
}
