// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Auth.Controllers.AuthControllerBase_Tests;

public class SignIn_Tests
{
	[Fact]
	public async Task Wrong_Password__Adds_Error_Alert_And_Returns_Model_Without_Password()
	{
		// Arrange
		var (controller, auth, _) = Setup.Get();
		auth.ValidateUserAsync(default!, default!)
			.ReturnsForAnyArgs(R.False);
		var model = new SignInModel { Email = Rnd.Str, Password = Rnd.Str, RememberMe = true };

		// Act
		var result = await controller.SignIn(model);

		// Assert
		var view = Assert.IsType<ViewResult>(result);
		var returned = Assert.IsType<SignInModel>(view.Model);
		Assert.Equal(model.Email, returned.Email);
		Assert.True(returned.RememberMe);
		Assert.Empty(returned.Password);
		Assert.NotEmpty(controller.TempData);
	}
}
