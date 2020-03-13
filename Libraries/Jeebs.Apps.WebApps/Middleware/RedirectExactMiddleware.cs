using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Apps.WebApps.Config;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Jeebs.Apps.WebApps.Middleware
{
	/// <summary>
	/// Redirect Exact Middleware
	/// </summary>
	public sealed class RedirectExactMiddleware
	{
		/// <summary>
		/// The next request in the pipeline
		/// </summary>
		private readonly RequestDelegate next;

		/// <summary>
		/// ILogger
		/// </summary>
		private readonly ILogger logger = Log.ForContext<RedirectExactMiddleware>();

		/// <summary>
		/// Registered redirections
		/// </summary>
		private readonly RedirectionsConfig redirections;

		/// <summary>
		/// Construct object
		/// </summary>
		/// <param name="next">RequestDelegate object</param>
		/// <param name="redirections">Redirections</param>
		public RedirectExactMiddleware(RequestDelegate next, RedirectionsConfig redirections)
		{
			this.next = next;
			this.redirections = redirections;
		}

		/// <summary>
		/// Invoke middleware and perform any redirections
		/// </summary>
		/// <param name="context">HttpContext object</param>
		/// <returns>Task</returns>
		public async Task Invoke(HttpContext context)
		{
			// Get current path
			var req = context.Request;
			var currentPath = req.Path.ToString();

			// If the current path matches a redirection, go there
			if (redirections.ContainsKey(currentPath) && redirections[currentPath] is string redirectToPath)
			{
				// Build redirection URL and log action
				var url = $"{req.Scheme}://{req.Host}{redirectToPath}";
				logger.Information("Redirecting from '{UrlFrom}' to '{UrlTo}'.");

				// Perform the (permanent) redirect
				context.Response.Redirect(url, permanent: true);
			}
			else
			{
				// No redirections match so move on to the next delegate
				await next.Invoke(context);
			}
		}

		#region For Testing

		/// <summary>
		/// Construct object
		/// </summary>
		/// <param name="next">RequestDelegate object</param>
		/// <param name="redirections">Redirections</param>
		/// <param name="logger">ILogger</param>
		internal RedirectExactMiddleware(RequestDelegate next, RedirectionsConfig redirections, ILogger logger) : this(next, redirections)
		{
			this.logger = logger;
		}

		/// <summary>
		/// Construct an object for testing
		/// </summary>
		/// <param name="next">RequestDelegate</param>
		/// <param name="logger">ILogger</param>
		public static RedirectExactMiddleware CreateForTesting(RequestDelegate next, RedirectionsConfig redirections, ILogger logger)
			=> new RedirectExactMiddleware(next, redirections, logger);

		#endregion
	}
}
