// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Mvc.Auth.Functions.AuthF_Tests;

public class DoSignOutAsync_Tests
{
	private AuthF.SignOutArgs Setup()
	{
		var addInfo = Substitute.For<Action<string>>();
		var redirectUrl = Substitute.For<Func<string?>>();
		var signOut = Substitute.For<Func<Task>>();
		return new(addInfo, redirectUrl, signOut);
	}

	[Fact]
	public async Task Calls_SignOutAsync()
	{
		// Arrange
		var v = Setup();

		// Act
		_ = await AuthF.DoSignOutAsync(v);

		// Assert
		await v.SignOutAsync.Received().Invoke();
	}

	[Fact]
	public async Task Calls_AddInfoAlert()
	{
		// Arrange
		var v = Setup();

		// Act
		_ = await AuthF.DoSignOutAsync(v);

		// Assert
		v.AddInfoAlert.Received().Invoke("Goodbye!");
	}

	[Fact]
	public async Task Calls_GetSignInFormPage__Returns_AuthResult_SignedOut()
	{
		// Arrange
		var v = Setup();

		// Act
		var result = await AuthF.DoSignOutAsync(v);

		// Assert
		Assert.IsType<AuthResult.SignedOut>(result);
		v.RedirectUrl.Received().Invoke();
	}
}
