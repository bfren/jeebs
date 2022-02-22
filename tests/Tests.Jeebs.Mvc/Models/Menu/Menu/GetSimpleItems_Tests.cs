// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
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
		var menu = Substitute.ForPartsOf<Menu>();
		var getUri = Substitute.For<Func<IUrlHelper, MenuItem, string?>>();

		// Act
		var r0 = menu.GetSimpleItems(urlHelper);
		var r1 = menu.GetSimpleItems(urlHelper, getUri);

		// Assert
		Assert.Empty(r0);
		Assert.Empty(r1);
	}

	[Fact]
	public void With_Text_Uses_Item_Text_As_Text()
	{
		// Arrange
		var urlHelper = Substitute.For<IUrlHelper>();
		var i0 = new MenuItem { Text = F.Rnd.Str, Controller = F.Rnd.Str };
		var i1 = new MenuItem { Text = F.Rnd.Str, Controller = F.Rnd.Str };
		var menu = Substitute.ForPartsOf<Menu>();
		menu.Items.AddRange(new[] { i0, i1 });
		var getUri = Substitute.For<Func<IUrlHelper, MenuItem, string?>>();
		getUri.Invoke(urlHelper, Arg.Any<MenuItem>()).Returns(F.Rnd.Str);

		// Act
		var result = menu.GetSimpleItems(urlHelper, getUri);

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
		var menu = Substitute.ForPartsOf<Menu>();
		menu.Items.AddRange(new[] { i0, i1 });
		var getUri = Substitute.For<Func<IUrlHelper, MenuItem, string?>>();
		getUri.Invoke(urlHelper, Arg.Any<MenuItem>()).Returns(F.Rnd.Str);

		// Act
		var result = menu.GetSimpleItems(urlHelper, getUri);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i0.Controller, x.Text),
			x => Assert.Equal(i1.Controller, x.Text)
		);
	}

	//[Fact]
	//public void Calls_GetUri_With_Each_Item()
	//{
	//	// Arrange
	//	var urlHelper = Substitute.For<IUrlHelper>();
	//	var i0 = new MenuItem { Text = F.Rnd.Str, Controller = F.Rnd.Str };
	//	var i1 = new MenuItem { Text = F.Rnd.Str, Controller = F.Rnd.Str };
	//	var getUri = Substitute.For<Func<IUrlHelper, MenuItem, string?>>();
	//	var menu = Substitute.ForPartsOf<Menu>();
	//	menu.Items.AddRange(new[] { i0, i1 });

	//	// Act
	//	_ = menu.GetSimpleItems(urlHelper, getUri);

	//	// Assert
	//	getUri.Received().Invoke(urlHelper, i0);
	//	getUri.Received().Invoke(urlHelper, i1);
	//}

	[Fact]
	public void Uses_Uri_From_GetUri_As_Uri()
	{
		// Arrange
		var urlHelper = Substitute.For<IUrlHelper>();
		var i0 = new MenuItem { Text = F.Rnd.Str, Controller = F.Rnd.Str };
		var i1 = new MenuItem { Text = F.Rnd.Str, Controller = F.Rnd.Str };
		var uri = F.Rnd.Str;
		var getUri = Substitute.For<Func<IUrlHelper, MenuItem, string?>>();
		getUri.Invoke(urlHelper, Arg.Any<MenuItem>()).Returns(uri);
		var menu = Substitute.ForPartsOf<Menu>();
		menu.Items.AddRange(new[] { i0, i1 });

		// Act
		var result = menu.GetSimpleItems(urlHelper, getUri);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(uri, x.Url),
			x => Assert.Equal(uri, x.Url)
		);
	}
}
