// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using static Jeebs.Mvc.Auth.ClaimsPrincipalExtensions.M;
using static MaybeF.F.EnumerableF.M;

namespace Jeebs.Mvc.Auth.ClaimsPrincipalExtensions_Tests;

public class GetClaim_Tests : ClaimsPrincipalExtensions_Tests
{
	[Fact]
	public void Principal_Identity_Null__Returns_None_With_UserIsNotAuthenticatedMsg()
	{
		// Arrange
		var principal = Substitute.ForPartsOf<ClaimsPrincipal>();

		// Act
		var result = principal.GetClaim(Rnd.Str);

		// Assert
		result.AssertNone().AssertType<UserIsNotAuthenticatedMsg>();
	}

	[Fact]
	public void Principal_Identity_Not_Authenticated__Returns_None_With_UserIsNotAuthenticatedMsg()
	{
		// Arrange
		var principal = Setup(false);

		// Act
		var result = principal.GetClaim(Rnd.Str);

		// Assert
		result.AssertNone().AssertType<UserIsNotAuthenticatedMsg>();
	}

	[Fact]
	public void Claims_Is_Empty__Returns_None_With_ListIsEmptyMsg()
	{
		// Arrange
		var principal = Setup(true);

		// Act
		var result = principal.GetClaim(Rnd.Str);

		// Assert
		result.AssertNone().AssertType<ListIsEmptyMsg>();
	}

	[Fact]
	public void Claims_Does_Not_Contain__Returns_None_With_NoMatchingItemsMsg()
	{
		// Arrange
		var principal = Setup(true, new Claim[] { new(Rnd.Str, Rnd.Str), new(Rnd.Str, Rnd.Str) });

		// Act
		var result = principal.GetClaim(Rnd.Str);

		// Assert
		result.AssertNone().AssertType<NoMatchingItemsMsg>();
	}

	[Fact]
	public void Claims_Contains_Multiple__Returns_None_With_MultipleItemsMsg()
	{
		// Arrange
		var type = Rnd.Str;
		var principal = Setup(true, new Claim[] { new(type, Rnd.Str), new(type, Rnd.Str) });

		// Act
		var result = principal.GetClaim(type);

		// Assert
		result.AssertNone().AssertType<MultipleItemsMsg>();
	}

	[Fact]
	public void Claims_Contains_Single__Returns_Some_With_Value()
	{
		// Arrange
		var type = Rnd.Str;
		var value = Rnd.Str;
		var principal = Setup(true, new[] { new Claim(type, value) });

		// Act
		var result = principal.GetClaim(type);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}
}
