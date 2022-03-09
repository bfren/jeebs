// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc;
using static Jeebs.Mvc.Models.Menu;

namespace Jeebs.Mvc.Models.Menu_Tests.MenuF_Tests;

public class GetSimpleItems_Tests
{
	[Fact]
	public void No_Items_Returns_Empty_List()
	{
		// Arrange
		var urlHelper = Substitute.For<IUrlHelper>();
		var getUri = Substitute.For<Menu.GetUri>();

		// Act
		var result = MenuF.GetSimpleItems(urlHelper, new(), getUri);

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void With_Text_Uses_Item_Text_As_Text()
	{
		// Arrange
		var urlHelper = Substitute.For<IUrlHelper>();
		var i0 = new MenuItem { Text = Rnd.Str, Controller = Rnd.Str };
		var i1 = new MenuItem { Text = Rnd.Str, Controller = Rnd.Str };
		var items = new[] { i0, i1 }.ToList();
		var getUri = Substitute.For<Menu.GetUri>();
		_ = getUri.Invoke(urlHelper, Arg.Any<MenuItem>()).Returns(Rnd.Str);

		// Act
		var result = MenuF.GetSimpleItems(urlHelper, items, getUri);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i0.Text, x.Text),
			x => Assert.Equal(i1.Text, x.Text)
		);
	}

	[Fact]
	public void Without_Text_Uses_Item_Controller_As_Text()
	{
		// Arrange
		var urlHelper = Substitute.For<IUrlHelper>();
		var i0 = new MenuItem { Controller = Rnd.Str };
		var i1 = new MenuItem { Controller = Rnd.Str };
		var items = new[] { i0, i1 }.ToList();
		var getUri = Substitute.For<Menu.GetUri>();
		_ = getUri.Invoke(urlHelper, Arg.Any<MenuItem>()).Returns(Rnd.Str);

		// Act
		var result = MenuF.GetSimpleItems(urlHelper, items, getUri);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i0.Controller, x.Text),
			x => Assert.Equal(i1.Controller, x.Text)
		);
	}

	[Fact]
	public void Uses_Uri_From_GetUri_As_Uri()
	{
		// Arrange
		var urlHelper = Substitute.For<IUrlHelper>();
		var i0 = new MenuItem { Text = Rnd.Str, Controller = Rnd.Str };
		var i1 = new MenuItem { Text = Rnd.Str, Controller = Rnd.Str };
		var items = new[] { i0, i1 }.ToList();
		var uri = Rnd.Str;
		var getUri = Substitute.For<Menu.GetUri>();
		_ = getUri.Invoke(urlHelper, Arg.Any<MenuItem>()).Returns(uri);

		// Act
		var result = MenuF.GetSimpleItems(urlHelper, items, getUri);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(uri, x.Url),
			x => Assert.Equal(uri, x.Url)
		);
	}
}
