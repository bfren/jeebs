// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Web.Redirections;
using Microsoft.AspNetCore.Http;

namespace Jeebs.Apps.Web.Middleware.RedirectExactMiddleware_Tests;

public class InvokeAsync_Tests
{
	[Fact]
	public async Task Match__Redirects_To_Configured_Path_Ignoring_Host()
	{
		// Arrange
		var from = $"/{Rnd.Str}";
		var to = $"/{Rnd.Str}";
		var config = new RedirectionsConfig { { from, to } };
		var middleware = new RedirectExactMiddleware(config);
		var context = new DefaultHttpContext();
		context.Request.Host = new("evil.example");
		context.Request.Path = from;
		var next = Substitute.For<RequestDelegate>();

		// Act
		await middleware.InvokeAsync(context, next);

		// Assert
		Assert.Equal(StatusCodes.Status301MovedPermanently, context.Response.StatusCode);
		Assert.Equal(to, context.Response.Headers.Location.ToString());
		await next.DidNotReceiveWithAnyArgs().Invoke(default!);
	}

	[Fact]
	public async Task No_Match__Calls_Next()
	{
		// Arrange
		var middleware = new RedirectExactMiddleware(new RedirectionsConfig());
		var context = new DefaultHttpContext();
		context.Request.Path = $"/{Rnd.Str}";
		var next = Substitute.For<RequestDelegate>();

		// Act
		await middleware.InvokeAsync(context, next);

		// Assert
		await next.Received().Invoke(context);
	}
}
