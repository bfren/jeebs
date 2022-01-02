// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace AppMvc.Controllers;

public class ErrorController : Jeebs.Mvc.ErrorController
{
	public ErrorController(ILog log) : base(log) { }

	public IActionResult Throw_Exception() =>
		throw new Exception("Something");

	public async Task<IActionResult> Return_Error() =>
		await this.ExecuteErrorAsync(new TestErrorMsg()).ConfigureAwait(false);

	public IActionResult Return_NotFound() =>
		NotFound();

	public IActionResult Return_Unauthorised() =>
		Unauthorized();

	public IActionResult Return_Forbidden() =>
		NotAllowed();

	public async Task<IActionResult> Return_Error404() =>
		await this.ExecuteErrorAsync(new NotFoundMsg(42)).ConfigureAwait(false);

	public record class NotFoundMsg(int Value) : NotFoundMsg<int>
	{
		public override string Name =>
			"Code";
	}
}

public record class TestErrorMsg : Msg;
