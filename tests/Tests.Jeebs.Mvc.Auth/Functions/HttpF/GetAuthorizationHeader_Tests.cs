// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Primitives;

namespace Jeebs.Mvc.Auth.Functions.HttpF_Tests;

public class GetAuthorizationHeader_Tests
{
	[Fact]
	public void Missing_Header_Returns_None_With_MissingAuthorisationHeaderMsg()
	{
		// Arrange
		var headers = new Dictionary<string, StringValues>();

		// Act
		var result = HttpF.GetAuthorizationHeader(headers);

		// Assert
		result.AssertFailure("'Authorization' header is missing.");
	}

	[Fact]
	public void Returns_Authorization_Header()
	{
		// Arrange
		var value = Rnd.Str;
		var headers = new Dictionary<string, StringValues>
		{
			{ "Authorization", value }
		};

		// Act
		var result = HttpF.GetAuthorizationHeader(headers);

		// Assert
		result.AssertOk(value);
	}
}
