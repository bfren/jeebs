// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc;
using static Jeebs.Mvc.Models.Menu;

namespace Jeebs.Mvc.Models.Menu_Tests.MenuF_Tests;

public class GetUris_Tests
{
	[Fact]
	public void Empty_List_Returns_Empty_List()
	{
		// Arrange
		var urlHelper = Substitute.For<IUrlHelper>();
		var getUri = Substitute.For<GetUri>();

		// Act
		var result = MenuF.GetUris(urlHelper, [], getUri);

		// Assert
		Assert.Empty(result);
		getUri.DidNotReceiveWithAnyArgs().Invoke(urlHelper, Arg.Any<MenuItem>());
	}

	[Fact]
	public void Calls_GetUri_For_Each_Item()
	{
		// Arrange
		var urlHelper = Substitute.For<IUrlHelper>();
		var i0 = new MenuItem();
		var i1 = new MenuItem();
		var items = new[] { i0, i1 }.ToList();
		var getUri = Substitute.For<GetUri>();

		// Act
		var result = MenuF.GetUris(urlHelper, items, getUri);

		// Assert
		Assert.NotEmpty(result);
		getUri.Received(1).Invoke(urlHelper, i0);
		getUri.Received(1).Invoke(urlHelper, i1);
	}

	[Fact]
	public void Calls_GetUri_For_Each_Item_And_Children()
	{
		// Arrange
		var urlHelper = Substitute.For<IUrlHelper>();
		var c0 = new MenuItem { Text = Rnd.Str };
		var c1 = new MenuItem { Text = Rnd.Str };
		var c2 = new MenuItem { Text = Rnd.Str };
		var c3 = new MenuItem { Text = Rnd.Str };
		var c4 = new MenuItem { Text = Rnd.Str };
		var p0 = new MenuItem { Text = Rnd.Str, Children = new([c0, c1, c2]) };
		var p1 = new MenuItem { Text = Rnd.Str, Children = new([c3, c4]) };
		var items = new[] { p0, p1 }.ToList();
		var getUri = Substitute.For<GetUri>();
		getUri.Invoke(urlHelper, Arg.Any<MenuItem>()).Returns(x => x.ArgAt<MenuItem>(1).Text);

		// Act
		var result = MenuF.GetUris(urlHelper, items, getUri);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(p0.Text, x),
			x => Assert.Equal(c0.Text, x),
			x => Assert.Equal(c1.Text, x),
			x => Assert.Equal(c2.Text, x),
			x => Assert.Equal(p1.Text, x),
			x => Assert.Equal(c3.Text, x),
			x => Assert.Equal(c4.Text, x)
		);
		getUri.ReceivedWithAnyArgs(7).Invoke(urlHelper, Arg.Any<MenuItem>());
	}
}
