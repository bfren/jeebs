﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using System.Security.Principal;
using Jeebs;
using Jeebs.Config;
using NSubstitute;
using Xunit;
using static F.JwtF.Msg;

namespace F.JwtF_Tests;

public class CreateToken_Tests
{
	[Fact]
	public void Identity_Null_Returns_None_With_NullIdentityMsg()
	{
		// Arrange
		var config = new JwtConfig();
		var principal = Substitute.For<ClaimsPrincipal>();
		principal.Identity.Returns(_ => null);

		// Act
		var result = JwtF.CreateToken(config, principal);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<NullIdentityMsg>(none);
	}

	[Fact]
	public void Identity_Not_Authenticated_Returns_None_With_IdentityNotAuthenticatedMsg()
	{
		// Arrange
		var config = new JwtConfig();
		var principal = Substitute.For<ClaimsPrincipal>();

		// Act
		var result = JwtF.CreateToken(config, principal);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<IdentityNotAuthenticatedMsg>(none);
	}

	[Fact]
	public void Invalid_Config_Returns_None_With_JwtConfigInvalidMsg()
	{
		// Arrange
		var config = new JwtConfig();
		var identity = Substitute.For<IIdentity>();
		identity.IsAuthenticated.Returns(true);
		var principal = Substitute.For<ClaimsPrincipal>();
		principal.Identity.Returns(identity);

		// Act
		var result = JwtF.CreateToken(config, principal);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<ConfigInvalidMsg>(none);
	}

	[Fact]
	public void SigningKey_Not_Long_Enough_Returns_None_With_SigningKeyNotLongEnoughMsg()
	{
		// Arrange
		var config = new JwtConfig
		{
			SigningKey = Rnd.Str,
			Issuer = Rnd.Str,
			Audience = Rnd.Str
		};
		var identity = Substitute.For<IIdentity>();
		identity.IsAuthenticated.Returns(true);
		var principal = Substitute.For<ClaimsPrincipal>();
		principal.Identity.Returns(identity);

		// Act
		var result = JwtF.CreateToken(config, principal);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<SigningKeyNotLongEnoughMsg>(none);
	}

	[Fact]
	public void EncryptingKey_Not_Long_Enough_Returns_None_With_EncryptingKeyNotLongEnoughMsg()
	{
		// Arrange
		var config = new JwtConfig
		{
			SigningKey = Rnd.StringF.Get(32),
			EncryptingKey = Rnd.Str,
			Issuer = Rnd.Str,
			Audience = Rnd.Str
		};
		var identity = Substitute.For<IIdentity>();
		identity.IsAuthenticated.Returns(true);
		var principal = Substitute.For<ClaimsPrincipal>();
		principal.Identity.Returns(identity);

		// Act
		var result = JwtF.CreateToken(config, principal);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<EncryptingKeyNotLongEnoughMsg>(none);
	}

	[Fact]
	public void Valid_Config_Without_Encryption_Returns_Token()
	{
		// Arrange
		var config = new JwtConfig
		{
			SigningKey = Rnd.StringF.Get(32),
			Issuer = Rnd.Str,
			Audience = Rnd.Str
		};
		var identity = Substitute.For<IIdentity>();
		identity.IsAuthenticated.Returns(true);
		var principal = Substitute.For<ClaimsPrincipal>();
		principal.Identity.Returns(identity);

		// Act
		var result = JwtF.CreateToken(config, principal);

		// Assert
		result.AssertSome();
	}

	[Fact]
	public void Valid_Config_With_Encryption_Returns_Token()
	{
		// Arrange
		var config = new JwtConfig
		{
			SigningKey = Rnd.StringF.Get(32),
			EncryptingKey = Rnd.StringF.Get(64),
			Issuer = Rnd.Str,
			Audience = Rnd.Str
		};
		var identity = Substitute.For<IIdentity>();
		identity.IsAuthenticated.Returns(true);
		var principal = Substitute.For<ClaimsPrincipal>();
		principal.Identity.Returns(identity);

		// Act
		var result = JwtF.CreateToken(config, principal);

		// Assert
		result.AssertSome();
	}
}
