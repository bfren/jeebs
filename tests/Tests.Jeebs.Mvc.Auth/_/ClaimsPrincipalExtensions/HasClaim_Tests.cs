// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;

namespace Jeebs.Mvc.Auth.ClaimsPrincipalExtensions_Tests;

public class HasClaim_Tests : ClaimsPrincipalExtensions_Tests
{
	[Fact]
	public void Principal_Identity_Null__Returns_False()
	{
		// Arrange
		var principal = Substitute.ForPartsOf<ClaimsPrincipal>();

		// Act
		var result = principal.HasClaim(Rnd.Str);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Principal_Identity_Not_Authenticated__Returns_False()
	{
		// Arrange
		var principal = Setup(false);

		// Act
		var result = principal.HasClaim(Rnd.Str);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Claims_Is_Empty__Returns_False()
	{
		// Arrange
		var principal = Setup(true);

		// Act
		var result = principal.HasClaim(Rnd.Str);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Claims_Does_Not_Contain__Returns_False()
	{
		// Arrange
		var principal = Setup(true, new Claim[] { new(Rnd.Str, Rnd.Str), new(Rnd.Str, Rnd.Str) });

		// Act
		var result = principal.HasClaim(Rnd.Str);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Claims_Contains_Multiple__Returns_False()
	{
		// Arrange
		var type = Rnd.Str;
		var principal = Setup(true, new Claim[] { new(type, Rnd.Str), new(type, Rnd.Str) });

		// Act
		var result = principal.HasClaim(type);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Claims_Contains_Single__Returns_True()
	{
		// Arrange
		var type = Rnd.Str;
		var value = Rnd.Str;
		var principal = Setup(true, new[] { new Claim(type, value) });

		// Act
		var result = principal.HasClaim(type);

		// Assert
		Assert.True(result);
	}
}
