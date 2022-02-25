// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Jeebs.Mvc.Models.Menu_Tests;

public class GetSimpleItems_Tests
{
	[Fact]
	public void No_Items_Returns_Empty_List()
	{
		// Arrange
		var urlHelper = Substitute.For<IUrlHelper>();
		var getUri = Substitute.For<Menu.GetUri>();

		// Act
		var result = Menu.F.GetSimpleItems(urlHelper, new(), getUri);

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void With_Text_Uses_Item_Text_As_Text()
	{
		// Arrange
		var urlHelper = Substitute.For<IUrlHelper>();
		var i0 = new MenuItem { Text = F.Rnd.Str, Controller = F.Rnd.Str };
		var i1 = new MenuItem { Text = F.Rnd.Str, Controller = F.Rnd.Str };
		var items = new[] { i0, i1 }.ToList();
		var getUri = Substitute.For<Menu.GetUri>();
		getUri.Invoke(urlHelper, Arg.Any<MenuItem>()).Returns(F.Rnd.Str);

		// Act
		var result = Menu.F.GetSimpleItems(urlHelper, items, getUri);

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
		var i0 = new MenuItem { Controller = F.Rnd.Str };
		var i1 = new MenuItem { Controller = F.Rnd.Str };
		var items = new[] { i0, i1 }.ToList();
		var getUri = Substitute.For<Menu.GetUri>();
		getUri.Invoke(urlHelper, Arg.Any<MenuItem>()).Returns(F.Rnd.Str);

		// Act
		var result = Menu.F.GetSimpleItems(urlHelper, items, getUri);

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
		var i0 = new MenuItem { Text = F.Rnd.Str, Controller = F.Rnd.Str };
		var i1 = new MenuItem { Text = F.Rnd.Str, Controller = F.Rnd.Str };
		var items = new[] { i0, i1 }.ToList();
		var uri = F.Rnd.Str;
		var getUri = Substitute.For<Menu.GetUri>();
		getUri.Invoke(urlHelper, Arg.Any<MenuItem>()).Returns(uri);

		// Act
		var result = Menu.F.GetSimpleItems(urlHelper, items, getUri);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(uri, x.Url),
			x => Assert.Equal(uri, x.Url)
		);
	}
}
