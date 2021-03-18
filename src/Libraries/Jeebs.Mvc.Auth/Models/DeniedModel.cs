// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Mvc.Auth.Models
{
	/// <summary>
	/// Access Denied Model
	/// </summary>
	/// <param name="AccessUrl">The URL the user was trying to access</param>
	public sealed record DeniedModel(string? AccessUrl);
}
