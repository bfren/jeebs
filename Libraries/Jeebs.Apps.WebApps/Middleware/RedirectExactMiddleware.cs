// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Threading.Tasks;
using Jeebs.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;

namespace Jeebs.Apps.WebApps.Middleware
{
	/// <summary>
	/// Redirect Exact Middleware
	/// </summary>
	public sealed class RedirectExactMiddleware : IMiddleware
	{
		private readonly JeebsConfig config;

		private readonly ILogger logger = Serilog.Log.ForContext<RedirectExactMiddleware>();

		/// <summary>
		/// Construct object
		/// </summary>
		/// <param name="config">JeebsConfig</param>
		public RedirectExactMiddleware(IOptions<JeebsConfig> config) =>
			this.config = config.Value;

		/// <summary>
		/// Invoke middleware and perform any redirections
		/// </summary>
		/// <param name="context">HttpContext</param>
		/// <param name="next">Next Middleware</param>
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			// Get current path and query
			var req = context.Request;
			var current = req.Path.ToString();
			var currentWithQuery = req.QueryString.HasValue switch
			{
				true =>
					current + req.QueryString.Value,

				false =>
					current
			};

			// Get redirections configuration
			var redirectionsConfig = config.Web.Redirections;

			// Check for current path and current path with query
			var redirect = (redirectionsConfig.ContainsKey(current), redirectionsConfig.ContainsKey(currentWithQuery)) switch
			{
				(true, _) =>
					redirectionsConfig[current],

				(false, true) =>
					redirectionsConfig[currentWithQuery],

				(false, false) =>
					null
			};

			// If there is a match redirect to it
			if (redirect is string redirectTo)
			{
				// Build redirection URL and log action
				var url = $"{req.Scheme}://{req.Host}{redirectTo}";
				logger.Information("Redirecting from '{RedirectFrom}' to '{RedirectTo}'.", current, url);

				// Perform the (permanent) redirect
				context.Response.Redirect(url, permanent: true);
			}
			else
			{
				// No redirections match so move on to the next delegate
				await next(context);
			}
		}
	}
}
