// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;
using Jeebs.Messages;
using Jeebs.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AppMvc.Controllers;

public class ErrorController : Jeebs.Mvc.Controllers.ErrorController
{
	public ErrorController(ILog log) : base(log) { }

	public IActionResult ThrowException() =>
		throw new Exception("Something");

	public async Task<IActionResult> ReturnError() =>
		await this.ExecuteErrorAsync(new TestErrorMsg()).ConfigureAwait(false);

	public IActionResult ReturnNotFound() =>
		NotFound();

	public IActionResult ReturnUnauthorised() =>
		Unauthorized();

	public IActionResult ReturnForbidden() =>
		NotAllowed();

	public async Task<IActionResult> ReturnError404() =>
		await this.ExecuteErrorAsync(new NotFoundMsg(42)).ConfigureAwait(false);

	public record class NotFoundMsg(int Value) : NotFoundMsg<int>
	{
		public override string Name =>
			"Code";
	}
}

public record class TestErrorMsg : Msg;
