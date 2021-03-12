// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Security.Claims;
using System.Security.Principal;
using F.JwtFMsg;
using Jeebs;
using Jeebs.Config;
using NSubstitute;
using Xunit;

namespace F.JwtF_Tests
{
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
			var none = Assert.IsType<None<string>>(result);
			Assert.IsType<NullIdentityMsg>(none.Reason);
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
			var none = Assert.IsType<None<string>>(result);
			Assert.IsType<IdentityNotAuthenticatedMsg>(none.Reason);
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
			var none = Assert.IsType<None<string>>(result);
			Assert.IsType<ConfigInvalidMsg>(none.Reason);
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
			var none = Assert.IsType<None<string>>(result);
			Assert.IsType<SigningKeyNotLongEnoughMsg>(none.Reason);
		}

		[Fact]
		public void EncryptingKey_Not_Long_Enough_Returns_None_With_EncryptingKeyNotLongEnoughMsg()
		{
			// Arrange
			var config = new JwtConfig
			{
				SigningKey = StringF.Random(32),
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
			var none = Assert.IsType<None<string>>(result);
			Assert.IsType<EncryptingKeyNotLongEnoughMsg>(none.Reason);
		}

		[Fact]
		public void Valid_Config_Without_Encryption_Returns_Token()
		{
			// Arrange
			var config = new JwtConfig
			{
				SigningKey = StringF.Random(32),
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
			Assert.IsType<Some<string>>(result);
		}

		[Fact]
		public void Valid_Config_With_Encryption_Returns_Token()
		{
			// Arrange
			var config = new JwtConfig
			{
				SigningKey = StringF.Random(32),
				EncryptingKey = StringF.Random(64),
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
			Assert.IsType<Some<string>>(result);
		}
	}
}
