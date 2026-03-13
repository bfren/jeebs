// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data;
using Jeebs.Logging;
using M = Jeebs.Mvc.Auth.Models;

namespace Jeebs.Mvc.Auth.Functions.AuthF_Tests;

public class ValidateUserAsync_Tests
{
	[Fact]
	public async Task Calls_Log_Vrb__With_Correct_Values()
	{
		// Arrange
		var auth = Substitute.For<IAuthDataProvider>();
		auth.ValidateUserAsync(default!, default!)
			.ReturnsForAnyArgs(false);
		var email = Rnd.Str;
		var model = new M.SignInModel { Email = email };
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
		auth.ValidateUserAsync(default!, default!)
			.ReturnsForAnyArgs(false);
		var email = Rnd.Str;
		var password = Rnd.Str;
		var model = new M.SignInModel { Email = email, Password = password };
		var log = Substitute.For<ILog>();

		// Act
		_ = await AuthF.ValidateUserAsync(auth, model, log);

		// Assert
		await auth.Received().ValidateUserAsync(email, password);
	}

	[Fact]
	public async Task Calls_ValidateUserAsync__Receives_None__Returns_Result()
	{
		// Arrange
		var auth = Substitute.For<IAuthDataProvider>();
		auth.ValidateUserAsync(default!, default!)
			.ReturnsForAnyArgs(FailGen.Create());
		var log = Substitute.For<ILog>();

		// Act
		var result = await AuthF.ValidateUserAsync(auth, new(), log);

		// Assert
		await auth.DidNotReceiveWithAnyArgs().RetrieveUserAsync<M.AuthUserModel, M.AuthRoleModel>(email: default!);
		result.AssertFailure();
	}

	[Fact]
	public async Task Calls_ValidateUserAsync__Receives_Some__Calls_RetrieveUserWithRolesAsync_With_Correct_Values()
	{
		// Arrange
		var auth = Substitute.For<IAuthDataProvider>();
		auth.ValidateUserAsync(default!, default!)
			.ReturnsForAnyArgs(true);
		auth.RetrieveUserAsync<M.AuthUserModel, M.AuthRoleModel>(email: default!)
			.ReturnsForAnyArgs(FailGen.Create());
		var email = Rnd.Str;
		var model = new M.SignInModel { Email = email };
		var log = Substitute.For<ILog>();

		// Act
		_ = await AuthF.ValidateUserAsync(auth, model, log);

		// Assert
		await auth.Received().RetrieveUserAsync<M.AuthUserModel, M.AuthRoleModel>(email);
	}

	[Fact]
	public async Task Calls_ValidateUserAsync__Receives_Some__Calls_RetrieveUserWithRolesAsync__Returns_Result()
	{
		// Arrange
		var auth = Substitute.For<IAuthDataProvider>();
		var user = new M.AuthUserModel();
		auth.ValidateUserAsync(default!, default!)
			.ReturnsForAnyArgs(true);
		auth.RetrieveUserAsync<M.AuthUserModel, M.AuthRoleModel>(email: default!)
			.ReturnsForAnyArgs(user);
		var log = Substitute.For<ILog>();

		// Act
		var result = await AuthF.ValidateUserAsync(auth, new(), log);

		// Assert
		var ok = result.AssertOk();
		Assert.Same(user, ok);
	}
}
