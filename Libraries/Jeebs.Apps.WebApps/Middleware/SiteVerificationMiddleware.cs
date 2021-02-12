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
	public sealed class SiteVerificationMiddleware : IMiddleware
	{
		private readonly VerificationConfig config;

		private readonly ILogger logger = Serilog.Log.ForContext<SiteVerificationMiddleware>();

		/// <summary>
		/// Set Site Verification configuration
		/// </summary>
		/// <param name="config">Verification code</param>
		public SiteVerificationMiddleware(VerificationConfig config) =>
			this.config = config;

		/// <summary>
		/// Invoke Middleware
		/// </summary>
		/// <param name="context">HttpContext</param>
		/// <param name="next">Next Middleware</param>
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
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
