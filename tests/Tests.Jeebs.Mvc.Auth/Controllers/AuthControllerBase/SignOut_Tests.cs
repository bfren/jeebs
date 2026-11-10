// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Auth.Controllers.AuthControllerBase_Tests;

public class SignOut_Tests
{
	[Fact]
	public void Requires_Post_With_AntiForgery_Token()
	{
		// Arrange
		var method = typeof(AuthControllerBase).GetMethod(
			nameof(AuthControllerBase.SignOut),
			BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly,
			Type.EmptyTypes
		)!;

		// Act
		var post = method.GetCustomAttribute<HttpPostAttribute>();
		var antiForgery = method.GetCustomAttribute<ValidateAntiForgeryTokenAttribute>();

		// Assert
		Assert.NotNull(post);
		Assert.NotNull(antiForgery);
	}
}
