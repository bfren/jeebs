// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Ids;
using Jeebs.Logging;

namespace Jeebs.Mvc.Auth.Functions.AuthF_Tests;

public class UpdateUserLastSignInAsync_Tests
{
	private sealed record class Vars(
		ILog Log,
		IAuthDataProvider Provider,
		IAuthUserRepository Repository,
		AuthUserId UserId
	);

	private Vars Setup()
	{
		var log = Substitute.For<ILog>();

		var repo = Substitute.For<IAuthUserRepository>();
		repo.UpdateLastSignInAsync(default!)
			.ReturnsForAnyArgs(R.False);

		var provider = Substitute.For<IAuthDataProvider>();
		provider.User
			.Returns(repo);

		var userId = IdGen.LongId<AuthUserId>();

		return new(log, provider, repo, userId);
	}

	[Fact]
	public async Task Calls_Log_Vrb__With_Correct_Values()
	{
		// Arrange
		var v = Setup();

		// Act
		_ = await AuthF.UpdateUserLastSignInAsync(v.Provider, v.UserId, v.Log);

		// Assert
		v.Log.Received().Vrb("Updating last sign in for user {UserId}.", v.UserId.Value);
	}

	[Fact]
	public async Task Calls_UpdateLastSignInAsync__With_Correct_Values()
	{
		// Arrange
		var v = Setup();

		// Act
		_ = await AuthF.UpdateUserLastSignInAsync(v.Provider, v.UserId, v.Log);

		// Assert
		await v.Repository.Received().UpdateLastSignInAsync(v.UserId);
	}

	[Fact]
	public async Task Calls_UpdateLastSignInAsync__Receives_None__Calls_Log_Msg__With_Correct_Values()
	{
		// Arrange
		var v = Setup();
		var failure = FailGen.Create();
		v.Repository.UpdateLastSignInAsync(default!)
			.ReturnsForAnyArgs(failure);

		// Act
		_ = await AuthF.UpdateUserLastSignInAsync(v.Provider, v.UserId, v.Log);

		// Assert
		v.Log.Received().Failure(failure.Value);
	}

	[Fact]
	public async Task Calls_UpdateLastSignInAsync__Receives_None__Returns_False()
	{
		// Arrange
		var v = Setup();
		v.Repository.UpdateLastSignInAsync(default!)
			.ReturnsForAnyArgs(FailGen.Create());

		// Act
		var result = await AuthF.UpdateUserLastSignInAsync(v.Provider, v.UserId, v.Log);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public async Task Calls_UpdateLastSignInAsync__Receives_Some__Returns_Result()
	{
		// Arrange
		var v = Setup();
		var value = Rnd.Flip;
		v.Repository.UpdateLastSignInAsync(default!)
			.ReturnsForAnyArgs(value);

		// Act
		var result = await AuthF.UpdateUserLastSignInAsync(v.Provider, v.UserId, v.Log);

		// Assert
		Assert.Equal(value, result);
	}
}
