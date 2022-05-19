// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Models;
using Jeebs.Auth.Jwt.Constants;
using Jeebs.Logging;
using Jeebs.Mvc.Auth.Models;
using MaybeF;
using Microsoft.AspNetCore.Mvc;
using static StrongId.Testing.Generator;

namespace Jeebs.Mvc.Auth.Functions.AuthF_Tests;

public class DoSignInAsync_Tests
{
	private (AuthUserModel user, AuthF.SignInArgs v) Setup()
	{
		var model = new SignInModel { Email = Rnd.Str, Password = Rnd.Str, RememberMe = Rnd.Flip };
		var addErrorAlert = Substitute.For<Action<string>>();
		var repo = Substitute.For<IAuthUserRepository<AuthUserEntity>>();
		repo.UpdateLastSignInAsync(default!)
			.ReturnsForAnyArgs(F.True);
		var auth = Substitute.For<IAuthDataProvider>();
		auth.User
			.Returns(repo);
		var getClaims = Substitute.For<AuthF.GetClaims>();
		getClaims.Invoke(default!, default!)
			.ReturnsForAnyArgs(new List<Claim>());
		var log = Substitute.For<ILog>();
		var signIn = Substitute.For<Func<ClaimsPrincipal, Task<AuthResult>>>();
		var url = Substitute.For<IUrlHelper>();
		url.IsLocalUrl(default)
			.ReturnsForAnyArgs(true);
		var user = new AuthUserModel { Id = LongId<AuthUserId>(), EmailAddress = Rnd.Str, IsSuper = Rnd.Flip };
		return (user, new(model, auth, log, url, getClaims, signIn));
	}

	private Func<IAuthDataProvider, SignInModel, ILog, Task<Maybe<AuthUserModel>>> SetupValidate(AuthUserModel? user = null)
	{
		var validateUser = Substitute.For<Func<IAuthDataProvider, SignInModel, ILog, Task<Maybe<AuthUserModel>>>>();
		validateUser(default!, default!, default!)
			.ReturnsForAnyArgs(user is not null ? F.Some(user) : Create.None<AuthUserModel>());

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
		var msg = Substitute.For<IMsg>();
		var validateUser = SetupValidate();
		validateUser(default!, default!, default!)
			.ReturnsForAnyArgs(F.None<AuthUserModel>(msg));

		// Act
		_ = await AuthF.DoSignInAsync(v, validateUser);

		// Assert
		v.Log.Received().Msg(msg);
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
				x.Claims.Any(c => c.Type == JwtClaimTypes.UserId && c.Value == user.Id.Value.ToString())
				&& x.Claims.Any(c => c.Type == ClaimTypes.Email && c.Value == user.EmailAddress)
				&& x.Claims.Any(c => (c.Type == JwtClaimTypes.IsSuper && c.Value == user.IsSuper.ToString()) || !user.IsSuper)
			)
		);
	}

	[Fact]
	public async Task Invalid_User__Calls_Log_Err__With_Correct_Values()
	{
		// Arrange
		var (_, v) = Setup();

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

		// Act
		var result = await AuthF.DoSignInAsync(v);

		// Assert
		Assert.IsType<AuthResult.TryAgain>(result);
	}
}
