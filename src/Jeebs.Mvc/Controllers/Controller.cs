// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Controllers;

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
		F.ParseUInt64(Request.Query["p"]).Switch<ulong>(
			some: x => x,
			none: _ => 1
		);

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
	/// <param name="maybe">Maybe value</param>
	/// <param name="success">Function to run when the result is successful</param>
	protected Task<IActionResult> ProcessOptionAsync<T>(Maybe<T> maybe, Func<T, Task<IActionResult>> success) =>
		maybe.SwitchAsync(
			some: value =>
				success(value),
			none: reason =>
				this.ExecuteErrorAsync(reason)
		);

	/// <summary>
	/// Do something, process the result and return errors if necessary, or perform the success function
	/// </summary>
	/// <typeparam name="T">Result type</typeparam>
	/// <param name="maybe">Maybe value</param>
	/// <param name="success">Function to run when the result is successful</param>
	protected Task<IActionResult> ProcessOptionAsync<T>(Task<Maybe<T>> maybe, Func<T, IActionResult> success) =>
		maybe.SwitchAsync(
			some: value =>
				success(value),
			none: reason =>
				this.ExecuteErrorAsync(reason)
		);

	/// <summary>
	/// Do something, process the result and return errors if necessary, or perform the success function
	/// </summary>
	/// <typeparam name="T">Result type</typeparam>
	/// <param name="maybe">Maybe value</param>
	/// <param name="success">Function to run when the result is successful</param>
	protected Task<IActionResult> ProcessOptionAsync<T>(Task<Maybe<T>> maybe, Func<T, Task<IActionResult>> success) =>
		maybe.SwitchAsync(
			some: value =>
				success(value),
			none: reason =>
				this.ExecuteErrorAsync(reason)
		);

	/// <inheritdoc cref="ProcessOptionAsync{T}(Maybe{T}, Func{T, Task{IActionResult}})"/>
	protected IActionResult ProcessOption<T>(Maybe<T> maybe, Func<T, IActionResult> success) =>
		maybe.Switch(
			some: value =>
				success(value),
			none: reason =>
				this.ExecuteErrorAsync(reason).GetAwaiter().GetResult()
		);

	/// <summary>
	/// Redirect to error page with HTTP status 500
	/// </summary>
	protected static RedirectToActionResult RedirectToError() =>
		RedirectToError(StatusCodes.Status500InternalServerError);

	/// <summary>
	/// Redirect to error page
	/// </summary>
	/// <param name="code">HTTP Status Code</param>
	protected static RedirectToActionResult RedirectToError(int code) =>
		new(nameof(ErrorController.Handle), "Error", new { code });

	/// <summary>
	/// Return a 403 Not Allowed result
	/// </summary>
	protected StatusCodeResult NotAllowed() =>
		StatusCode(403);
}
