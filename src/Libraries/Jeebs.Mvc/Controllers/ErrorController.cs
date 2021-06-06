// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc
{
	/// <summary>
	/// Error Controller
	/// </summary>
	public abstract class ErrorController : Controller
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="log">ILog</param>
		protected ErrorController(ILog log) : base(log) { }

		/// <summary>
		/// Default error view
		/// </summary>
		public async Task<IActionResult> Index() =>
			await Handle(StatusCodes.Status500InternalServerError);

		/// <summary>
		/// Handle an error with the specified code
		/// </summary>
		/// <param name="code">Http Status Code</param>
		[Route("/Error/{code:int}")]
		public async Task<IActionResult> Handle(int code) =>
			await this.ExecuteErrorAsync(new Msg.UnknownErrorMsg(), code);

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>An unknown error has occured</summary>
			public sealed record UnknownErrorMsg : IMsg { }
		}
	}
}
