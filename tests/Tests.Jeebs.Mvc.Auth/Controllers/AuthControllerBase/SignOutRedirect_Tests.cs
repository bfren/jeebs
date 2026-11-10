// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc.Routing;

namespace Jeebs.Mvc.Auth.Controllers.AuthControllerBase_Tests;

public class SignOutRedirect_Tests
{
	[Fact]
	public void Returns_SignIn_Action_On_Auth_Controller()
	{
		// Arrange
		var (controller, _, url) = Setup.Get();
		var expected = Rnd.Str;
		url.Action(Arg.Is<UrlActionContext>(x => x.Action == "SignIn" && x.Controller == "Auth"))
			.Returns(expected);

		// Act
		var result = controller.GetSignOutRedirect();

		// Assert
		Assert.Equal(expected, result);
	}
}
