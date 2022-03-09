// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;
using static Jeebs.Mvc.Models.Menu;

namespace Jeebs.Mvc.Models.Menu_Tests.MenuF_Tests;

public class LoadUriAsync_Tests
{
	[Fact]
	public async Task Appends_Failed_On_Failure()
	{
		// Arrange
		var result = new StringBuilder();
		var handler = new MockHttpMessageHandler(System.Net.HttpStatusCode.InternalServerError);
		var client = new HttpClient(handler);
		var uri = "https://bfren.dev";

		// Act
		await MenuF.LoadUriAsync(result, client, uri, CancellationToken.None);

		// Assert
		Assert.Contains("failed", result.ToString());
	}

	[Fact]
	public async Task Appends_Done_On_Success()
	{
		// Arrange
		var result = new StringBuilder();
		var handler = new MockHttpMessageHandler();
		var client = new HttpClient(handler);
		var uri = "https://bfren.dev";

		// Act
		await MenuF.LoadUriAsync(result, client, uri, CancellationToken.None);

		// Assert
		Assert.Contains("done", result.ToString());
	}

	[Fact]
	public async Task Returns_Correct_Message()
	{
		// Arrange
		var result = new StringBuilder();
		var handler = new MockHttpMessageHandler();
		var client = new HttpClient(handler);
		var uri = "https://bfren.dev";
		var expected = $"Loading {uri} .. done<br/>{Environment.NewLine}";

		// Act
		await MenuF.LoadUriAsync(result, client, uri, CancellationToken.None);

		// Assert
		Assert.Equal(expected, result.ToString());
	}
}
