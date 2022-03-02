// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Mvc.Models.Menu_Tests;

public class LoadUrisAsync_Tests
{
	[Fact]
	public async Task Empty_List_Returns_Nothing()
	{
		// Arrange
		var handler = new MockHttpMessageHandler();
		var client = new HttpClient(handler);
		var loadUri = Substitute.For<Menu.LoadUri>();

		// Act
		var result = await Menu.F.LoadUrisAsync(client, new(), loadUri);

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
		var u0 = F.Rnd.Str;
		var u1 = F.Rnd.Str;
		var u2 = F.Rnd.Str;
		var uris = new[] { u0, u1, u2 }.ToList();
		var loadUri = Substitute.For<Menu.LoadUri>();

		// Act
		_ = await Menu.F.LoadUrisAsync(client, uris, loadUri);

		// Assert
		await loadUri.Received(1).Invoke(Arg.Any<StringBuilder>(), client, u0, Arg.Any<CancellationToken>());
		await loadUri.Received(1).Invoke(Arg.Any<StringBuilder>(), client, u1, Arg.Any<CancellationToken>());
		await loadUri.Received(1).Invoke(Arg.Any<StringBuilder>(), client, u2, Arg.Any<CancellationToken>());
	}
}
