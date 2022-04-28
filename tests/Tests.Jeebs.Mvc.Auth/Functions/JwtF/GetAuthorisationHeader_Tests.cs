// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Primitives;
using static Jeebs.Mvc.Auth.Functions.JwtF.M;

namespace Jeebs.Mvc.Auth.Functions.JwtF_Tests;

public class GetAuthorisationHeader_Tests
{
	[Fact]
	public void Missing_Header_Returns_None_With_MissingAuthorisationHeaderMsg()
	{
		// Arrange
		var headers = new Dictionary<string, StringValues>();

		// Act
		var result = JwtF.GetAuthorisationHeader(headers);

		// Assert
		result.AssertNone().AssertType<MissingAuthorisationHeaderMsg>();
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
		var result = JwtF.GetAuthorisationHeader(headers);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}
}
