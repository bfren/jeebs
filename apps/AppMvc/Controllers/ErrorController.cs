// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;
using Jeebs.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AppMvc.Controllers;

public class ErrorController(ILog log) : Jeebs.Mvc.Controllers.ErrorController(log)
{
	public IActionResult ThrowException() =>
		throw new Exception("Something");

	public async Task<IActionResult> ReturnError() =>
		await this.ExecuteErrorAsync(new("Test error."));

	public IActionResult ReturnNotFound() =>
		NotFound();

	public IActionResult ReturnUnauthorised() =>
		Unauthorized();

	public IActionResult ReturnForbidden() =>
		NotAllowed();

	public async Task<IActionResult> ReturnError404() =>
		await this.ExecuteErrorAsync(new("Not found."), 404);
}
