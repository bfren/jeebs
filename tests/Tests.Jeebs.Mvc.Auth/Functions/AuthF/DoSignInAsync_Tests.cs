// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Ids;
using Jeebs.Logging;
using Jeebs.Mvc.Auth.Models;
using Microsoft.AspNetCore.Mvc;
using CT = Jeebs.Auth.Jwt.Constants.ClaimTypes;

namespace Jeebs.Mvc.Auth.Functions.AuthF_Tests;

public class DoSignInAsync_Tests
{
	private (AuthUserModel user, AuthF.SignInArgs v) Setup()
	{
		var model = new SignInModel { Email = Rnd.Str, Password = Rnd.Str, RememberMe = Rnd.Flip };
		var addErrorAlert = Substitute.For<Action<string>>();
		var repo = Substitute.For<IAuthUserRepository>();
		repo.UpdateLastSignInAsync(default!)
			.ReturnsForAnyArgs(R.True);
		var auth = Substitute.For<IAuthDataProvider>();
		auth.User
			.Returns(repo);
		var getClaims = Substitute.For<AuthF.GetClaims>();
		getClaims.Invoke(default!, default!)
			.ReturnsForAnyArgs([]);
		var log = Substitute.For<ILog>();
		var signIn = Substitute.For<Func<ClaimsPrincipal, Task<AuthOp>>>();
		var url = Substitute.For<IUrlHelper>();
		url.IsLocalUrl(default)
			.ReturnsForAnyArgs(true);
		var user = new AuthUserModel { Id = IdGen.LongId<AuthUserId>(), EmailAddress = Rnd.Str, IsSuper = Rnd.Flip };
		return (user, new(model, auth, log, url, getClaims, signIn));
	}

	private Func<IAuthDataProvider, SignInModel, ILog, Task<Result<AuthUserModel>>> SetupValidate(AuthUserModel? user = null)
	{
		var validateUser = Substitute.For<Func<IAuthDataProvider, SignInModel, ILog, Task<Result<AuthUserModel>>>>();
		validateUser(default!, default!, default!)
			.ReturnsForAnyArgs(user is not null ? R.Wrap(user) : FailGen.Create());

		return validateUser;
	}

	[Fact]
	public async Task Calls_ValidateUserAsync__With_Correct_Values()
	{
		// Arrange
		var (_, v) = Setup();
		var validateUser = SetupValidate();

		// Act
		_ = await AuthF.DoSignInAsync(v, validateUser);

		// Assert
		await validateUser.Received().Invoke(v.Auth, v.Model, v.Log);
	}

	[Fact]
	public async Task Calls_ValidateUserAsync__Receives_Some__Calls_Log_Dbg__With_Correct_Values()
	{
		// Arrange
		var (user, v) = Setup();
		var validateUser = SetupValidate(user);

		// Act
		_ = await AuthF.DoSignInAsync(v, validateUser);

		// Assert
		v.Log.Received().Dbg("User {UserId} validated.", user.Id.Value);
	}

	[Fact]
	public async Task Calls_ValidateUserAsync__Receives_None__Calls_Log_Msg__With_Correct_Values()
	{
		// Arrange
		var (_, v) = Setup();
		var failure = FailGen.Create();
		var validateUser = SetupValidate();
		validateUser(default!, default!, default!)
			.ReturnsForAnyArgs(failure);

		// Act
		_ = await AuthF.DoSignInAsync(v, validateUser);

		// Assert
		v.Log.Received().Failure(failure.Value);
	}

	[Fact]
	public async Task Valid_User__Calls_SignInAsync__With_Correct_Values()
	{
		// Arrange
		var (user, v) = Setup();
		var validateUser = SetupValidate(user);

		// Act
		_ = await AuthF.DoSignInAsync(v, validateUser);

		// Assert
		await v.SignInAsync.Received().Invoke(
			Arg.Is<ClaimsPrincipal>(x =>
				x.Claims.Any(c => c.Type == CT.UserId && c.Value == user.Id.Value.ToString())
				&& x.Claims.Any(c => c.Type == ClaimTypes.Email && c.Value == user.EmailAddress)
				&& x.Claims.Any(c => (c.Type == CT.IsSuper && c.Value == user.IsSuper.ToString()) || !user.IsSuper)
			)
		);
	}

	[Fact]
	public async Task Invalid_User__Calls_Log_Err__With_Correct_Values()
	{
		// Arrange
		var (_, v) = Setup();
		v.Auth.ValidateUserAsync(default!, default!)
			.ReturnsForAnyArgs(false);

		// Act
		_ = await AuthF.DoSignInAsync(v);

		// Assert
		v.Log.Received().Err("Unknown username or password: {Email}.", v.Model.Email);
	}

	[Fact]
	public async Task Invalid_User__Returns_AuthResult_TryAgain()
	{
		// Arrange
		var (_, v) = Setup();
		v.Auth.ValidateUserAsync(default!, default!)
			.ReturnsForAnyArgs(false);

		// Act
		var result = await AuthF.DoSignInAsync(v);

		// Assert
		Assert.IsType<AuthOp.TryAgain>(result);
	}
}
