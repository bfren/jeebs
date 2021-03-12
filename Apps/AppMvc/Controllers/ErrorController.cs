// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace AppMvc.Controllers
{
	public class ErrorController : Jeebs.Mvc.ErrorController
	{
		public ErrorController(ILog log) : base(log) { }

		public IActionResult Throw_Exception() =>
			throw new Exception("Something");

		public async Task<IActionResult> Return_Error() =>
			await this.ExecuteErrorAsync(new TestErroMsg());

		public IActionResult Return_NotFound() =>
			NotFound();

		public IActionResult Return_Unauthorised() =>
			Unauthorized();

		public IActionResult Return_Forbidden() =>
			NotAllowed();

		public async Task<IActionResult> Return_Error404() =>
			await this.ExecuteErrorAsync(new NotFoundMsg());

		public class NotFoundMsg : INotFoundMsg { }
	}

	public record TestErroMsg : IMsg { }
}
