// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Models;
using Jeebs.Logging;
using Jeebs.Mvc.Auth.Models;
using MaybeF;

namespace Jeebs.Mvc.Auth.Functions.AuthF_Tests;

public class ValidateUserAsync_Tests
{
	[Fact]
	public async Task Calls_Log_Vrb__With_Correct_Values()
	{
		// Arrange
		var auth = Substitute.For<IAuthDataProvider>();
		var email = Rnd.Str;
		var model = new SignInModel { Email = email };
		var log = Substitute.For<ILog>();

		// Act
		_ = await AuthF.ValidateUserAsync(auth, model, log);

		// Assert
		log.Received().Vrb("Validating credentials for {User}.", email);
	}

	[Fact]
	public async Task Calls_ValidateUserAsync__With_Correct_Values()
	{
		// Arrange
		var auth = Substitute.For<IAuthDataProvider>();
		var email = Rnd.Str;
		var password = Rnd.Str;
		var model = new SignInModel { Email = email, Password = password };
		var log = Substitute.For<ILog>();

		// Act
		_ = await AuthF.ValidateUserAsync(auth, model, log);

		// Assert
		await auth.Received().ValidateUserAsync<AuthUserModel>(email, password);
	}

	[Fact]
	public async Task Calls_ValidateUserAsync__Receives_None__Returns_Result()
	{
		// Arrange
		var auth = Substitute.For<IAuthDataProvider>();
		var msg = Substitute.For<IMsg>();
		auth.ValidateUserAsync<AuthUserModel>(default!, default!)
			.ReturnsForAnyArgs(F.None<AuthUserModel>(msg));
		var log = Substitute.For<ILog>();

		// Act
		var result = await AuthF.ValidateUserAsync(auth, new(), log);

		// Assert
		await auth.DidNotReceiveWithAnyArgs().RetrieveUserWithRolesAsync<AuthUserModel, AuthRoleModel>(email: default!);
		var none = result.AssertNone();
		Assert.Same(msg, none);
	}

	[Fact]
	public async Task Calls_ValidateUserAsync__Receives_Some__Calls_RetrieveUserWithRolesAsync_With_Correct_Values()
	{
		// Arrange
		var auth = Substitute.For<IAuthDataProvider>();
		auth.ValidateUserAsync<AuthUserModel>(default!, default!)
			.ReturnsForAnyArgs(new AuthUserModel());
		var email = Rnd.Str;
		var model = new SignInModel { Email = email };
		var log = Substitute.For<ILog>();

		// Act
		_ = await AuthF.ValidateUserAsync(auth, model, log);

		// Assert
		await auth.Received().RetrieveUserWithRolesAsync<AuthUserModel, AuthRoleModel>(email);
	}

	[Fact]
	public async Task Calls_ValidateUserAsync__Receives_Some__Calls_RetrieveUserWithRolesAsync__Returns_Result()
	{
		// Arrange
		var auth = Substitute.For<IAuthDataProvider>();
		var user = new AuthUserModel();
		auth.ValidateUserAsync<AuthUserModel>(default!, default!)
			.ReturnsForAnyArgs(new AuthUserModel());
		auth.RetrieveUserWithRolesAsync<AuthUserModel, AuthRoleModel>(email: default!)
			.ReturnsForAnyArgs(user);
		var log = Substitute.For<ILog>();

		// Act
		var result = await AuthF.ValidateUserAsync(auth, new(), log);

		// Assert
		var some = result.AssertSome();
		Assert.Same(user, some);
	}
}
