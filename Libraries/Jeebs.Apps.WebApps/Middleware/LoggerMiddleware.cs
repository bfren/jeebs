using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Serilog;
using Serilog.Events;

namespace Jeebs.Apps.WebApps.Middleware
{
	/// <summary>
	/// Logger Middleware
	/// </summary>
	public class LoggerMiddleware
	{
		/// <summary>
		/// The next request in the pipeline
		/// </summary>
		private readonly RequestDelegate next;

		/// <summary>
		/// ILogger
		/// </summary>
		private readonly ILogger logger = Log.ForContext<LoggerMiddleware>();

		/// <summary>
		/// Log message template
		/// </summary>
		private const string messageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

		/// <summary>
		/// Construct object
		/// </summary>
		/// <param name="next">RequestDelegate</param>
		public LoggerMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		/// <summary>
		/// Invoke Middleware
		/// </summary>
		/// <param name="context">HttpContext</param>
		/// <returns>Next delegate</returns>
		public async Task Invoke(HttpContext context)
		{
			// Start stopwatch
			var stopwatch = Stopwatch.StartNew();

			try
			{
				// Call the rest of the pipeline
				await next(context);
				stopwatch.Stop();

				// If the status is HTTP 200 (success) return
				var status = context.Response?.StatusCode;
				if (status == 200)
				{
					return;
				}

				// Get log level based on HTTP status - 500 or over is an HTTP error
				var level = status > 499 ? LogEventLevel.Error : LogEventLevel.Information;

				// Write event to log
				logger.Write(level, messageTemplate, context.Request.Method, GetPath(context), status, stopwatch.ElapsedMilliseconds);
			}
			catch (Exception ex)
			{
				// Log error
				logger.Error(ex, messageTemplate, context.Request.Method, GetPath(context), 500, stopwatch.ElapsedMilliseconds);
			}
		}

		/// <summary>
		/// Get Request Path
		/// </summary>
		/// <param name="context">HttpContext</param>
		private static string GetPath(HttpContext context) =>
			context.Features.Get<IHttpRequestFeature>()?.RawTarget ?? context.Request.Path.ToString();

		#region For Testing

		/// <summary>
		/// Construct object
		/// </summary>
		/// <param name="next">RequestDelegate</param>
		/// <param name="logger">ILogger</param>
		internal LoggerMiddleware(in RequestDelegate next, in ILogger logger) : this(next)
		{
			this.logger = logger;
		}

		/// <summary>
		/// Construct an object for testing
		/// </summary>
		/// <param name="next">RequestDelegate</param>
		/// <param name="logger">ILogger</param>
		public static LoggerMiddleware CreateForTesting(in RequestDelegate next, in ILogger logger)
			=> new LoggerMiddleware(next, logger);

		#endregion
	}
}
