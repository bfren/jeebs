// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.ThreadF_Tests;

public class FireAndForget_Tests
{
	private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(5);

	[Fact]
	public async Task Runs_Task()
	{
		// Arrange
		var ran = new TaskCompletionSource();

		// Act
		ThreadF.FireAndForget(() =>
		{
			ran.SetResult();
			return Task.CompletedTask;
		});

		// Assert
		await ran.Task.WaitAsync(Timeout, TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task Task_Throws__Calls_OnError_With_Exception()
	{
		// Arrange
		var exception = new InvalidOperationException(Rnd.Str);
		var received = new TaskCompletionSource<Exception>();

		// Act
		ThreadF.FireAndForget(
			async () =>
			{
				await Task.Yield();
				throw exception;
			},
			received.SetResult
		);

		// Assert
		var result = await received.Task.WaitAsync(Timeout, TestContext.Current.CancellationToken);
		Assert.Same(exception, result);
	}
}
