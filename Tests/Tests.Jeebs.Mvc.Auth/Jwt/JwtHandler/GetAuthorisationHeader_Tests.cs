// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs.Mvc.Auth.Jwt.JwtHandlerMsg;
using Microsoft.Extensions.Primitives;
using Xunit;

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
			var none = Assert.IsType<None<string>>(result);
			Assert.IsType<MissingAuthorisationHeaderMsg>(none.Reason);
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
			var some = Assert.IsType<Some<string>>(result);
			Assert.Equal(value, some.Value);
		}
	}
}
