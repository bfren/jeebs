// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;
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

		// Act
		await MenuF.LoadUrisAsync(client, uris, loadUri);

		// Assert
		await loadUri.Received(1).Invoke(Arg.Any<StringBuilder>(), client, u0, Arg.Any<CancellationToken>());
		await loadUri.Received(1).Invoke(Arg.Any<StringBuilder>(), client, u1, Arg.Any<CancellationToken>());
		await loadUri.Received(1).Invoke(Arg.Any<StringBuilder>(), client, u2, Arg.Any<CancellationToken>());
	}
}
