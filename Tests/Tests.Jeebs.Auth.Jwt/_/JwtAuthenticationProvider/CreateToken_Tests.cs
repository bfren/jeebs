using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Config;
using Jm.Functions.JwtF.CreateToken;
using NSubstitute;
using Xunit;

namespace Jeebs.Auth.JwtAuthenticationProvider_Tests
{
	public class CreateToken_Tests
	{

		[Fact]
		public void Identity_Null_Returns_None_With_NullIdentityMsg()
		{
			// Arrange
			var config = new JwtConfig();
			var principal = Substitute.For<IPrincipal>();
			principal.Identity.Returns(_ => null);
			var provider = new JwtAuthenticationProvider(config);

			// Act
			var result = provider.CreateToken(principal);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			Assert.IsType<NullIdentityMsg>(none.Reason);
		}

		[Fact]
		public void Identity_Not_Authenticated_Returns_None_With_IdentityNotAuthenticatedMsg()
		{
			// Arrange
			var config = new JwtConfig();
			var principal = Substitute.For<IPrincipal>();
			var provider = new JwtAuthenticationProvider(config);

			// Act
			var result = provider.CreateToken(principal);

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
			var principal = Substitute.For<IPrincipal>();
			principal.Identity.Returns(identity);
			var provider = new JwtAuthenticationProvider(config);

			// Act
			var result = provider.CreateToken(principal);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			Assert.IsType<JwtConfigInvalidMsg>(none.Reason);
		}

		[Fact]
		public void SigningKey_Not_Long_Enough_Returns_None_With_SigningKeyNotLongEnoughMsg()
		{
			// Arrange
			var config = new JwtConfig
			{
				SigningKey = F.Rnd.Str,
				Issuer = F.Rnd.Str,
				Audience = F.Rnd.Str
			};
			var identity = Substitute.For<IIdentity>();
			identity.IsAuthenticated.Returns(true);
			var principal = Substitute.For<IPrincipal>();
			principal.Identity.Returns(identity);
			var provider = new JwtAuthenticationProvider(config);

			// Act
			var result = provider.CreateToken(principal);

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
				SigningKey = F.StringF.Random(32),
				EncryptingKey = F.Rnd.Str,
				Issuer = F.Rnd.Str,
				Audience = F.Rnd.Str
			};
			var identity = Substitute.For<IIdentity>();
			identity.IsAuthenticated.Returns(true);
			var principal = Substitute.For<IPrincipal>();
			principal.Identity.Returns(identity);
			var provider = new JwtAuthenticationProvider(config);

			// Act
			var result = provider.CreateToken(principal);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			Assert.IsType<EncryptingKeyNotLongEnoughMsg>(none.Reason);
		}

		[Fact]
		public void Valid_Config_Returns_Token()
		{
			// Arrange
			var config = new JwtConfig
			{
				SigningKey = F.StringF.Random(32),
				EncryptingKey = F.StringF.Random(64),
				Issuer = F.Rnd.Str,
				Audience = F.Rnd.Str
			};
			var identity = Substitute.For<IIdentity>();
			identity.IsAuthenticated.Returns(true);
			var principal = Substitute.For<IPrincipal>();
			principal.Identity.Returns(identity);
			var provider = new JwtAuthenticationProvider(config);

			// Act
			var result = provider.CreateToken(principal);

			// Assert
			Assert.IsType<Some<string>>(result);
		}
	}
}
