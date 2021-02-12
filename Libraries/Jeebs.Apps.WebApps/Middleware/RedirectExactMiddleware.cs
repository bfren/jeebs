﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Config;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Jeebs.Apps.WebApps.Middleware
{
	/// <summary>
	/// Redirect Exact Middleware
	/// </summary>
	public sealed class RedirectExactMiddleware : IMiddleware
	{
		/// <summary>
		/// ILogger
		/// </summary>
		private readonly ILogger logger = Serilog.Log.ForContext<RedirectExactMiddleware>();

		/// <summary>
		/// Registered redirections
		/// </summary>
		private readonly RedirectionsConfig redirections;

		/// <summary>
		/// Construct object
		/// </summary>
		/// <param name="redirections">Redirections</param>
		public RedirectExactMiddleware(RedirectionsConfig redirections) =>
			this.redirections = redirections;

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

			// Check for current path and current path with query
			var redirect = (redirections.ContainsKey(current), redirections.ContainsKey(currentWithQuery)) switch
			{
				(true, _) =>
					redirections[current],

				(false, true) =>
					redirections[currentWithQuery],

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

		#region For Testing

		/// <summary>
		/// Construct object
		/// </summary>
		/// <param name="redirections">Redirections</param>
		/// <param name="logger">ILogger</param>
		internal RedirectExactMiddleware(RedirectionsConfig redirections, ILogger logger) : this(redirections) =>
			this.logger = logger;

		/// <summary>
		/// Construct an object for testing
		/// </summary>
		/// <param name="redirections">RedirectionsConfig</param>
		/// <param name="logger">ILogger</param>
		public static RedirectExactMiddleware CreateForTesting(RedirectionsConfig redirections, ILogger logger) =>
			new RedirectExactMiddleware(redirections, logger);

		#endregion
	}
}
