// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.Mvc.Models.Menu;

namespace Jeebs.Mvc.Models.Menu_Tests.MenuF_Tests;

public class LoadUriAsync_Tests
{
	[Fact]
	public async Task Returns_Failed_On_Failure()
	{
		// Arrange
		var handler = new MockHttpMessageHandler(System.Net.HttpStatusCode.InternalServerError);
		var client = new HttpClient(handler);
		var uri = "https://bfren.dev";

		// Act
		var result = await MenuF.LoadUriAsync(client, uri, CancellationToken.None);

		// Assert
		Assert.Contains("failed", result);
	}

	[Fact]
	public async Task Returns_Failed_On_Timeout()
	{
		// Arrange
		var client = new HttpClient(new TimeoutHttpMessageHandler());
		var uri = "https://bfren.dev";

		// Act
		var result = await MenuF.LoadUriAsync(client, uri, CancellationToken.None);

		// Assert
		Assert.StartsWith($"Loading {uri} .. failed", result);
	}

	[Fact]
	public async Task Returns_Done_On_Success()
	{
		// Arrange
		var handler = new MockHttpMessageHandler();
		var client = new HttpClient(handler);
		var uri = "https://bfren.dev";

		// Act
		var result = await MenuF.LoadUriAsync(client, uri, CancellationToken.None);

		// Assert
		Assert.Contains("done", result);
	}

	[Fact]
	public async Task Returns_Correct_Message()
	{
		// Arrange
		var handler = new MockHttpMessageHandler();
		var client = new HttpClient(handler);
		var uri = "https://bfren.dev";
		var expected = $"Loading {uri} .. done<br/>{Environment.NewLine}";

		// Act
		var result = await MenuF.LoadUriAsync(client, uri, CancellationToken.None);

		// Assert
		Assert.Equal(expected, result);
	}

	private sealed class TimeoutHttpMessageHandler : HttpMessageHandler
	{
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
			throw new TaskCanceledException("The request timed out.");
	}
}
