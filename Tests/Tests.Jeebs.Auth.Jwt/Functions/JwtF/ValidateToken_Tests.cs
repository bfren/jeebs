using System;
using System.Security.Principal;
using Jeebs;
using Jeebs.Config;
using Jm.Functions.JwtF.ValidateToken;
using NSubstitute;
using Xunit;
using Defaults = Jeebs.Auth.Defaults.Algorithms;

namespace F.JwtF_Tests
{
	public class ValidateToken_Tests
	{
		private (JwtConfig config, string token, string user) GetToken(DateTime notBefore, DateTime expires)
		{
			var config = new JwtConfig
			{
				SigningKey = StringF.Random(32),
				EncryptingKey = StringF.Random(64),
				Issuer = Rnd.Str,
				Audience = Rnd.Str
			};
			var name = Rnd.Str;
			var identity = Substitute.For<IIdentity>();
			identity.IsAuthenticated.Returns(true);
			identity.Name.Returns(name);
			var principal = Substitute.For<IPrincipal>();
			principal.Identity.Returns(identity);

			var token = JwtF.CreateToken(
				config,
				principal,
				Defaults.Signing,
				Defaults.Encrypting,
				notBefore,
				expires
			).Unwrap(
				() => throw new Exception()
			);

			return (config, token, name);
		}

		[Fact]
		public void Not_Valid_Yet_Returns_None_With_TokenIsNotValidYetMsg()
		{
			// Arrange
			var (config, token, _) = GetToken(DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(1));

			// Act
			var result = JwtF.ValidateToken(config, token);

			// Assert
			var none = Assert.IsType<None<IPrincipal>>(result);
			Assert.IsType<TokenIsNotValidYetMsg>(none.Reason);
		}

		[Fact]
		public void Expired_Returns_None_With_TokenHasExpiredMsg()
		{
			// Arrange
			var (config, token, _) = GetToken(DateTime.UtcNow, DateTime.UtcNow);
			System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

			// Act
			var result = JwtF.ValidateToken(config, token);

			// Assert
			var none = Assert.IsType<None<IPrincipal>>(result);
			Assert.IsType<TokenHasExpiredMsg>(none.Reason);
		}

		[Fact]
		public void Valid_Token_Returns_Principal()
		{
			// Arrange
			var (config, token, name) = GetToken(DateTime.UtcNow, DateTime.UtcNow.AddHours(1));

			// Act
			var result = JwtF.ValidateToken(config, token);

			// Assert
			var some = Assert.IsType<Some<IPrincipal>>(result);
			Assert.Equal(name, some.Value.Identity?.Name);
		}
	}
}
