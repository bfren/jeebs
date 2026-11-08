// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Http;

namespace Jeebs.Apps.Web.Middleware.LoggerMiddleware_Tests;

public class InvokeAsync_Tests
{
	[Fact]
	public async Task Next_Throws__Rethrows_Exception()
	{
		// Arrange
		var middleware = new LoggerMiddleware();
		var context = new DefaultHttpContext();
		var ex = new InvalidOperationException(Rnd.Str);

		// Act
		var result = await Assert.ThrowsAsync<InvalidOperationException>(
			() => middleware.InvokeAsync(context, _ => throw ex)
		);

		// Assert
		Assert.Same(ex, result);
	}
}
