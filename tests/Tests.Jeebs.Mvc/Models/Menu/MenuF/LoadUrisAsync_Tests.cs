// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.Mvc.Models.Menu;

namespace Jeebs.Mvc.Models.Menu_Tests.MenuF_Tests;

public class LoadUrisAsync_Tests
{
	[Fact]
	public async Task Empty_List_Returns_Nothing()
	{
		// Arrange
		var handler = new MockHttpMessageHandler();
		var client = new HttpClient(handler);
		var loadUri = Substitute.For<LoadUri>();

		// Act
		var result = await MenuF.LoadUrisAsync(client, [], loadUri);

		// Assert
		var some = result.AssertSome();
		Assert.Empty(some);
	}

	[Fact]
	public async Task Loads_Each_Uri()
	{
		// Arrange
		var handler = new MockHttpMessageHandler();
		var client = new HttpClient(handler);
		var u0 = Rnd.Str;
		var u1 = Rnd.Str;
		var u2 = Rnd.Str;
		var uris = new[] { u0, u1, u2 }.ToList();
		var loadUri = Substitute.For<LoadUri>();
#pragma warning disable CA2012 // NSubstitute setup call: the ValueTask is never consumed, only configured
		loadUri.Invoke(default!, default!, TestContext.Current.CancellationToken).ReturnsForAnyArgs(ValueTask.FromResult(Rnd.Str));
#pragma warning restore CA2012

		// Act
		await MenuF.LoadUrisAsync(client, uris, loadUri);

		// Assert
		await loadUri.Received(1).Invoke(client, u0, Arg.Any<CancellationToken>());
		await loadUri.Received(1).Invoke(client, u1, Arg.Any<CancellationToken>());
		await loadUri.Received(1).Invoke(client, u2, Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task Returns_Every_Line_In_Uri_Order()
	{
		// Arrange
		var client = new HttpClient(new MockHttpMessageHandler());
		var uris = Enumerable.Range(0, 50).Select(i => i.ToString()).ToList();

		// later URIs finish first, so lines would come back out of order (or be lost) if they shared one buffer
		static async ValueTask<string> loadUri(HttpClient _, string uri, CancellationToken token)
		{
			await Task.Delay(50 - int.Parse(uri), token);
			return $"{uri};";
		}

		// Act
		var result = await MenuF.LoadUrisAsync(client, uris, loadUri);

		// Assert
		Assert.Equal(string.Concat(uris.Select(u => $"{u};")), result.AssertSome());
	}
}
