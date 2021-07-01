// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static Jeebs.Mvc.Auth.Jwt.JwtHandler.Msg;

namespace Jeebs.Mvc.Auth.Jwt.JwtHandler_Tests
{
	public class GetToken_Tests
	{
		[Fact]
		public void Invalid_Authorization_Header_Returns_None_With_InvalidAuthorisationHeaderMsg()
		{
			// Arrange
			var header = F.Rnd.Str;

			// Act
			var result = JwtHandler.GetToken(header);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			Assert.IsType<InvalidAuthorisationHeaderMsg>(none.Reason);
		}

		[Fact]
		public void Returns_Token()
		{
			// Arrange
			var value = F.Rnd.Str;
			var header = $"Bearer {value}";

			// Act
			var result = JwtHandler.GetToken(header);

			// Assert
			var some = Assert.IsType<Some<string>>(result);
			Assert.Equal(value, some.Value);
		}
	}
}
