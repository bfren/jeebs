// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Logging;
using Jeebs.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Controllers;

/// <summary>
/// Error Controller.
/// </summary>
public abstract class ErrorController : MvcController
{
	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="log">ILog.</param>
	protected ErrorController(ILog log) : base(log) { }

	/// <summary>
	/// Default error view.
	/// </summary>
	public async Task<IActionResult> Index() =>
		await Handle(StatusCodes.Status500InternalServerError);

	/// <summary>
	/// Handle an error with the specified code.
	/// </summary>
	/// <param name="code">Http Status Code.</param>
	[Route("/Error/{code:int}")]
	public async Task<IActionResult> Handle(int code) =>
		await this.ExecuteErrorAsync(FailureValue.Create("Unknown error."), code);
}
