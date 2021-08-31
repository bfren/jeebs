// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Microsoft.Extensions.Primitives;
using Xunit;
using static Jeebs.Mvc.Auth.Jwt.JwtHandler.Msg;

namespace Jeebs.Mvc.Auth.Jwt.JwtHandler_Tests
{
	public class GetAuthorisationHeader_Tests
	{
		[Fact]
		public void Missing_Header_Returns_None_With_MissingAuthorisationHeaderMsg()
		{
			// Arrange
			var headers = new Dictionary<string, StringValues>();

			// Act
			var result = JwtHandler.GetAuthorisationHeader(headers);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<MissingAuthorisationHeaderMsg>(none);
		}

		[Fact]
		public void Returns_Authorization_Header()
		{
			// Arrange
			var value = F.Rnd.Str;
			var headers = new Dictionary<string, StringValues>
			{
				{ "Authorization", value }
			};

			// Act
			var result = JwtHandler.GetAuthorisationHeader(headers);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(value, some);
		}
	}
}
