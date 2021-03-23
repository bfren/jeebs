// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Auth.Data.Models
{
	/// <inheritdoc cref="Jeebs.Auth.Data.IAuthCreateUserModel"/>
	public record AuthCreateUserModel(string EmailAddress, string Password) : IAuthCreateUserModel;
}
