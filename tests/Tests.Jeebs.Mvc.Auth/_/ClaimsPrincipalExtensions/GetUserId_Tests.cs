// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using Jeebs.Auth.Jwt.Constants;
using static Jeebs.Mvc.Auth.ClaimsPrincipalExtensions.M;
using static MaybeF.F.EnumerableF.M;

namespace Jeebs.Mvc.Auth.ClaimsPrincipalExtensions_Tests;

public class GetUserId_Tests : ClaimsPrincipalExtensions_Tests
{
	[Fact]
	public void Claims_Does_Not_Contain_UserId__Returns_None_With_NoMatchingItemsMsg()
	{
		// Arrange
		var principal = Setup(true, new[] { new Claim(Rnd.Str, Rnd.Str), new Claim(Rnd.Str, Rnd.Str) });

		// Act
		var result = principal.GetUserId();

		// Assert
		result.AssertNone().AssertType<NoMatchingItemsMsg>();
	}

	[Fact]
	public void Claims_Contains_Multiple_UserIds__Returns_None_With_MultipleItemsMsg()
	{
		// Arrange
		var principal = Setup(true, new[] { new Claim(JwtClaimTypes.UserId, Rnd.Str), new Claim(JwtClaimTypes.UserId, Rnd.Str) });

		// Act
		var result = principal.GetUserId();

		// Assert
		result.AssertNone().AssertType<MultipleItemsMsg>();
	}

	[Fact]
	public void Claims_Contains_Single_UserId__Incorrect_Type__Returns_None_With_InvalidUserIdMsg()
	{
		// Arrange
		var principal = Setup(true, new[] { new Claim(JwtClaimTypes.UserId, Rnd.Str) });

		// Act
		var result = principal.GetUserId();

		// Assert
		result.AssertNone().AssertType<InvalidUserIdMsg>();
	}

	[Fact]
	public void Claims_Contains_Single_UserId__Correct_Type__Returns_Some_With_AuthUserId()
	{
		// Arrange
		var userId = Rnd.Lng;
		var principal = Setup(true, new[] { new Claim(JwtClaimTypes.UserId, userId.ToString()) });

		// Act
		var result = principal.GetUserId();

		// Assert
		var some = result.AssertSome();
		Assert.Equal(userId, some.Value);
	}
}
