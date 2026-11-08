// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Controllers.MvcController_Tests;

public class ProcessAsync_Tests
{
	[Fact]
	public async Task Result_With_Sync_Success__Ok__Runs_Success()
	{
		// Arrange
		var controller = new TestController(Substitute.For<ILog>());
		var value = Rnd.Int;

		// Act
		var result = await controller.Run(R.Wrap(value), x => new OkObjectResult(x));

		// Assert
		var ok = Assert.IsType<OkObjectResult>(result);
		Assert.Equal(value, ok.Value);
	}

	private sealed class TestController(ILog log) : MvcController(log)
	{
		public Task<IActionResult> Run<T>(Result<T> result, Func<T, IActionResult> success) =>
			ProcessAsync(result, success);
	}
}
