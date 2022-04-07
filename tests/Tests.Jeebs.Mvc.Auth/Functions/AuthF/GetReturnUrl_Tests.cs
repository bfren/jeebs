// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Auth.Functions.AuthF_Tests;

public class GetReturnUrl_Tests
{
	[Fact]
	public void Arg_Is_Not_Null__Arg_Is_Local_Url__Return_Arg()
	{
		// Arrange
		var url = Substitute.For<IUrlHelper>();
		url.IsLocalUrl(default)
			.ReturnsForAnyArgs(true);
		var returnUrl = Rnd.Str;

		// Act
		var result = AuthF.GetReturnUrl(url, returnUrl);

		// Assert
		Assert.Equal(returnUrl, result);
	}

	[Fact]
	public void Arg_Is_Not_Null__Arg_Is_Not_Local_Url__Return_Slash()
	{
		// Arrange
		var url = Substitute.For<IUrlHelper>();
		url.IsLocalUrl(default)
			.ReturnsForAnyArgs(false);

		// Act
		var result = AuthF.GetReturnUrl(url, Rnd.Str);

		// Assert
		Assert.Equal("/", result);
	}

	[Fact]
	public void Arg_Is_Null__Return_Slash()
	{
		// Arrange
		var url = Substitute.For<IUrlHelper>();

		// Act
		var result = AuthF.GetReturnUrl(url, null);

		// Assert
		Assert.Equal("/", result);
	}
}
