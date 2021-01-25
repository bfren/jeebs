using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Config;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Jeebs.Apps.WebApps.Middleware
{
	/// <summary>
	/// Google Site Verification
	/// </summary>
	public sealed class SiteVerificationMiddleware
	{
		/// <summary>
		/// The next request in the pipeline
		/// </summary>
		private readonly RequestDelegate next;

		private readonly VerificationConfig config;

		private readonly ILogger logger = Serilog.Log.ForContext<SiteVerificationMiddleware>();

		/// <summary>
		/// Set Site Verification configuration
		/// </summary>
		/// <param name="next"></param>
		/// <param name="config">Verification code</param>
		public SiteVerificationMiddleware(RequestDelegate next, VerificationConfig config) =>
			(this.next, this.config) = (next, config);

		/// <summary>
		/// Invoke Middleware
		/// </summary>
		/// <param name="context">HttpContext</param>
		/// <returns>Next delegate</returns>
		public async Task Invoke(HttpContext context)
		{
			var path = context.Request.Path.ToString().TrimStart('/');

			try
			{
				if (path == config.Google)
				{
					await WriteAsync(context, "text/html", $"google-site-verification: {path}");
					return;
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex, "Unable to return Site Verification page '{Path}'.", path);
			}

			await next(context);
		}

		private static async Task WriteAsync(HttpContext context, string contentType, string content)
		{
			context.Response.Clear();
			context.Response.ContentType = contentType;
			await context.Response.WriteAsync(content);
			await context.Response.CompleteAsync();
		}
	}
}
