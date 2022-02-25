// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.Mvc.Models.Menu_Tests;

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
		await Menu.F.LoadUriAsync(result, client, uri, CancellationToken.None);

		// Assert
		result.ToString().Contains("failed");
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
		await Menu.F.LoadUriAsync(result, client, uri, CancellationToken.None);

		// Assert
		result.ToString().Contains("done");
	}

	[Fact]
	public async Task Returns_Correct_Message()
	{
		// Arrange
		var result = new StringBuilder();
		var handler = new MockHttpMessageHandler();
		var client = new HttpClient(handler);
		var uri = "https://bfren.dev";
		var expected = $"Loading {uri} .. done<br/>\r\n";

		// Act
		await Menu.F.LoadUriAsync(result, client, uri, CancellationToken.None);

		// Assert
		Assert.Equal(expected, result.ToString());
	}
}
