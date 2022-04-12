// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Jeebs.Mvc.Result_Tests;

public class ExecuteResultAsync_Tests
{
	[Fact]
	public async Task Calls_IActionResult_Executor_ExecuteAsync__With_Correct_Values()
	{
		// Arrange
		var executor = Substitute.For<IActionResultExecutor<JsonResult>>();
		var serviceProvider = Substitute.For<IServiceProvider>();
		serviceProvider.GetService(typeof(IActionResultExecutor<JsonResult>))
			.Returns(executor);
		var httpContext = Substitute.For<HttpContext>();
		httpContext.RequestServices
			.Returns(serviceProvider);
		var actionContext = new ActionContext { HttpContext = httpContext };
		var value = Rnd.Str;
		var statusCode = Rnd.Int;
		var jsonResult = Result.Create(value) with { StatusCode = statusCode };

		// Act
		await jsonResult.ExecuteResultAsync(actionContext);

		// Assert
		await executor.Received().ExecuteAsync(actionContext, Arg.Is<JsonResult>(x =>
			x.ContentType == "application/json"
			&& x.StatusCode == statusCode
			&& jsonResult.Equals(x.Value)
		));
	}
}
