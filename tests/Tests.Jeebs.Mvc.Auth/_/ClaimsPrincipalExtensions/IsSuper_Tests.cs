// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using Jeebs.Auth.Jwt.Constants;

namespace Jeebs.Mvc.Auth.ClaimsPrincipalExtensions_Tests;

public class IsSuper_Tests : ClaimsPrincipalExtensions_Tests
{
	[Fact]
	public void Principal_Identity_Null__Returns_False()
	{
		// Arrange
		var principal = Substitute.ForPartsOf<ClaimsPrincipal>();

		// Act
		var result = principal.IsSuper();

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Principal_Identity_Not_Authenticated__Returns_False()
	{
		// Arrange
		var principal = Setup(false);

		// Act
		var result = principal.IsSuper();

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Claims_Is_Empty__Returns_False()
	{
		// Arrange
		var principal = Setup(true);

		// Act
		var result = principal.IsSuper();

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Claims_Does_Not_Contain__Returns_False()
	{
		// Arrange
		var principal = Setup(true, new Claim[] { new(Rnd.Str, Rnd.Str), new(Rnd.Str, Rnd.Str) });

		// Act
		var result = principal.IsSuper();

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Claims_Contains_Multiple__Returns_False()
	{
		// Arrange
		var principal = Setup(true, new Claim[] { new(JwtClaimTypes.IsSuper, Rnd.Str), new(JwtClaimTypes.IsSuper, Rnd.Str) });

		// Act
		var result = principal.IsSuper();

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Claims_Contains_Single__Returns_True()
	{
		// Arrange
		var principal = Setup(true, new[] { new Claim(JwtClaimTypes.IsSuper, Rnd.Str) });

		// Act
		var result = principal.IsSuper();

		// Assert
		Assert.True(result);
	}
}
